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
        if (__instance.Any() && k_actors.ActorAction.ContainsKey(__instance.asset.id))
        {
            k_action pAction = __instance.getAnimation();
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
        if (k_actors.AttackAction.ContainsKey(uid))
        {
            k_actors.AttackAction[uid](__instance, pTarget, pTile);
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
            return;
        }
        __result = Toolbox.DistVec3(__instance.currentPosition, pObject.currentPosition) < __instance.stats[S.range] + pObject.stats[S.size];
    }
    [HarmonyPrefix]
    [HarmonyPatch(typeof(BehGoToActorTarget), "execute")]
    public static bool execute(BehGoToActorTarget __instance,Actor pActor, ref BehResult __result)
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
            if (pActor.hasStatus("effect_cavalry")&&TDJL is > 4f)
            {
                pActor.addStatusEffect("charge",3f);
            }
            __result = BehResult.Continue;
            return false;
        }
        pActor.ignoreTarget(pActor.beh_actor_target);
        __result = BehResult.Stop;
        return true;
    }
}