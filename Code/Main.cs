using System.IO;
using System.Collections.Generic;
using NCMS;
using NCMS.Utils;
using UnityEngine;
using ReflectionUtility;
using HarmonyLib;
using Newtonsoft.Json;
using System.IO.Compression;
using ai;
using EpPathFinding.cs;
using K_mod.Utils;
using UnityEngine.Events;

namespace K_mod
{
    [ModEntry]

    class Main : MonoBehaviour
    {
        public static bool KmodLoad = false;
        public K_actors moreActors = new();
        public RacesLibrary RacesLibrary = new();
        public MoreKingdoms moreKingdoms = new();
        public K_buildings moreBuildings = new();
        public k_buildingsLibrary buildingLibrary = new();
        public static List<Actor> Rider = new();
        public static List<Actor> Horse = new();
        public static Dictionary<string, string> Friendlybonds = new() { { "orc", "wolf" }, { "dwarf", "rhino" } };
        public static Dictionary<string, UnityAction<Actor, K_action>> KActions = new();
        public static Dictionary<Actor, List<K_action>> Actor_Action = new();
        public static List<K_action> k_actions = new();
        public static Dictionary<Actor, float> Rider_z = new();
        public static Dictionary<Actor, float> Rider_x = new();
        public static Dictionary<Actor, Actor> Rider_horse = new();
        public static Dictionary<Actor, Actor> Horse_rider = new();
        public static Main instance;
        public const string mainPath = "Mods/罗马mod";
        public List<string> addRaces = new(){
            "Rome","Arab","Russia"
            };


        void Awake()
        {
            Dictionary<string, ScrollWindow> allWindows = (Dictionary<string, ScrollWindow>)Reflection.GetField(typeof(ScrollWindow), null, "allWindows");
            Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "inspect_unit");
            allWindows["inspect_unit"].gameObject.SetActive(false);
            Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "village");
            allWindows["village"].gameObject.SetActive(false);
            Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "debug");
            allWindows["debug"].gameObject.SetActive(false);
            Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "kingdom");
            allWindows["kingdom"].gameObject.SetActive(false);
            Reflection.CallStaticMethod(typeof(BannerGenerator), "loadTexturesFromResources", "Rome");

            //建筑
            moreBuildings.init();
            buildingLibrary.init();
            //种族
            moreActors.init();
            RacesLibrary.init();
            moreKingdoms.init();
            cultureTech.init();
            //效果
            K_effects.init();
            //武器和投掷物
            projectile.init();
            K_items.Rome.init();
            K_items.Others.init();
            //其他
            K_Job.init();
            trait_group.init();
            Traits.init();
            //ui
            NewGodPowers.init();
            TabManager.init();
            NewUI.init();
            WindowManager.init();

            K_action.init();
            translation.init();

            Harmony.CreateAndPatchAll(typeof(Main));

            Harmony.CreateAndPatchAll(typeof(K_harmony_city));
            Harmony.CreateAndPatchAll(typeof(K_harmony_other));
            Harmony.CreateAndPatchAll(typeof(K_harmony_actors));

        }
        public void Update()
        {
            if (!Config.gameLoaded)
            {
                Rider.Clear();
                Horse.Clear();
                Rider_z.Clear();
                Rider_horse.Clear();
                Horse_rider.Clear();
                return;
            }

            bool paused = World.world.isPaused();
            K_update.update_actions(paused);
            K_update.update_horse(paused);
        }

        public static K_action NewAction(Actor a, KActionSave pSave)
        {
            if (!a.Any()) { return null; }
            K_action pAction = NewAction(pSave.id, a, pSave.interval, pSave.intervals, pSave.time, pSave.paused_);
            for (int n = 0; n < pSave.statsId.Count; n++)
            {
                pAction.stats[pSave.statsId[n]] = pSave.statsValue[n];
            }
            pAction.for_ = pSave.for_;
            pAction.animation = pSave.animation;
            pAction.textures.AddRange(pSave.textures);
            return pAction;
        }
        public static K_action NewAction(string id, Actor a, float interval = 100f, float intervals = 100f, float time = -1f, bool paused = true)
        {
            if (!a.Any()) { return null; }
            List<K_action> actions = k_actions;
            bool flag = Actor_Action.ContainsKey(a);
            if (flag)
            {
                actions = Actor_Action[a];
            }
            float world_time = (float)World.world.getCurWorldTime();
            bool flag2 = interval != float.MaxValue;
            bool flag3 = time >= 0f && time != float.MaxValue;
            for (int i = 0; i < actions.Count; i++)
            {
                K_action action = actions[i];
                if (!action.destroy && action.id == id && action.a == a)
                {
                    action.intervals = intervals;
                    action.paused_ = paused;
                    if (flag2)
                    {
                        action.interval = world_time + interval;
                    }
                    else
                    {
                        action.interval = interval;
                    }
                    if (flag3)
                    {
                        action.time = world_time + time;
                    }
                    else
                    {
                        action.time = time;
                    }
                    return action;
                }
            }
            K_action New = new()
            {
                a = a,
                id = id,
                paused_ = paused,
                intervals = intervals
            };
            if (flag2)
            {
                New.interval = world_time + interval;
            }
            else
            {
                New.interval = interval;
            }
            if (flag3)
            {
                New.time = world_time + time;
            }
            else
            {
                New.time = time;
            }
            k_actions.Add(New);
            if (flag)
            {
                Actor_Action[a].Add(New);
            }
            else { Actor_Action.Add(a, new List<K_action> { New }); }
            a.setStatsDirty();
            return New;
        }
        public static K_action NewAnimation(string id, Actor a, float pSpeed, List<string> textures, bool paused = true)
        {
            if (!a.Any()) { return null; }
            K_action New = NewAction(id, a, pSpeed, pSpeed, -1f, paused);
            New.textures = textures;
            New.animation = true;
            return New;
        }
        public static List<string> NewTextures(string texture, int num, int num2 = 1)
        {
            List<string> New = new();
            for (int n = 0; n < num2; n++)
            {
                for (int i = 0; i < num; i++)
                {
                    New.Add(texture + "/walk_" + i);
                    Debug.Log($"{texture} + /walk_{i}");
                }
            }
            return New;
        }
        public static void Mount_horse(Actor rider, Actor horse, float Zts = 0.6f)
        {
            if (horse != null && rider != null && horse.data != null && rider.data != null && (horse != rider)
            && horse.data.alive && rider.data.alive && horse.isAlive() && rider.isAlive() && (rider != horse))
            {
                if (Horse_rider.ContainsKey(horse) || Horse.Contains(horse)) { Dismount_rider(horse); }
                if (Rider_horse.ContainsKey(rider) || Rider_z.ContainsKey(rider)
                || Rider.Contains(rider)) { Dismount_horse(rider); }
                if (Horse_rider.ContainsKey(rider) || Horse.Contains(rider)) { Dismount_rider(rider); }
                if (Rider_horse.ContainsKey(horse) || Rider_z.ContainsKey(horse)
                || Rider.Contains(horse)) { Dismount_horse(horse); }
                Rider.Add(rider);
                Horse.Add(horse);
                Rider_z.Add(rider, Zts);
                Rider_horse.Add(rider, horse);
                Horse_rider.Add(horse, rider);
                rider.curTransformPosition = new Vector3(horse.curTransformPosition.x, horse.curTransformPosition.y + Zts);
                rider.transform.position = new Vector3(horse.transform.position.x, horse.transform.position.y + Zts);
                rider.currentPosition = horse.currentPosition;
                rider.currentTile = horse.currentTile;
                rider.setShowShadow(false);
            }
        }
        public static void Dismount_horse(Actor rider)
        {
            if (rider == null) { return; }
            _ = Rider.Remove(rider);
            _ = Rider_z.Remove(rider);
            if (Rider_horse.ContainsKey(rider))
            {
                Actor horse = Rider_horse[rider];
                rider.transform.position = horse.transform.position;
                rider.currentPosition = horse.currentPosition;
                rider.currentTile = horse.currentTile;
                _ = Horse_rider.Remove(horse);
                _ = Horse.Remove(horse);
            }
            _ = Rider_horse.Remove(rider);
            if (rider != null && rider.asset != null)
            {
                rider.setShowShadow(rider.asset.shadow);
            }
        }
        public static void Dismount_rider(Actor horse)
        {
            if (horse == null) { return; }
            _ = Horse.Remove(horse);
            if (Horse_rider.ContainsKey(horse))
            {
                Actor rider = Horse_rider[horse];
                rider.transform.position = horse.transform.position;
                rider.currentPosition = horse.currentPosition;
                rider.currentTile = horse.currentTile;
                if (rider != null && rider.asset != null)
                {
                    rider.setShowShadow(rider.asset.shadow);
                }
                _ = Rider_horse.Remove(rider);
                _ = Rider.Remove(rider);
            }
            _ = Horse_rider.Remove(horse);
        }
        public static bool action_Dismount(WorldTile pTile, GodPower pPower)
        {
            if (pTile == null) { return false; }
            List<Actor> RAH = new();
            for (int j = 0; j < pTile._units.Count; j++)
            {
                Actor actor = pTile._units[j];
                if (actor != null && actor.data != null && actor.data.alive && !Horse.Contains(actor))
                {
                    if (!RAH.Contains(actor)) { RAH.Add(actor); }

                }
            }
            foreach (Actor a in RAH)
            {
                if (a != null && a.data != null && a.data.alive && a.isAlive())
                {
                    if (Rider_horse.ContainsKey(a)) { Dismount_horse(a); }
                    if (Horse_rider.ContainsKey(a)) { Dismount_rider(a); }
                }
            }
            return true;
        }

        public static List<BaseSimObject> getObjectsInChunks(WorldTile pTile, int pRadius = 3, MapObjectType pObjectType = MapObjectType.All)
        {
            List<BaseSimObject> temp_map_objects = new();
            temp_map_objects = checkChunk(temp_map_objects, pTile.chunk, pTile, pRadius, pObjectType);
            foreach (MapChunk MC in pTile.chunk.neighbours)
            {
                temp_map_objects = checkChunk(temp_map_objects, MC, pTile, pRadius, pObjectType);
            }
            return temp_map_objects;
        }
        public static List<BaseSimObject> checkChunk(List<BaseSimObject> temp_map_objects, MapChunk pChunk, WorldTile pTile, int pRadius, MapObjectType pObjectType = MapObjectType.All)
        {
            for (int i = 0; i < pChunk.k_list_objects.Count; i++)
            {
                Kingdom key = pChunk.k_list_objects[i];
                List<BaseSimObject> list = pChunk.k_dict_objects[key];
                for (int j = 0; j < list.Count; j++)
                {
                    BaseSimObject baseSimObject = list[j];
                    if (!(baseSimObject == null) && baseSimObject.base_data.alive && !baseSimObject.object_destroyed)
                    {
                        if (baseSimObject.isActor())
                        {
                            if (pObjectType == MapObjectType.Building)
                            {
                                goto IL_84;
                            }
                        }
                        else if (pObjectType == MapObjectType.Actor)
                        {
                            goto IL_84;
                        }
                        if (pRadius == 0 || DistTile(baseSimObject.currentTile, pTile) <= pRadius)
                        {
                            temp_map_objects.Add(baseSimObject);
                        }
                    }
                IL_84:;
                }
            }
            return temp_map_objects;
        }
        public static float DistTile(WorldTile pT1, WorldTile pT2)
        {
            return Mathf.Sqrt((pT1.x - (float)pT2.x) * (pT1.x - (float)pT2.x) + (pT1.y - (float)pT2.y) * (pT1.y - (float)pT2.y));
        }

        public static ExecuteEvent goTo(Actor actor, WorldTile target, bool pPathOnLiquid = false, bool pWalkOnBlocks = false)
        {
            if (target == null) { return ExecuteEvent.False; }
            actor.setTileTarget(target);
            bool flag = false;
            actor.clearOldPath();
            if (!DebugConfig.isOn(DebugOption.SystemUnitPathfinding))
            {
                actor.current_path.Add(target);
                return ExecuteEvent.True;
            }
            if (actor.asset.isBoat && !target.isGoodForBoat())
            {
                return ExecuteEvent.False;
            }
            if (flag)
            {
                actor.current_path.Add(target);
                return ExecuteEvent.True;
            }
            if (actor.isFlying())
            {
                actor.current_path.Add(target);
                return ExecuteEvent.True;
            }
            bool flag2 = actor.currentTile.isSameIsland(target);
            if (flag2 && !actor.isInLiquid())
            {
                pPathOnLiquid = false;
            }
            World.world.pathfindingParam.resetParam();
            World.world.pathfindingParam.block = pWalkOnBlocks;
            World.world.pathfindingParam.lava = !actor.asset.dieInLava;
            if (actor.currentTile.Type.lava)
            {
                World.world.pathfindingParam.lava = true;
            }
            World.world.pathfindingParam.ocean = actor.asset.oceanCreature;
            if (pPathOnLiquid && !actor.asset.damagedByOcean)
            {
                World.world.pathfindingParam.ocean = true;
            }
            else if (actor.isInLiquid())
            {
                World.world.pathfindingParam.ocean = true;
            }
            World.world.pathfindingParam.ground = actor.asset.landCreature;
            World.world.pathfindingParam.boat = actor.asset.isBoat && actor.currentTile.isGoodForBoat();
            World.world.pathfindingParam.limit = true;
            if (!PathfinderTools.tryToGetSimplePath(actor.currentTile, target, actor.current_path, actor.asset, World.world.pathfindingParam))
            {
                actor.current_path.Clear();
            }
            World.world.pathFindingVisualiser.showPath(null, actor.current_path);
            if (actor.isUsingPath())
            {
                actor.setTileTarget(target);
                return ExecuteEvent.True;
            }
            if (!flag2)
            {
                if ((!target.Type.ground || !World.world.pathfindingParam.ground) && (!target.Type.ocean || !World.world.pathfindingParam.ocean) && (!target.Type.lava || !World.world.pathfindingParam.lava) && (!target.Type.block || !World.world.pathfindingParam.block))
                {
                    return ExecuteEvent.False;
                }
                if (target.region.island.getTileCount() < actor.currentTile.region.island.getTileCount())
                {
                    if (!target.region.island.isConnectedWith(actor.currentTile.region.island))
                    {
                        return ExecuteEvent.False;
                    }
                    World.world.pathfindingParam.endToStartPath = true;
                }
                else if (!actor.currentTile.region.island.isConnectedWith(target.region.island))
                {
                    return ExecuteEvent.False;
                }
            }
            bool flag3 = DebugConfig.isOn(DebugOption.UseGlobalPathLock);
            if (flag3)
            {
                if (actor.asset.isBoat)
                {
                    flag3 = true;
                }
                else if (!flag2)
                {
                    flag3 = false;
                }
            }
            if (flag3)
            {
                PathFinderResult globalPath = World.world.regionPathFinder.getGlobalPath(actor.currentTile, target, actor.asset.isBoat);
                if (globalPath == PathFinderResult.SamePlace)
                {
                    actor.current_path.Add(target);
                    return ExecuteEvent.True;
                }
                if (globalPath == PathFinderResult.NotFound)
                {
                    return ExecuteEvent.True;
                }
                if (globalPath == PathFinderResult.DifferentIslands)
                {
                    return ExecuteEvent.True;
                }
                if (World.world.regionPathFinder.last_globalPath != null)
                {
                    actor.current_path_global = World.world.regionPathFinder.last_globalPath;
                    int num = 0;
                    for (int i = 0; i < actor.current_path_global.Count; i++)
                    {
                        MapRegion mapRegion = actor.current_path_global[i];
                        mapRegion.usedByPathLock = true;
                        mapRegion.regionPathID = num++;
                    }
                }
                else
                {
                    actor.currentTile.region.usedByPathLock = true;
                    actor.currentTile.region.regionPathID = 0;
                }
            }
            World.world.pathfindingParam.useGlobalPathLock = flag3;
            WorldTile pTargetTile = target;
            if (AStarFinder.result_split_path && actor.current_path_global != null && actor.current_path_global.Count > 4)
            {
                pTargetTile = actor.current_path_global[4].tiles.GetRandom<WorldTile>();
            }
            _ = World.world.calcPath(actor.currentTile, pTargetTile, actor.current_path);
            if (AStarFinder.result_split_path)
            {
                actor.split_path = SplitPathStatus.Prepare;
            }
            if (flag3)
            {
                if (actor.current_path_global != null)
                {
                    for (int j = 0; j < actor.current_path_global.Count; j++)
                    {
                        MapRegion mapRegion2 = actor.current_path_global[j];
                        mapRegion2.usedByPathLock = false;
                        mapRegion2.regionPathID = -1;
                    }
                }
                actor.currentTile.region.usedByPathLock = false;
                actor.currentTile.region.regionPathID = -1;
            }
            actor.setTileTarget(target);
            return ExecuteEvent.True;
        }
        [HarmonyPrefix]
        [HarmonyPatch(typeof(PowerButton), "findNeighbours")]
        public static bool findNeighbours(List<PowerButton> pButtons)
        {
            if (pButtons.Count > 1) { return false; }
            return true;
        }


        public static bool PVZRangeDamage(WorldTile pTile, float pDamage, int f, AttackType pType, BaseSimObject pAttacker = null, WorldAction action = null)
        {
            bool attack = false;
            List<BaseSimObject> temp_map_objects = getObjectsInChunks(pTile, f, MapObjectType.All);
            for (int i = 0; i < temp_map_objects.Count; i++)
            {
                bool isEnemy = true;
                BaseSimObject pObj = temp_map_objects[i];
                if (pAttacker != null && pAttacker.kingdom != null)
                {
                    isEnemy = pAttacker.kingdom.isEnemy(pObj.kingdom);
                }
                if (pObj.isAlive() && isEnemy && (action == null || action(pObj, pObj.currentTile)))
                {
                    pObj.getHit(pDamage, true, pType, pAttacker, true);
                    attack = true;
                }
            }
            return attack;
        }


        public static class translationTools
        {

            public static void easyTranslate(string pLanguage, string id, string name)
            {
                string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
                if (language is not "en" and not "ch" and not "cz")
                {
                    language = "en";
                }
                if (pLanguage != language)
                {
                    return;
                }
                Localization.addLocalization(id, name);
            }


            public static void easyTranslateWithDescription(string pLanguage, string id, string name)
            {
                string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
                if (language is not "en" and not "ch" and not "cz")
                {
                    pLanguage = "en";
                }
                if (pLanguage != language)
                {
                    return;
                }
                Localization.addLocalization(id, name);
            }
        }

    }
}