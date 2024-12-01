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
    class K_harmony_horse
    {

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Actor), "checkSpriteToRender")]
        public static void checkSpriteToRender(Actor __instance, ref Sprite __result)
        {
            if (Main.Rider.Contains(__instance))
            {
                Sprite sprite = __instance.animationContainer.idle.frames[0];
                if (__instance._last_main_sprite != sprite || __instance._last_color_asset != __instance.kingdom.kingdomColor)
                {
                    __instance.frame_data = __instance.animationContainer.dict_frame_data[sprite.name];
                    __instance._last_colored_sprite = UnitSpriteConstructor.getSpriteUnit(__instance.frame_data, sprite, __instance, __instance.kingdom.kingdomColor, __instance.race, __instance.data.skin_set, __instance.data.skin, __instance.asset.texture_atlas);
                    __instance._last_main_sprite = sprite;
                    __instance._last_color_asset = __instance.kingdom.kingdomColor;
                    __instance.spriteRenderer.sprite = __instance._last_colored_sprite;
                }
                __result = __instance._last_colored_sprite;
            }
        }
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ActorMove), "goTo")]
        public static bool goTo_ActorMove_Prefix(Actor actor, WorldTile target, ref ExecuteEvent __result, bool pPathOnLiquid = false, bool pWalkOnBlocks = false)
        {
            if (Main.Horse.Contains(actor) && Main.Horse_rider.ContainsKey(actor)
            && actor.hasTrait("驯服_Tame"))
            {
                __result = Main.goTo(actor, Main.Horse_rider[actor].tileTarget, pPathOnLiquid, pWalkOnBlocks);
                Main.Horse_rider[actor].setFlip(actor.flip);
                return false;
            }
            return true;
        }
        [HarmonyPostfix]
        [HarmonyPatch(typeof(ActorMove), "goTo")]
        public static void goTo_ActorMove_Postfix(Actor actor, WorldTile target, ref ExecuteEvent __result, bool pPathOnLiquid = false, bool pWalkOnBlocks = false)
        {
            if (Main.Rider.Contains(actor) && Main.Rider_horse.ContainsKey(actor)
            && Main.Rider_horse[actor].hasTrait("驯服_Tame"))
            {
                __result = Main.goTo(Main.Rider_horse[actor], target, pPathOnLiquid, pWalkOnBlocks);
                actor.setFlip(Main.Rider_horse[actor].flip);
            }
        }
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Actor), "updatePos")]
        public static void updatePos(Actor __instance)
        {
            if (Main.Rider.Contains(__instance) && Main.Rider_horse.ContainsKey(__instance))
            {
                Actor rider = __instance;
                Actor horse = Main.Rider_horse[rider];
                rider.setShowShadow(false);
                if (horse != null && rider != null && horse.data != null && rider.data != null
                && horse.data.alive && rider.data.alive && horse.isAlive() && rider.isAlive() && !rider.is_inside_building)
                {
                    horse.curTransformPosition = new Vector3(rider.curTransformPosition.x, rider.curTransformPosition.y - Main.Rider_z[rider]);
                    horse.transform.position = new Vector3(rider.transform.position.x, rider.transform.position.y - Main.Rider_z[rider]);
                    horse.currentPosition = rider.currentPosition;
                    horse.currentTile = rider.currentTile;
                }
                else { Main.Dismount_horse(rider); }
            }
            if (Main.Horse.Contains(__instance) && Main.Horse_rider.ContainsKey(__instance))
            {
                Actor horse = __instance;
                Actor rider = Main.Horse_rider[horse];
                rider.setShowShadow(false);
                if (horse != null && rider != null && horse.data != null && rider.data != null
                && horse.data.alive && rider.data.alive && horse.isAlive() && rider.isAlive() && !rider.is_inside_building)
                {
                    horse.curTransformPosition = new Vector3(rider.curTransformPosition.x, rider.curTransformPosition.y - Main.Rider_z[rider]);
                    horse.transform.position = new Vector3(rider.transform.position.x, rider.transform.position.y - Main.Rider_z[rider]);
                    horse.currentPosition = rider.currentPosition;
                    horse.currentTile = rider.currentTile;
                }
                else { Main.Dismount_rider(horse); }
            }
        }



    }
}
