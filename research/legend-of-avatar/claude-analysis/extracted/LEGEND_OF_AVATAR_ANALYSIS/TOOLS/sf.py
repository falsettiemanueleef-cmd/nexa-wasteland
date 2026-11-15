import struct, io

class R:
    def __init__(s, b, p=0, be=True): s.b=b; s.p=p; s.be=be
    def u8(s): v=s.b[s.p]; s.p+=1; return v
    def _u(s, fmt, n):
        v=struct.unpack_from(('>' if s.be else '<')+fmt, s.b, s.p)[0]; s.p+=n; return v
    def u16(s): return s._u('H',2)
    def i16(s): return s._u('h',2)
    def u32(s): return s._u('I',4)
    def i32(s): return s._u('i',4)
    def i64(s): return s._u('q',8)
    def u64(s): return s._u('Q',8)
    def f32(s): return s._u('f',4)
    def align(s, n=4): s.p = (s.p + n - 1) // n * n
    def cstr(s):
        e=s.b.index(0, s.p); v=s.b[s.p:e].decode('utf8','replace'); s.p=e+1; return v
    def bytes(s, n): v=s.b[s.p:s.p+n]; s.p+=n; return v
    def string(s):
        n=s.i32(); v=s.b[s.p:s.p+n].decode('utf8','replace'); s.p+=n; s.align(4); return v

CLASS_NAMES = {1:'GameObject',2:'Component',4:'Transform',20:'Camera',21:'Material',23:'MeshRenderer',25:'Renderer',28:'Texture2D',33:'MeshFilter',43:'Mesh',48:'Shader',49:'TextAsset',54:'Rigidbody',65:'BoxCollider',74:'AnimationClip',82:'AudioSource',83:'AudioClip',90:'Avatar',91:'AnimatorController',95:'Animator',104:'RenderSettings',108:'Light',111:'Animation',114:'MonoBehaviour',115:'MonoScript',128:'Font',135:'SphereCollider',136:'CapsuleCollider',137:'SkinnedMeshRenderer',152:'MovieTexture',156:'TerrainData',198:'ParticleSystem',199:'ParticleSystemRenderer',200:'ShaderVariantCollection',213:'Sprite',212:'SpriteRenderer',215:'ReflectionProbe',220:'LightProbeGroup',221:'AnimatorOverrideController',222:'CanvasRenderer',223:'Canvas',224:'RectTransform',225:'CanvasGroup',226:'BillboardAsset',258:'LightProbes',273:'Skybox',290:'AssetBundle',304:'MonoManager',308:'SpriteAtlas',310:'UnityConnectSettings',319:'AvatarMask',320:'PlayableDirector',328:'VideoPlayer',329:'VideoClip',363:'OcclusionCullingData',1001:'AssetBundleManifest',1004:'MonoScriptX',687078895:'SpriteAtlas',1953259897:'TerrainLayer',142:'AssetBundle',141:'BuildSettings',129:'PlayerSettings',150:'PreloadData',147:'ResourceManager',330:'Texture2DArray',187:'Texture3D',89:'CubeMap',117:'Texture',118:'Texture2D_',156:'TerrainData',184:'AudioMixer',240:'AudioMixerController',241:'AudioMixerGroup',243:'AudioMixerSnapshot',244:'AudioMixerGroupController',272:'AudioMixerEffectController',271:'AudioMixerSnapshotController',331:'Font_',1115:'MonoScript_',}

def parse(path):
    b = open(path,'rb').read()
    r = R(b)
    r.u32(); r.u32(); version = r.u32(); r.u32()
    out = dict(file=path, size=len(b), version=version)
    if version >= 9:
        endian = r.u8(); r.bytes(3)
    else: endian = 1
    if version >= 22:
        meta_size = r.u32(); fsize = r.i64(); data_offset = r.i64(); r.i64()
    else:
        raise NotImplementedError
    r.be = (endian != 0)
    out.update(endian=endian, meta_size=meta_size, data_offset=data_offset)
    out['unity_version'] = r.cstr()
    out['target_platform'] = r.i32()
    out['type_tree'] = bool(r.u8())
    tt = out['type_tree']
    ntypes = r.i32(); types=[]
    for _ in range(ntypes):
        t = {}
        t['class_id'] = r.i32(); t['stripped'] = r.u8(); t['script_idx'] = r.i16()
        if t['class_id'] == 114: t['script_id'] = r.bytes(16).hex()
        t['hash'] = r.bytes(16).hex()
        if tt:
            nn = r.i32(); sb = r.i32(); nodes=[]
            for _ in range(nn):
                nodes.append((r.u16(), r.u8(), r.u8(), r.u32(), r.u32(), r.i32(), r.i32(), r.i32(), r.u64()))
            strbuf = r.bytes(sb)
            def gs(o):
                if o & 0x80000000: return 'COMMON%d' % (o & 0x7fffffff)
                e = strbuf.index(0, o); return strbuf[o:e].decode()
            t['nodes'] = [dict(ver=n[0], level=n[1], flags=n[2], type=gs(n[3]), name=gs(n[4]), size=n[5], index=n[6], meta=n[7]) for n in nodes]
        if tt:
            ndeps = r.i32(); t['deps'] = [r.i32() for _ in range(ndeps)]
        types.append(t)
    out['types'] = types
    nobj = r.i32(); objs=[]
    for _ in range(nobj):
        r.align(4)
        pid = r.i64(); start = r.i64(); size = r.u32(); tid = r.i32()
        objs.append(dict(path_id=pid, offset=data_offset+start, size=size, type_idx=tid, class_id=types[tid]['class_id'], script_idx=types[tid]['script_idx']))
    out['objects'] = objs
    ns = r.i32(); scripts=[]
    for _ in range(ns):
        fi = r.i32(); r.align(4); li = r.i64(); scripts.append((fi, li))
    out['script_types'] = scripts
    ne = r.i32(); ext=[]
    for _ in range(ne):
        r.cstr(); g = r.bytes(16).hex(); ty = r.i32(); p = r.cstr(); ext.append(dict(guid=g, type=ty, path=p))
    out['externals'] = ext
    out['ref_types_count'] = r.i32()
    out['_end'] = r.p
    return b, out
