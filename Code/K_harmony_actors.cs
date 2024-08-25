using K_mod;
using HarmonyLib;
using System;
using UnityEngine;
using K_mod.Utils;
using ReflectionUtility;
using System.Collections.Generic;
using NCMS.Utils;
using ai.behaviours;
using ai;

namespace K_mod;
public class K_harmony_actors
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Actor), "checkSpriteToRender")]
    public static bool checkSpriteToRender(Actor __instance, ref Sprite __result)
    {
        if (__instance.Any() && K_actors.ActorAction.ContainsKey(__instance.asset.id))
        {
            K_action pAction = __instance.getAnimation();
            if (pAction != null && pAction.textures.Any())
            {
                __instance.checkAnimationContainer();
                __instance.checkSpriteHead();
                string texture = pAction.textures[0];
                if (__instance.isInLiquid())
                {
                    __result = Resources.Load<Sprite>(texture.Replace("walk_", "swim_"));
                    if (__result == null)
                    {
                        __result = Resources.Load<Sprite>(texture);
                    }
                }
                else { __result = Resources.Load<Sprite>(texture); }
                __instance._last_color_asset = __instance.kingdom.kingdomColor;
                __instance._last_colored_sprite = __result;
                __instance._last_main_sprite = __result;
                __instance.spriteRenderer.sprite = __result;
                return false;
            }

        }
        return true;
    }
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Actor), "tryToAttack")]
    public static bool tryToAttack(Actor __instance, BaseSimObject pTarget, bool pDoChecks, ref bool __result)
    {
        if (pTarget == null)
        {
            __result = false;
            return false;
        }
        if (!pTarget.isAlive())
        {
            __result = false;
            return false;
        }
        if (pDoChecks)
        {
            if (__instance.s_attackType == WeaponType.Melee && pTarget.zPosition.y > 0f)
            {
                return true;
            }
            if (__instance.isInLiquid() && !__instance.asset.oceanCreature)
            {
                return true;
            }
            if (!__instance.isAttackReady())
            {
                return true;
            }
            if (!__instance.isInAttackRange(pTarget))
            {
                return true;
            }
        }
        WorldTile pTile = World.world.GetTile((int)__instance.currentPosition.x, (int)__instance.currentPosition.y);
        string uid = __instance.asset.id;
        if (K_actors.AttackAction.ContainsKey(uid))
        {
            K_actors.AttackAction[uid](__instance, pTarget, pTile);
        }
        return true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(ActorBase), "checkAnimationContainer")]
    public static void checkAnimationContainer(ActorBase __instance)
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
            // AnimationTexture = "actors/" + actor.asset.texture_path;
            if (setAnimationContainer)
            {
                actor.animationContainer = ActorAnimationLoader.loadAnimationUnit(animationContainerPath, actor.asset);
            }
        }
        // }

        // }
        // actor.animationContainer = ActorAnimationLoader.loadAnimationUnit(AnimationTexture, actor.asset);
        // }
    }
    [HarmonyPostfix]
    [HarmonyPatch(typeof(Actor), "isInAttackRange")]
    public static void isInAttackRange(Actor __instance, BaseSimObject pObject, ref bool __result)
    {
        if (pObject.a.Any() && pObject.a.hasStatus("effect_cavalry"))
        {
            __result = Toolbox.DistVec3(__instance.currentPosition, pObject.currentPosition) < __instance.stats[S.range] + pObject.stats[S.size] + 10f;
        }
    }
    [HarmonyPrefix]
    [HarmonyPatch(typeof(BehGoToActorTarget), "execute")]
    public static bool execute(BehGoToActorTarget __instance, Actor pActor, ref BehResult __result)
    {
        WorldTile pTile = pActor.beh_actor_target.currentTile;
        string text = __instance.type;
        if (text != null)
        {
            if (!(text == "sameTile"))
            {
                if (text == "sameRegion")
                {
                    pTile = pActor.beh_actor_target.currentTile.region.tiles.GetRandom<WorldTile>();
                }
            }
            else
            {
                pTile = pActor.beh_actor_target.currentTile;
            }
        }
        if (pActor.goTo(pTile, __instance.pathOnWater, false) == ExecuteEvent.True)
        {
            float TDJL = Toolbox.DistTile(pTile, pActor.currentTile);
            if (pActor.hasStatus("effect_cavalry") && TDJL is > 4f)
            {
                pActor.addStatusEffect("charge", 3f);
            }
            __result = BehResult.Continue;
            return false;
        }
        pActor.ignoreTarget(pActor.beh_actor_target);
        __result = BehResult.Stop;
        return true;
    }
    [HarmonyPrefix]
    [HarmonyPatch(typeof(ActorBase), "checkSpriteHead")]
    public static bool checkSpriteHead(ActorBase __instance)
    {
        if (!__instance.dirty_sprite_head)
        {
            return true;
        }

        if (!__instance.asset.body_separate_part_head)
        {
            return true;
        }
        if (__instance.tryToLoadFunHead())
        {
            return true;
        }
        if (!__instance.asset.unit)
        {
            return true;
        }
        string text;
        string pPath;
        if (__instance.data.profession == UnitProfession.Warrior && !__instance.equipment.helmet.isEmpty() && __instance.asset.race == "Russia")
        {
            int i = Toolbox.randomInt(1, 5);
            text = $"head_warrior{i}";
            pPath = "actors/races/" + __instance.asset.race + "/heads_special";
            __instance.setHeadSprite(ActorAnimationLoader.getHeadSpecial(pPath, text));
            __instance.dirty_sprite_head = false;
            return false;
        }
        return true;
    }
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
    [HarmonyPatch(typeof(ActorBase), "updateStats")]

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
        if (__instance.a.Any() && __instance.a.equipment != null && ActorEquipment.getList(__instance.a.equipment) != null)
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
}