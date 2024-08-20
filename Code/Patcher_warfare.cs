using System;
using System.Collections.Generic;
using HarmonyLib;
using ReflectionUtility;
using UnityEngine;
using NCMS.Utils;
using ai.behaviours;
using K_mod.Utils;

namespace K_mod
{
    class Patcher_warfare
    {
        public static Patcher_warfare instance;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ActorBase), "checkAnimationContainer")]
        public static void CheckAnimationContainerPostfix(ActorBase __instance)
        {
            Actor actor = Reflection.GetField(__instance.GetType(), __instance, "a") as Actor;

            if (actor == null || actor.data == null || actor.asset == null || actor.batch == null || !actor.asset.unit || !actor.isAlive())
                return;

            string pid = __instance.asset.id;
            string texturePath = actor.asset.texture_path;
            string animationContainerPath = "actors/" + texturePath;
            bool setAnimationContainer = false;

            Dictionary<string, string> unitToCavalryTexture = new()
            {
                { "unit_Rome", "actors/Rome_cavalry" },
                { "unit_Xia", "actors/cavalry" },
                { "unit_Arab", "actors/Arab_cavalry" },
                { "unit_orc", "actors/wolf_cavalry" },
                { "unit_human", "actors/human_cavalry" },
                { "unit_elf", "actors/elf_cavalry" }
            };

            if (!PowerButtons.GetToggleValue("texture Cavalry"))
            {
                if (actor.hasStatus("effect_cavalry"))
                {
                    setAnimationContainer = true;
                    animationContainerPath = "actors/other_cavalry";
                    string pPath = "actors/heads_nothing";
                    actor.checkHeadID();
                    actor.setHeadSprite(ActorAnimationLoader.getHead(pPath, 0));
                    actor.has_rendered_sprite_head = true;
                    actor.dirty_sprite_head = false;

                    if (unitToCavalryTexture.ContainsKey(actor.asset.id))
                    {
                        animationContainerPath = unitToCavalryTexture[actor.asset.id];
                    }
                }
                else if (actor.hasStatus("BigPig"))
                {
                    setAnimationContainer = true;
                    animationContainerPath = "actors/pig_cavalry";
                }
                else if (actor.hasStatus("rhino"))
                {
                    setAnimationContainer = true;
                    string pPath = "actors/heads_nothing";
                    actor.checkHeadID();
                    actor.setHeadSprite(ActorAnimationLoader.getHead(pPath, 0));
                    actor.has_rendered_sprite_head = true;
                    actor.dirty_sprite_head = false;
                    animationContainerPath = "actors/dwarf_cavalry";
                }
            }

            if (setAnimationContainer)
            {
                actor.animationContainer = ActorAnimationLoader.loadAnimationUnit(animationContainerPath, actor.asset);
            }
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CityBehGiveInventoryItem), "tryToGiveItem")]
        public static void TryToGiveItemPostfix(City pCity, List<ItemData> pItems)
        {
            if (pCity.race == null)
                return;

            // Check toggle value and specific race IDs
            if (!PowerButtons.GetToggleValue("texture Cavalry") &&
                !IsCavalryRace(pCity.race.id))
            {
                return;
            }

            if (pCity.data.storage.get(SR.gold) < 10)
                return;

            float cavalryChance = CalculateCavalryChance(pCity);

            if (pCity.countProfession(UnitProfession.Warrior) > 0)
            {
                Actor actor = pCity.professionsDict[UnitProfession.Warrior].GetRandom<Actor>();

                if (actor == null || !actor.isAlive())
                    return;

                // Check actor conditions and apply effects
                if (!actor.isKing() && !actor.isCityLeader() && !actor.hasStatus("effect_cavalry") && !actor.hasStatus("BigPig") &&
                    !actor.hasStatus("rhino") && Toolbox.randomChance(cavalryChance))
                {
                    ApplyActorEffect(actor, pCity.race.id, pItems, pCity);
                    return;
                }
            }
        }

        private static bool IsCavalryRace(string raceId)
        {
            HashSet<string> cavalryRaces = new()
            {
                "human", "orc", "elf", "dwarf", "Arab", "Pig", "Rome", "Xia"
            };
            return cavalryRaces.Contains(raceId);
        }

        private static float CalculateCavalryChance(City pCity)
        {
            float defaultChance = 0.005f;
            float enhancedChance = 0.09f;

            Culture culture = pCity.getCulture();
            if (culture == null || !IsCavalryRace(pCity.race.id))
                return 0f;
            if (culture.hasTech("AW_hufuqishe") || pCity.race.id == "Arab" || pCity.race.id == "orc")
            {
                if (pCity.race.id == "Pig")
                { return 0.03f; }
                if (pCity.race.id == "dwarf")
                { return 0.06f; }
                return enhancedChance;
            }




            return defaultChance;
        }

        private static void ApplyActorEffect(Actor actor, string raceId, List<ItemData> pItems, City pCity)
        {
            if (actor.asset.id == "unit_Pig")
            {
                actor.addStatusEffect("BigPig");
                actor.removeStatusEffect("effect_cavalry");
                actor.data.health += 100;
            }
            else if (actor.asset.id == "unit_dwarf")
            {
                actor.addStatusEffect("rhino");
                actor.removeStatusEffect("effect_cavalry");
                actor.data.health += 250;
                City.giveItem(actor, pItems, pCity); // Assuming pItems and pCity are accessible
            }
            else
            {
                actor.addStatusEffect("effect_cavalry");
                actor.data.health += 100;
                City.giveItem(actor, pItems, pCity); // Assuming pItems and pCity are accessible
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(City), "isGettingCapturedBy")]
        public static bool isGettingCapturedByPrefix(City __instance, Kingdom pKingdom, ref bool __result)
        {
            if (__instance != null && pKingdom != null)
            {
                __result = __instance._capturing_units.ContainsKey(pKingdom) && __instance._capturing_units[pKingdom] > 0;
            }
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ActorBase), "updateStats")]
        [Obsolete]
        public static void updateStats_ActorBase(ActorBase __instance)
        {
            if (__instance == null || __instance.asset == null || __instance.data == null || !__instance.isAlive())
            {
                return;
            }

            if (__instance.asset.id is "Ballista" or "Catapult")
            {
                __instance.stats[S.fertility] = 0f;
            }

            ApplyMountedSpeedBonus(__instance);

            if (PowerButtons.GetToggleValue("national_strength"))
            {
                if (__instance.data.profession == UnitProfession.Warrior)
                {
                    ApplyWarriorStrengthBonuses(__instance);
                }
                else if (__instance.isCityLeader())
                {
                    ApplyCityLeaderBonuses(__instance);
                }
            }

            __instance.stats.normalize();
            if (__instance.data.health > __instance.getMaxHealth())
            {
                __instance.data.health = __instance.getMaxHealth();
            }
        }

        [Obsolete]
        private static void ApplyMountedSpeedBonus(ActorBase actorBase)
        {
            if (Main.Rider_horse.ContainsKey((Actor)actorBase))
            {
                Actor horse = Main.Rider_horse[(Actor)actorBase];
                actorBase.stats[S.speed] += horse.stats[S.speed];
            }
        }

        private static void ApplyWarriorStrengthBonuses(ActorBase actorBase)
        {
            Actor groupLeader = actorBase.unit_group?.groupLeader;
            if (groupLeader == null || actorBase.unit_group.units.Count == 0)
            {
                return;
            }

            float warfare = groupLeader.stats[S.warfare] - 10;

            // 王国国王对战争的影响
            if (groupLeader.kingdom.king != null && groupLeader.kingdom.king.stats[S.warfare] > 10)
            {
                warfare += groupLeader.kingdom.king.stats[S.warfare] / 5;
            }

            // 将wafare限制在一定范围内
            if (groupLeader.stats[S.warfare] <= 10)
            {
                warfare = 0;
            }
            else if (groupLeader.stats[S.warfare] < 5)
            {
                warfare = groupLeader.stats[S.warfare] - 5;
            }

            if (warfare > 90)
            {
                warfare = 90;
            }

            // Apply bonuses based on warfare
            actorBase.stats[S.damage] += (int)(actorBase.stats[S.damage] * 0.07 * warfare);
            actorBase.stats[S.armor] += (int)(actorBase.stats[S.armor] * 0.03 * warfare);
            actorBase.stats[S.speed] += (int)(actorBase.stats[S.speed] * 0.02 * warfare);
            actorBase.stats[S.attack_speed] += (int)(actorBase.stats[S.attack_speed] * 0.02 * warfare);
            actorBase.kingdom.stats.bonus_max_unit_level.add(0.1f * warfare);
            actorBase.kingdom.stats.bonus_max_army.add(0.005f * warfare);
        }

        private static void ApplyCityLeaderBonuses(ActorBase actorBase)
        {
            float intelligence = actorBase.stats[S.intelligence];

            if (actorBase.hasTrait("防御"))
            {
                if (intelligence > 100)
                {
                    intelligence = 100;
                }
                actorBase.stats[S.bonus_towers] = intelligence / 10;
            }

            if (actorBase.hasTrait("军备"))
            {
                actorBase.city.status.maximumItems = (int)(intelligence / 20);
            }
        }
        [HarmonyPostfix]
        [HarmonyPatch(typeof(City), "updateAge")]
        [Obsolete]
        public static void updateAgeCity(City __instance)
        {
            if (__instance == null || __instance.leader == null)
            {
                return;
            }

            __instance.gold_in_tax = __instance.getPopulationTotal(true) / 2;
            __instance.gold_out_homeless = __instance.getPopulationTotal(true) - __instance.status.housingTotal;
            if (__instance.gold_out_homeless < 0)
            {
                __instance.gold_out_homeless = 0;
            }
            __instance.gold_out_army = __instance.countProfession(UnitProfession.Warrior) / 2;
            __instance.gold_out_buildings = __instance.buildings.Count / 2;

            // Calculate cavalry expenses
            CalculateCavalryExpenses(__instance);

            // Enhance national strength effect
            if (PowerButtons.GetToggleValue("national_strength"))
            {
                EnhanceNationalStrength(__instance);
            }

            __instance.updatePopPoints();
        }

        private static void CalculateCavalryExpenses(City city)
        {
            if (city.getArmy() > 0)
            {
                float cavalryCost = 0;

                foreach (Actor actor in city.professionsDict[UnitProfession.Warrior])
                {
                    if (actor.hasStatus("effect_cavalry"))
                    {
                        cavalryCost += CalculateCavalryCost(actor);
                    }
                    else if (actor.hasStatus("rhino") || actor.hasStatus("BigPig"))
                    {
                        cavalryCost += 3; //特定类型的成本
                    }
                }

                city.data.storage.change("gold", -(int)cavalryCost);
                city.data.set("horse", (int)cavalryCost);
                city.gold_change += (int)cavalryCost;
            }
        }

        private static float CalculateCavalryCost(Actor actor)
        {
            float baseCost = 2; //骑兵的默认成本
            if (actor.race.id is "orc" or "Arab")
            {
                baseCost -= 1; //降低特定race的成本
            }
            return baseCost;
        }

        private static void EnhanceNationalStrength(City city)
        {
            int initialTax = city.gold_in_tax;
            if (city.leader.hasTrait("经济") && city.leader.stats[S.stewardship] >= 15)
            {
                city.gold_in_tax = (int)(city.getPopulationTotal(true) * 0.7);
            }

            int taxDifference = city.gold_in_tax - initialTax;
            int totalGoldChange = city.gold_in_tax - city.gold_out_army - city.gold_out_buildings - city.gold_out_homeless;
            if (totalGoldChange < 0)
            {
                totalGoldChange = 0;
            }

            float stewardshipModifier = CalculateStewardshipModifier(city);

            float taxIncrease = (float)(totalGoldChange * 0.4 * stewardshipModifier) + taxDifference;

            city.gold_change += (int)taxIncrease;
            city.data.storage.change("gold", (int)taxIncrease);
            city.data.set("增税收", (int)taxIncrease);
        }

        private static float CalculateStewardshipModifier(City city)
        {
            float stewardship = city.leader.stats[S.stewardship];
            float modifier = stewardship - 5;
            if (stewardship <= 10)
            {
                stewardship += 2;
                if (stewardship > 5)
                {
                    modifier = 0;
                }
            }
            //限制（如有必要）
            // if (modifier > 200)
            // {
            //     modifier = 200;
            // }
            return modifier;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(Actor), "getHit")]
        public static void getHit(Actor __instance, float pDamage, bool pFlash = true, AttackType pAttackType = AttackType.Other, BaseSimObject pAttacker = null, bool pSkipIfShake = true, bool pMetallicWeapon = false)
        {
            // 统一空引用检查
            if (__instance == null || __instance.asset == null || pAttacker == null || pAttacker.a == null)
            {
                return;
            }

            // 跳过震动效果检查
            if (pSkipIfShake && __instance.shake_active)
            {
                return;
            }

            // 如果生命值小于等于0，直接返回
            if (__instance.data.health <= 0)
            {
                return;
            }

            // 处理防守方为骑兵情况
            if ((__instance.hasStatus("effect_cavalry") || __instance.hasStatus("rhino")) && !pAttacker.a.hasStatus("effect_cavalry") && !pAttacker.a.hasStatus("rhino"))
            {
                if (pAttacker.a.getWeapon() != null)
                {
                    string weapon = pAttacker.a.getWeapon().id;
                    if (weapon == "spear" || weapon == "hammer")
                    {
                        float armorReductionPercentage = 1f - __instance.stats[S.armor] / 100f;
                        pDamage *= armorReductionPercentage;
                        if (pDamage < 1f)
                        {
                            pDamage = 1f;
                        }
                        __instance.data.health -= (int)(pDamage * 0.5);
                    }
                }
            }
            // 处理攻击方为骑兵情况
            else if ((pAttacker.a.hasStatus("effect_cavalry") || pAttacker.a.hasStatus("rhino")) && !__instance.hasStatus("effect_cavalry") && !__instance.hasStatus("rhino"))
            {
                // pAttacker.a.addStatusEffect("ChargeCooling", 5f);
                if (__instance.getWeapon() != null)
                {
                    string weapon = __instance.getWeapon().id;
                    if (weapon == "spear" || weapon == "hammer" || weapon == "CrutchGun")
                    {
                        // 反伤效果
                        float armorReductionPercentage = 1f - pAttacker.stats[S.armor] / 100f;
                        pDamage *= armorReductionPercentage;
                        if (pDamage < 1f)
                        {
                            pDamage = 1f;
                        }
                        pAttacker.a.data.health -= (int)(pDamage * 0.2);
                        if (pFlash)
                        {
                            pAttacker.a.startColorEffect(ActorColorEffect.Red);
                        }
                        if (pAttacker.a.data.health <= 0)
                        {
                            Kingdom kingdom = pAttacker.a.kingdom;
                            if (__instance != null && pAttacker != __instance && __instance.isActor() && __instance.isAlive())
                            {
                                BattleKeeperManager.unitKilled(pAttacker.a);
                                __instance.newKillAction(pAttacker.a, kingdom);
                                if (__instance.city != null)
                                {
                                    bool isAnimal = __instance.asset.animal;
                                    bool isUnitWithSavageTrait = __instance.asset.unit && pAttacker.a.hasTrait("savage");

                                    if (isAnimal)
                                    {
                                        __instance.city.data.storage.change("meat", 1);
                                    }
                                    else if (isUnitWithSavageTrait)
                                    {
                                        // 处理特定条件
                                    }

                                    // 随机资源收集
                                    if (isAnimal || isUnitWithSavageTrait)
                                    {
                                        HandleRandomResourceGathering(__instance.city.data.storage);
                                    }
                                }
                            }
                            pAttacker.a.killHimself(false, pAttackType, true, true, true);
                            return;
                        }
                        if (pAttackType == AttackType.Weapon && !pAttacker.a.asset.immune_to_injuries && !pAttacker.a.hasStatus("shield"))
                        {
                            if (Toolbox.randomChance(0.02f))
                            {
                                pAttacker.a.addTrait("crippled", false);
                            }
                            if (Toolbox.randomChance(0.02f))
                            {
                                pAttacker.a.addTrait("eyepatch", false);
                            }
                        }
                        pAttacker.a.startShake(0.3f, 0.1f, true, true);
                    }
                }
            }

            // 处理防守方闪烁效果
            if (pFlash)
            {
                __instance.startColorEffect(ActorColorEffect.Red);
            }

            // 处理防守方死亡
            if (__instance.data.health <= 0)
            {
                Kingdom kingdom = __instance.kingdom;
                if (pAttacker != null && pAttacker != __instance && pAttacker.isActor() && pAttacker.isAlive())
                {
                    BattleKeeperManager.unitKilled(__instance);
                    pAttacker.a.newKillAction(__instance, kingdom);
                    if (pAttacker.city != null)
                    {
                        bool isAnimal = __instance.asset.animal;
                        bool isUnitWithSavageTrait = __instance.asset.unit && pAttacker.a.hasTrait("savage");

                        if (isAnimal)
                        {
                            pAttacker.city.data.storage.change("meat", 1);
                        }
                        else if (isUnitWithSavageTrait)
                        {
                            // 处理特定条件
                        }

                        // 随机资源收集
                        if (isAnimal || isUnitWithSavageTrait)
                        {
                            HandleRandomResourceGathering(pAttacker.city.data.storage);
                        }
                    }
                }
                __instance.killHimself(false, pAttackType, true, true, true);
                return;
            }

            // 处理防守方受伤
            if (pAttackType == AttackType.Weapon && !__instance.asset.immune_to_injuries && !__instance.hasStatus("shield"))
            {
                if (Toolbox.randomChance(0.02f))
                {
                    __instance.addTrait("crippled", false);
                }
                if (Toolbox.randomChance(0.02f))
                {
                    __instance.addTrait("eyepatch", false);
                }
            }

            // 开始震动效果
            __instance.startShake(0.3f, 0.1f, true, true);
        }

        // 处理随机资源收集的方法
        private static void HandleRandomResourceGathering(CityStorage storage)
        {
            if (Toolbox.randomChance(0.5f))
            {
                storage.change(SR.bones, 1);
            }
            else if (Toolbox.randomChance(0.5f))
            {
                storage.change(SR.leather, 1);
            }
            else if (Toolbox.randomChance(0.5f))
            {
                storage.change(SR.meat, 1);
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(BaseSimObject), "canAttackTarget")]
        public static void canAttackTarget(BaseSimObject pTarget, BaseSimObject __instance, ref bool __result)
        {
            if (__instance.a.Any()&&__instance.a.equipment != null && ActorEquipment.getList(__instance.a.equipment) != null)
            {
                var We = __instance.a.equipment.weapon;
                if (We.data != null)
                {
                    if (__instance.a.equipment.weapon.data.id == "CrutchGun")
                    {
                        __instance.a.data.get("Continuous_firing", out int pResult, 0);
                        if (pResult >= 4)
                        {
                            _ = __instance.a.data.set("Continuous_firing", 0);
                            __instance.addStatusEffect("filling", 10f);
                        }
                    }
                }
            }
            if (__instance.hasStatus("filling"))
            {
                __result = false;
            }
        }
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ActorBase), "nextJobActor")]
        public static bool nextJobActor_Postfix(ActorBase pActor, ref string __result)
        {
            if (pActor.hasStatus("ChargeCooling") || pActor.hasStatus("filling"))
            {
                Debug.Log("逃离");
                __result = "Strike retreat";
                return false;
            }
            return true;
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
