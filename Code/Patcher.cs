using System;
using HarmonyLib;
using UnityEngine;
using NCMS.Utils;
using ai;
using ai.behaviours;

namespace K_mod
{
    class Patcher
    {
        public static Patcher instance;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TooltipLibrary), "showResource")]
        [Obsolete]
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
        [HarmonyPatch(typeof(Actor), "checkSpriteToRender")]
        public static bool checkSpriteToRender(Actor __instance, ref Sprite __result)
        {
            if (__instance == null || __instance.data == null) { return false; }
            if (__instance.isFighting())
            {
                if (__instance.asset.id is "Ballista" or "Catapult")
                {
                    var random_list = SpriteTextureLoader.getSpriteList($"actors/{__instance.asset.id}/{__instance.asset.id}_Shoot");
                    __result = AnimationHelper.getSpriteFromList(__instance.GetHashCode(), random_list, 10f);
                    return false;
                }
            }
            if (__instance.asset.has_override_sprite)
            {
                Sprite sprite = __instance.asset.get_override_sprite(__instance);
                __instance.spriteRenderer.sprite = sprite;
                __result = sprite;
                return false;
            }
            __instance.checkAnimationContainer();
            __instance.checkSpriteHead();
            if (__instance.ai.action != null && __instance.ai.action.force_animation)
            {
                Sprite sprite2 = __instance.animationContainer.sprites[__instance.ai.action.force_animation_id];
                __instance.spriteRenderer.sprite = sprite2;
                __result = sprite2;
                return false;
            }
            Sprite sprite3;
            if (!__instance.isAlive() || __instance.has_status_frozen)
            {
                sprite3 = __instance.animationContainer.idle.frames[0];
            }
            else
            {
                float pAnimationSpeed = __instance.asset.animation_walk_speed;
                ActorAnimation actorAnimation;
                if (__instance.is_moving)
                {
                    if (__instance.animationContainer.has_swimming && __instance.isAffectedByLiquid())
                    {
                        actorAnimation = __instance.animationContainer.swimming;
                        pAnimationSpeed = __instance.asset.animation_swim_speed;
                    }
                    else
                    {
                        actorAnimation = __instance.animationContainer.walking;
                    }
                }
                else if (__instance.zPosition.y > 0f)
                {
                    actorAnimation = __instance.animationContainer.idle;
                }
                else if (__instance.animationContainer.has_swimming && __instance.isAffectedByLiquid())
                {
                    actorAnimation = __instance.animationContainer.swimming;
                    pAnimationSpeed = __instance.asset.animation_swim_speed;
                }
                else
                {
                    actorAnimation = __instance.animationContainer.idle;
                    pAnimationSpeed = __instance.asset.animation_idle_speed;
                }
                if (actorAnimation.frames.Length > 1)//报错位置
                {
                    sprite3 = AnimationHelper.getSpriteFromList(__instance.GetHashCode(), actorAnimation.frames, pAnimationSpeed);
                }
                else
                {
                    sprite3 = actorAnimation.frames[0];
                }
            }
            if (__instance._last_main_sprite != sprite3 || __instance._last_color_asset != __instance.kingdom.kingdomColor)
            {
                __instance.frame_data = __instance.animationContainer.dict_frame_data[sprite3.name];
                __instance._last_colored_sprite = UnitSpriteConstructor.getSpriteUnit(__instance.frame_data, sprite3, __instance, __instance.kingdom.kingdomColor, __instance.race, __instance.data.skin_set, __instance.data.skin, __instance.asset.texture_atlas);
                __instance._last_main_sprite = sprite3;
                __instance._last_color_asset = __instance.kingdom.kingdomColor;
                __instance.spriteRenderer.sprite = __instance._last_colored_sprite;
            }
            __result = __instance._last_colored_sprite;
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
                if (buildingAsset == null){continue;}
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
        [HarmonyPatch(typeof(CityBehCheckLeader), "checkFindLeader")]
        public static bool checkFindLeader(City pCity)
        {
            Actor pActor = pCity.leader;
            if (pActor != null)
            {
                if (pActor.asset.id is "Ballista" or "Catapult")
                {
                    pCity.removeLeader();
                }
            }
            return true;
        }


    }
}
