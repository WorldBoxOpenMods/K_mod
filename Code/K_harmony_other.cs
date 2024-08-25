using System;
using HarmonyLib;
using UnityEngine;
using NCMS.Utils;
using ai;
using ai.behaviours;
using K_mod.Utils;
using System.Collections.Generic;

namespace K_mod
{
    class K_harmony_other
    {
        public static K_harmony_other instance;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TooltipLibrary), "showResource")]

        public static void showResource(Tooltip pTooltip, string pType, TooltipData pData = default(TooltipData))
        {
            ResourceAsset resource = pData.resource;
            City selectedCity = pData.city ?? Config.selectedCity;
            pTooltip.name.text = LocalizedTextManager.getText(resource.id, null);
            if (resource.id == SR.gold)
            {
                selectedCity.data.get("增税收", out int 增税收, 0);
                if (增税收 != 0 && PowerButtons.GetToggleValue("national_strength"))
                {
                    pTooltip.addItemText("upkeep_增税收", 增税收);
                }
                selectedCity.data.get("horse", out int horse, 0);
                if (horse != 0)
                {
                    pTooltip.addItemText("upkeep_骑兵", -horse);
                }
            }
        }
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ActorTool), "findNewBuildingTarget")]
        public static bool findNewBuildingTarget(Actor pActor, string pType, ref Building __result)
        {
            if (pType == "Market")
            {
                if (pActor.city.hasBuildingType("Market", true))
                {
                    Building buildingType = pActor.city.getBuildingType("Market", true, false);
                    if (buildingType.currentTile.isSameIsland(pActor.currentTile))
                    {
                        ActorTool.possible_buildings.Add(buildingType);
                    }
                }
            }
            else
            {
                return true;
            }

            if (ActorTool.possible_buildings.Count == 0)
            {
                __result = null;
                return false;
            }
            Building random = ActorTool.possible_buildings.GetRandom<Building>();
            __result = random;
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(CityBehBuild), "calcPossibleBuildings")]
        public static bool calcPossibleBuildings(City pCity)
        {
            if (pCity == null)
            {
                return false;
            }
            if (pCity.race == null)
            {
                return false;
            }
            foreach (BuildOrder buildOrder in AssetManager.race_build_orders.get(pCity.race.build_order_id).list)
            {
                if (buildOrder == null)
                {
                    break;
                }

                BuildingAsset buildingAsset = buildOrder.getBuildingAsset(pCity, null);
                if (buildingAsset == null) { continue; }
                if ((buildingAsset.id == "Catapultfactory" || buildingAsset.id == "Ballistafactory") && pCity.race.id != "Arab" && pCity.race.id != "Rome" && pCity.race.id != "Xia" && pCity.race.id != "Russia")
                {
                    continue;
                }

                if (buildingAsset.id == "Arab_Market" && pCity.race.id != "Arab")
                {
                    continue;
                }
                if (!CityBehBuild.canUseBuildAsset(buildOrder, pCity))
                {
                    continue;
                }
                if (!CityBehBuild.hasResourcesForBuildAsset(buildOrder, pCity))
                {
                    CityBehBuild._possible_buildings_no_resources.Add(buildOrder);
                    continue;
                }



                CityBehBuild._possible_buildings.Add(buildOrder);


            }

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(BuildOrder), "getBuildingAsset")]
        public static bool getBuildingAsset_Prefix(BuildingAsset __instance, ref BuildingAsset __result, City pCity, string pBuildingID = null)
        {
            if (string.IsNullOrEmpty(pBuildingID))
            {
                pBuildingID = __instance.id;
            }
            if (pCity.race.building_order_keys.ContainsKey(pBuildingID))
            {
                string pID = pCity.race.building_order_keys[pBuildingID];
                __result = AssetManager.buildings.get(pID);
                return true;
            }
            else
            {
                Race race = AssetManager.raceLibrary.get(S.human);
                string pID = race.building_order_keys[pBuildingID];

                __result = AssetManager.buildings.get(pID);
                return false;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(UnitSpawner), "update")]
        public static bool update_UnitSpawner(UnitSpawner __instance)
        {
            if (__instance.building.isAbandoned() || __instance.building.isRuin())
            {
                return false;
            }

            if (__instance.building.kingdom != null && __instance.building.kingdom.isCiv() && __instance.building.city != null)
            {
                string buildingType = __instance.building.asset.type;
                Culture culture = __instance.building.city.getCulture();

                // 设置默认值
                int unitsLimit = 1;
                float spawnInterval = 100f;

                // 根据建筑类型和文化科技设置单位限制和生成间隔
                switch (buildingType)
                {
                    case "Catapultfactory":
                        if (culture != null)
                        {
                            if (culture.hasTech("Catapultfactory_tec1"))
                            {
                                unitsLimit = 2;
                                spawnInterval = 200f;
                            }
                            // else if (culture.hasTech("Catapultfactory_tec2"))
                            // {
                            //     unitsLimit = 3;
                            //     spawnInterval = 300f;
                            // }
                        }
                        break;

                    case "Ballistafactory":
                        if (culture != null)
                        {
                            if (culture.hasTech("Ballistafactory_tec1"))
                            {
                                unitsLimit = 2;
                                spawnInterval = 200f;
                            }
                            // else if (culture.hasTech("Ballistafactory_tec2"))
                            // {
                            //     unitsLimit = 3;
                            //     spawnInterval = 300f;
                            // }
                        }
                        break;

                    default:
                        break;
                }

                // 应用设置
                __instance.units_limit = unitsLimit;
                __instance.spawnInterval = spawnInterval;

                return true;
            }

            return true;
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(Kingdom), "setKing")]
        public static void setKing(Kingdom __instance, Actor pActor)
        {
            if (pActor != null)
            {
                if (pActor.asset.id is "Ballista" or "Catapult")
                {
                    pActor.kingdom.removeKing();
                    return;
                }
            }
            return;
        }
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Projectile), "destroy")]
        public static bool destroy_Prefix_Projectile(Projectile __instance)
        {
            BaseSimObject byWho = __instance.byWho;
            WorldTile tile = World.world.GetTile((int)__instance.m_transform.position.x, (int)__instance.m_transform.position.y);
            if (byWho != null && byWho.isAlive() && tile != null)
            {
                int range = (int)byWho.stats[S.area_of_effect];
                if (__instance.IsID("stone"))
                {
                    float damage = byWho.stats[S.damage];
                    List<BaseSimObject> objs = new();
                    if (__instance.stats[S.damage] > byWho.stats[S.damage])
                    {
                        damage += __instance.stats[S.damage] - byWho.stats[S.damage];
                    }
                    List<WorldTile> tiles = new() { tile };
                    List<WorldTile> all_tiles = new() { tile };
                    for (int i = 0; i < range.Min(2); i++)
                    {
                        List<WorldTile> new_tiles = new();
                        foreach (WorldTile tile2 in tiles)
                        {
                            foreach (WorldTile tile3 in tile2.neighboursAll)
                            {
                                if (!all_tiles.Contains(tile3))
                                {
                                    new_tiles.Add(tile3);
                                    all_tiles.Add(tile3);
                                }
                            }
                            Building b = tile2.building;
                            if (b != null && b.isAlive() && byWho.kingdom.isEnemy(b.kingdom) && !objs.Contains(b))
                            {
                                objs.Add(b);
                            }
                            foreach (Actor a in tile2._units)
                            {
                                if (a.Any() && byWho.kingdom.isEnemy(a.kingdom) && !objs.Contains(a))
                                {
                                    objs.Add(a);
                                }
                            }
                        }
                        tiles = new_tiles;
                    }
                    Main.PVZRangeDamage(tile, damage, range, AttackType.Fire, byWho, delegate (BaseSimObject pTarget, WorldTile pTile)
                    {
                        objs.Remove(pTarget);
                        return true;
                    });
                    foreach (BaseSimObject obj in objs)
                    {
                        obj.getHit(damage, true, AttackType.Weapon, byWho, false);
                    }
                }
            }
            return true;
        }



    }
}
