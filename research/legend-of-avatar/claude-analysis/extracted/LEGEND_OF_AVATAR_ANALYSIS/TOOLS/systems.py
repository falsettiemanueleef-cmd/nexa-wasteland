import csv, re, json, collections, os
rows=[r for r in csv.DictReader(open('out/monoscripts.csv')) if r['assembly'].startswith('Assembly-CSharp')]
SYS={
 'BATTLE':r'^(Battle|Skill|Action|Combat|Spawn|Stage|Monster|Boss|Bot|Actor|Unit|Damage|Buff|PlayerBuff|AdBuff|TimeAccel|Formation|Replay)',
 'CHARACTER_POLYMORPH':r'^(Character|Polymorph|Spirit|Pet|Creature|Ability|Specialize|Vision)',
 'SUMMON_GACHA':r'^(Gacha|Summon|Shop(Summon)|Pick)',
 'EQUIPMENT':r'^(ItemGear|UpgradableGear|Emblem|Essentials|Blessing|Vessel|Relic|Rune|Wing|Accessory|Halidom|SetBonus|StatOption|Item|Guardian|Synthesis|Craft|Alchemy|Enhance|Upgradable)',
 'PROGRESSION':r'^(Growth|Chapter|Dungeon|Infinity|Pantheon|Tower|Quest|Attendance|Collection|Tutorial|Offline|AutoBattle|Reward|Stage|Season|World|Cargo|Garden|Deco)',
 'GUILD_PVP':r'^(Guild|Arena|Pvp|PVP|Ranking|Chat|Friend)',
 'EVENT':r'^(Event|Mini|Goblin|Roulette)',
 'ECONOMY_SHOP':r'^(Shop|Benefit|Vip|Ad|Iap|IAP|Purchase|Product|ReqCost|Currency|Goods|BattlePass|QuestPass|Post)',
 'TOOLING_DEBUG':r'^(Cheat|K3|Kz|Editor)',
}
def sysof(n):
    for k,p in SYS.items():
        if re.match(p,n): return k
    return 'OTHER'
out=collections.defaultdict(list)
for r in rows: out[sysof(r['class_name'])].append(r)
os.makedirs('out/systems',exist_ok=True)
for k,v in out.items():
    with open(f'out/systems/{k}.csv','w',newline='') as f:
        w=csv.writer(f); w.writerow(['class_name','namespace','path_id'])
        for r in sorted(v,key=lambda r:r['class_name']): w.writerow([r['class_name'],r['namespace'],r['path_id']])
print({k:len(v) for k,v in out.items()})
ui=re.compile(r'(Popup|Slot|Card|Button|Page|View|Pop|UI|Item|Message|Tab|Toggle|Cell|Panel)$')
for k in ['BATTLE','SUMMON_GACHA','CHARACTER_POLYMORPH']:
    names=sorted(r['class_name'] for r in out[k] if not ui.search(r['class_name']))
    print('\n##',k,len(names)); print(' '.join(names[:70]))
