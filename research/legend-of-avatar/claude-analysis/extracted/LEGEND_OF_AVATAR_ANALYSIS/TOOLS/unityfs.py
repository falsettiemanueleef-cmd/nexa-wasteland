import struct, lzma, sys, os, json, time

def lz4_block(src, usize):
    dst = bytearray()
    i = 0; n = len(src)
    while i < n:
        t = src[i]; i += 1
        ll = t >> 4
        if ll == 15:
            while True:
                b = src[i]; i += 1; ll += b
                if b != 255: break
        dst += src[i:i+ll]; i += ll
        if i >= n: break
        off = src[i] | (src[i+1] << 8); i += 2
        ml = t & 15
        if ml == 15:
            while True:
                b = src[i]; i += 1; ml += b
                if b != 255: break
        ml += 4
        s = len(dst) - off
        if off >= ml:
            dst += dst[s:s+ml]
        else:
            pat = bytes(dst[s:])
            dst += (pat * (ml // off + 1))[:ml]
    if len(dst) != usize:
        raise ValueError("lz4 size mismatch %d != %d" % (len(dst), usize))
    return bytes(dst)

def decomp(data, usize, mode):
    if mode == 0: return data
    if mode == 1:
        # unity LZMA: 5 byte props then raw
        props = data[:5]
        lc = props[0] % 9; r = props[0] // 9; lp = r % 5; pb = r // 5
        dict_size = struct.unpack('<I', props[1:5])[0]
        f = [{'id': lzma.FILTER_LZMA1, 'dict_size': dict_size, 'lc': lc, 'lp': lp, 'pb': pb}]
        return lzma.LZMADecompressor(format=lzma.FORMAT_RAW, filters=f).decompress(data[5:], max_length=usize)
    if mode in (2, 3): return lz4_block(data, usize)
    raise ValueError("compression %d" % mode)

def cstr(b, p):
    e = b.index(0, p); return b[p:e].decode('utf8', 'replace'), e + 1

def parse(path):
    b = open(path, 'rb').read()
    sig, p = cstr(b, 0)
    ver = struct.unpack_from('>I', b, p)[0]; p += 4
    uv, p = cstr(b, p); rev, p = cstr(b, p)
    size, cbi, ubi, flags = struct.unpack_from('>QIII', b, p); p += 20
    hdr = dict(sig=sig, version=ver, unity=uv, revision=rev, size=size, cbi=cbi, ubi=ubi, flags=hex(flags), filesize=len(b))
    if ver >= 7: p = (p + 15) & ~15
    if flags & 0x80: raw = b[len(b)-cbi:]
    else: raw = b[p:p+cbi]; p += cbi
    bi = decomp(raw, ubi, flags & 0x3f)
    q = 16
    nb = struct.unpack_from('>i', bi, q)[0]; q += 4
    blocks = []
    for _ in range(nb):
        u, c, f = struct.unpack_from('>IIH', bi, q); q += 10; blocks.append((u, c, f))
    nn = struct.unpack_from('>i', bi, q)[0]; q += 4
    nodes = []
    for _ in range(nn):
        off, sz, fl = struct.unpack_from('>QQI', bi, q); q += 20
        name, q = cstr(bi, q); nodes.append(dict(offset=off, size=sz, flags=fl, path=name))
    return b, p, hdr, blocks, nodes

if __name__ == '__main__':
    src, outdir = sys.argv[1], sys.argv[2]
    os.makedirs(outdir, exist_ok=True)
    b, p, hdr, blocks, nodes = parse(src)
    print(hdr); print('blocks', len(blocks), 'nodes', len(nodes))
    p = (p + 15) & ~15
    t = time.time(); out = bytearray()
    for (u, c, f) in blocks:
        out += decomp(b[p:p+c], u, f & 0x3f); p += c
    print('decompressed', len(out), 'in %.1fs' % (time.time() - t))
    for n in nodes:
        data = bytes(out[n['offset']:n['offset']+n['size']])
        open(os.path.join(outdir, n['path'].replace('/', '_')), 'wb').write(data)
        print(n['path'], n['size'], hex(n['flags']))
    json.dump(dict(header=hdr, nodes=nodes, blocks=len(blocks)), open(os.path.join(outdir, '_index.json'), 'w'), indent=1)
