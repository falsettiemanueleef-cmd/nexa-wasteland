import sf, csv, json, collections, os
scripts={}
for r in csv.DictReader(open('out/monoscripts.csv')): scripts[int(r['path_id'])]=r
gg_b, gg = sf.parse('vfs/globalgamemanagers.assets')
files={'globalgamemanagers.assets':(gg_b,gg)}
for f in ['sharedassets0.assets','level0']:
    files[f]=sf.parse('vfs/'+f)
mb_rows=[]; go_rows=[]; sprite_rows=[]; tex_rows=[]; misc_rows=[]
for fname,(b,o) in files.items():
    ext=o['externals']
    for ob in o['objects']:
        cid=ob['class_id']; r=sf.R(b,ob['offset'],be=False)
        try:
            if cid==114:
                gofile=r.i32(); gopid=r.i64(); en=r.u8(); r.align(4)
                sfile=r.i32(); spid=r.i64(); name=r.string()
                # script ptr: file index 0=local, >=1 -> externals[idx-1]
                tgt = 'local' if sfile==0 else ext[sfile-1]['path'] if sfile-1 < len(ext) else '?'
                sc = scripts.get(spid) if (sfile!=0 and 'globalgamemanagers.assets' in tgt) or (fname=='globalgamemanagers.assets' and sfile==0) else None
                mb_rows.append(dict(file=fname, path_id=ob['path_id'], name=name, size=ob['size'], script_class=sc['class_name'] if sc else f'?{sfile}:{spid}', namespace=sc['namespace'] if sc else '', go_path_id=gopid))
            elif cid==1:
                nc=r.i32(); comps=[(r.i32(),r.i64()) for _ in range(nc)]
                r.u32(); name=r.string()
                go_rows.append(dict(file=fname,path_id=ob['path_id'],name=name,components=nc))
            elif cid==213:
                name=r.string()
                sprite_rows.append(dict(file=fname,path_id=ob['path_id'],name=name,size=ob['size']))
            elif cid==28:
                name=r.string(); r.i32(); r.u8(); r.u8(); r.align(4)
                # m_ForcedFallbackFormat i32 (v>=?), m_DownscaleFallback bool..., then width,height
                # parse conservatively: see layout below
                tex_rows.append(dict(file=fname,path_id=ob['path_id'],name=name,size=ob['size'],raw=r.b[r.p:r.p+48].hex()))
            elif cid in (21,48,128,74,91,308,83,43):
                name=r.string()
                misc_rows.append(dict(file=fname,path_id=ob['path_id'],cls=sf.CLASS_NAMES.get(cid,cid),name=name))
        except Exception as e:
            misc_rows.append(dict(file=fname,path_id=ob['path_id'],cls='ERR%d'%cid,name=str(e)))
def dump(n,rows):
    if rows:
        with open(f'out/{n}.csv','w',newline='') as f:
            w=csv.DictWriter(f,fieldnames=list(rows[0].keys())); w.writeheader(); w.writerows(rows)
for n,rows in [('monobehaviours',mb_rows),('gameobjects',go_rows),('sprites',sprite_rows),('textures_raw',tex_rows),('misc_objects',misc_rows)]: dump(n,rows); print(n,len(rows))
c=collections.Counter((r['file'],r['script_class']) for r in mb_rows)
print(c.most_common(40))
print([r['name'] for r in sprite_rows[:25]])
print(collections.Counter(r['cls'] for r in misc_rows))
