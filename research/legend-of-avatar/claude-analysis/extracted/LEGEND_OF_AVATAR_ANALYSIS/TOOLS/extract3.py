import sf, csv, collections, struct
FMT={1:'Alpha8',3:'RGB24',4:'RGBA32',5:'ARGB32',7:'RGB565',10:'DXT1',12:'DXT5',34:'ETC_RGB4',45:'ETC2_RGB',46:'ETC2_RGBA1',47:'ETC2_RGBA8',48:'ASTC_4x4',49:'ASTC_5x5',50:'ASTC_6x6',51:'ASTC_8x8',52:'ASTC_10x10',53:'ASTC_12x12',63:'R8',25:'BC7'}
rows=[]
for fname in ['globalgamemanagers.assets','sharedassets0.assets']:
    b,o=sf.parse('vfs/'+fname)
    for ob in o['objects']:
        if ob['class_id']!=28: continue
        d=b[ob['offset']:ob['offset']+ob['size']]
        r=sf.R(d,0,be=False); name=r.string(); r.u8(); r.u8(); r.align(4)
        w=r.i32(); h=r.i32(); cs=r.i32(); r.i32(); fmt=r.i32(); mc=r.i32()
        sf_name=''; so=ss=0
        for cand in (b'sharedassets0.assets.resS', b'globalgamemanagers.assets.resS'):
            k=d.rfind(cand)
            if k>=0:
                sf_name=cand.decode(); so=struct.unpack_from('<Q',d,k-16)[0]; ss=struct.unpack_from('<I',d,k-8)[0]; break
        rows.append(dict(file=fname,path_id=ob['path_id'],name=name,width=w,height=h,format=FMT.get(fmt,fmt),mips=mc,complete_size=cs,stream_file=sf_name,stream_offset=so,stream_size=ss))
with open('out/textures.csv','w',newline='') as f:
    wr=csv.DictWriter(f,fieldnames=list(rows[0].keys())); wr.writeheader(); wr.writerows(rows)
print(len(rows), collections.Counter(r['format'] for r in rows))
print(collections.Counter((r['width'],r['height']) for r in rows).most_common(6))
bad=[r for r in rows if r['stream_file'] and r['stream_offset']+r['stream_size']>os.path.getsize('vfs/'+r['stream_file']) ] if False else []
print([ (r['name'][:40],r['width'],r['height'],r['format'],r['stream_offset'],r['stream_size']) for r in rows[:4]])
