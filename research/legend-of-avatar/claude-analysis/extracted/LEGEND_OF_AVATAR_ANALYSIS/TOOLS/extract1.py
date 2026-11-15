import sf, csv, json, os, collections
os.makedirs('out', exist_ok=True)
b,o = sf.parse('vfs/globalgamemanagers.assets')
rows=[]
for ob in o['objects']:
    if ob['class_id']!=115: continue
    r=sf.R(b, ob['offset'], be=False)
    try:
        name=r.string(); order=r.i32(); r.bytes(16)
        cls=r.string(); ns=r.string(); asm=r.string()
        rows.append(dict(path_id=ob['path_id'], name=name, class_name=cls, namespace=ns, assembly=asm))
    except Exception as e:
        rows.append(dict(path_id=ob['path_id'], name='ERR', class_name=str(e), namespace='', assembly=''))
print(len(rows)); 
bad=[r for r in rows if r['name']=='ERR']; print('bad',len(bad))
asms=collections.Counter(r['assembly'] for r in rows); print(len(asms), asms.most_common(8))
ac=[r for r in rows if r['assembly'].startswith('Assembly-CSharp')]
print('Assembly-CSharp classes', len(ac))
for r in rows[:5]: print(r)
with open('out/monoscripts.csv','w',newline='') as f:
    w=csv.DictWriter(f,fieldnames=['path_id','name','class_name','namespace','assembly']); w.writeheader(); w.writerows(rows)
