using System.Collections.Generic;
using NCMS.Utils;
using UnityEngine;

namespace K_mod.Utils
{
    class K_Tools : MonoBehaviour
    {
        public static City convertCityA = null;
        public static City convertCityB = null;
        public static City cityToBeExpanded = null;
        public static Culture cultureToBeExpanded = null;


        public static bool selectBuildingPower(string pPowerID)
        {
            BuildingAsset bAsset = AssetManager.buildings.get(TabManager.buildingid);
            if (bAsset == null) { return false; }
            int size = (bAsset.fundament.left + bAsset.fundament.right + 1) / 2;
            if (!AssetManager.brush_library.dict.ContainsKey($"sqr_{size}"))
            {
                BrushData bData = AssetManager.brush_library.clone($"sqr_{size}", "sqr_10");
                bData.size = size;
                bData.generate_action = delegate (BrushData pAsset)
                {
                    int size2 = pAsset.size;
                    new Vector2Int(size2 / 2, size2 / 2);
                    List<BrushPixelData> list2 = new();
                    for (int k = -size2; k <= size2; k++)
                    {
                        for (int l = -size2; l <= size2; l++)
                        {
                            list2.Add(new BrushPixelData(k, l, size2));
                        }
                    }
                    pAsset.pos = list2.ToArray();
                };
            }
            AssetManager.brush_library.post_init();
            AssetManager.powers.get(pPowerID).forceBrush = $"sqr_{size}";
            return false;
        }

        public static bool action_spawn_building(WorldTile pTile, GodPower pPower)
        {
            Building newBuilding = World.world.buildings.addBuilding(TabManager.buildingid, pTile, false, false, BuildPlacingType.New);
            if (newBuilding == null)
            {
                EffectsLibrary.spawnAtTile("fx_bad_place", pTile, 0.25f);
                return false;
            }
            if (newBuilding.asset.cityBuilding && pTile.zone.city != null)
            {
                pTile.zone.city.addBuilding(newBuilding);
                newBuilding.retake();
            }
            return true;
        }

        
        public static void action_give_item(WorldTile pTile = null, string pDropID = null)
        {
            if (EquipmentAdditionWindow.Launch_scope == "people")
            {
                action_give_people_item(pTile, pDropID);
            }
            if (EquipmentAdditionWindow.Launch_scope == "city")
            {
                action_give_city_item(pTile, pDropID);
            }
        }

        
        public static void action_give_city_item(WorldTile pTile, string pPower)
        {
            if (pTile.zone.city == null)
            {
                return;
            }
            var Units = World.world.units._container.getSimpleList();
            foreach (var pActor in Units)
            {
                if (pActor.city != null && pActor.city == pTile.zone.city)
                {

                    if (!pActor.asset.use_items)
                    {
                        continue;
                    }
                    foreach (KeyValuePair<string, ItemOption> kv in EquipmentAdditionWindow.itemAssets)
                    {
                        if (!kv.Value.active)
                        {
                            continue;
                        }
                        ActorEquipmentSlot slot = pActor.equipment.getSlot(kv.Value.asset.equipmentType);
                        ItemData data = ItemGenerator.generateItem(kv.Value.asset, kv.Value.material, World.world.mapStats.year, pActor.kingdom,pActor.getName(),1,pActor);
                        data.name = EquipmentAdditionWindow.itemNames[kv.Value.id.ToString()];//.inputField.text;
                        data.modifiers.Clear();
                        if (EquipmentAdditionWindow.itemModifiers.ContainsKey(kv.Value.id.ToString()))
                        {
                            foreach (ItemAsset modifier in EquipmentAdditionWindow.itemModifiers[kv.Value.id.ToString()])
                            {
                                // ItemGenerator.tryToAddMod(data, modifier);
                                data.modifiers.Add(modifier.id);
                            }
                        }
                        slot.setItem(data);
                    }
                    pActor.setStatsDirty();
                    pActor.startShake(0.3f, 0.1f, true, true);
                    pActor.startColorEffect(ActorColorEffect.White);
                }
            }
            Localization.AddOrSet("city_give_item_success", $"城市{pTile.zone.city.data.name} 装备添加成功");
            WorldTip.showNow("city_give_item_success", true, "top", 3f);
        }

        
        public static void action_give_people_item(WorldTile pTile = null, string pDropID = null)
        {
            MapBox.instance.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
            List<BaseSimObject> temp_objs = World.world.temp_map_objects;
            for (int i = 0; i < temp_objs.Count; i++)
            {
                Actor pActor = (Actor)temp_objs[i];

                if (!pActor.asset.use_items)
                {
                    continue;
                }
                bool gainedItem = false;
                foreach (KeyValuePair<string, ItemOption> kv in EquipmentAdditionWindow.itemAssets)
                {
                    if (!kv.Value.active)
                    {
                        continue;
                    }
                    ActorEquipmentSlot slot = pActor.equipment.getSlot(kv.Value.asset.equipmentType);
                    ItemData data = ItemGenerator.generateItem(kv.Value.asset, kv.Value.material, World.world.mapStats.year, pActor.kingdom,pActor.getName(),1,pActor);
                    data.name = EquipmentAdditionWindow.itemNames[kv.Value.id.ToString()];//.inputField.text;
                    data.modifiers.Clear();
                    if (EquipmentAdditionWindow.itemModifiers.ContainsKey(kv.Value.id.ToString()))
                    {
                        foreach (ItemAsset modifier in EquipmentAdditionWindow.itemModifiers[kv.Value.id.ToString()])
                        {
                            // ItemGenerator.tryToAddMod(data, modifier);
                            data.modifiers.Add(modifier.id);
                        }
                    }
                    slot.setItem(data);
                    gainedItem = true;
                }
                if (gainedItem)
                {
                    Localization.AddOrSet("give_item_success", $"生物{pActor.getName()} 装备添加成功");
                    WorldTip.showNow("give_item_success", true, "top", 3f);
                }
                pActor.setStatsDirty();
                pActor.startShake(0.3f, 0.1f, true, true);
                pActor.startColorEffect(ActorColorEffect.White);
            }
        }

        
        public static void action_give_Horse(WorldTile pTile = null, string pDropID = null)
        {
            MapBox.instance.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
            List<BaseSimObject> temp_objs = World.world.temp_map_objects;
            for (int i = 0; i < temp_objs.Count; i++)
            {
                Actor pActor = (Actor)temp_objs[i];

                if (!pActor.asset.unit)
                {
                    continue;
                }
                bool gainedHouse = false;
                if (pActor.asset.id == "unit_Pig")
                {
                    pActor.addStatusEffect("BigPig");
                    pActor.data.health += 100;
                    gainedHouse = true;
                }
                else if (pActor.asset.id == "unit_dwarf")
                {
                    pActor.addStatusEffect("rhino");
                    pActor.data.health += 250;
                    gainedHouse = true;
                }
                else
                {
                    pActor.addStatusEffect("effect_cavalry");
                    pActor.data.health += 100;
                    gainedHouse = true;
                    pActor.ai.setTask("mounted_troopers", true, false);
                }
                if (gainedHouse)
                {
                    Localization.AddOrSet("give_horse_success", $"{pActor.getName()} 马匹添加成功");
                    WorldTip.showNow("give_horse_success", true, "top", 2f);
                    pActor.setProfession(UnitProfession.Warrior, true);
                }
                pActor.setStatsDirty();
                pActor.startShake(0.3f, 0.1f, true, true);
                pActor.startColorEffect(ActorColorEffect.White);
            }
        }

    }
}
