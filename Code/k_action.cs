using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;
using System.Linq;
using System.Text;
using System.IO;
using ReflectionUtility;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using NCMS.Utils;
using HarmonyLib;
using UnityEngine.Tilemaps;
using static Config;
using Newtonsoft.Json;
using NCMS;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using K_mod.Utils;
using System.Globalization;
using System.Runtime.CompilerServices;
using EpPathFinding.cs;
using ai;

namespace K_mod
{
    [Serializable]
    public delegate T KAction<T>();
    [Serializable]
    public delegate T1 KAction<T1, T2>(T2 t);
    public class K_action
    {
        public Actor a = null;
        public string id = "null";
        public float interval = 0f;
        public float intervals = 1f;
        public bool animation = false;
        public bool destroy = false;
        public bool paused_ = true;
        public bool for_ = false;
        public float time = -1f;
        public BaseStats stats = new();
        public List<string> textures = new();
        public void update(bool paused)
        {
            if (destroy)
            {
                return;
            }
            if (!this.a.Any())
            {
                this.destroy = true;
                return;
            }
            if (this.paused_ && paused)
            {
                return;
            }
            float world_time = (float)World.world.getCurWorldTime();
            if (this.time > 0f && world_time >= this.time)
            {
                this.destroy = true;
                return;
            }
            float intervals = (1f + this.a.stats["k_mod_time"]) * this.intervals;
            float interval = world_time + intervals;
            if (this.interval > interval)
            {
                this.interval = interval;
            }
            if (world_time >= this.interval)
            {
                float value = world_time - this.interval;
                if (intervals != float.MaxValue && intervals > 0f)
                {
                    if (value >= intervals)
                    {
                        value %= intervals;
                    }
                    this.interval = interval - value;
                }
                else
                {
                    this.interval = intervals;
                }
                if (!for_)
                {
                    if (this.destroy)
                    {
                        return;
                    }
                    if (this.animation)
                    {
                        if (this.textures.Any())
                        {
                            this.textures.Remove(this.textures[0]);
                        }
                        this.destroy = !this.textures.Any();
                    }
                    if (Main.KActions.ContainsKey(this.id))
                    {
                        Main.KActions[this.id](this.a, this);
                    }
                    else if (!this.animation)
                    {
                        this.destroy = true;
                    }
                }
                else for (int i = 0; i < 1 + (int)(value / intervals); i++)
                    {
                        if (this.destroy)
                        {
                            return;
                        }
                        if (this.animation)
                        {
                            if (this.textures.Any())
                            {
                                this.textures.Remove(this.textures[0]);
                            }
                            this.destroy = !this.textures.Any();
                        }
                        if (Main.KActions.ContainsKey(this.id))
                        {
                            Main.KActions[this.id](this.a, this);
                        }
                        else if (!this.animation)
                        {
                            this.destroy = true;
                        }
                    }
            }
        }
        public static void init()
        {
            Main.KActions.Add("Ballista", delegate (Actor actor, K_action action)
            {
                // action.animation=true;
                // action.destroy = false;
                // action.textures = Main.NewTextures("actors/Ballista/Ballista_Shoot", 10);
                Main.NewAnimation("Ballista_Shoot", actor, 0.04f, Main.NewTextures("actors/Ballista/Ballista_Shoot", 5));

            });
            

        }


        public static BaseSimObject action_attack(Actor actor, bool attack = true)
        {
            BaseSimObject obj = actor.attackTarget;
            if (obj == null || !obj.isAlive())
            {
                obj = actor.findEnemyObjectTarget();
                if (obj != null && obj.isAlive())
                {
                    actor.setAttackTarget(obj);
                }
            }
            if (!attack) { return obj; }
            if (obj != null && obj.isAlive())
            {
                actor.tryToAttack(obj, false);
            }
            else { actor.tryToAttack(actor, false); }
            return obj;
        }
        public static void action_explosion(Vector3 pPos, float pScale, int range = 8)
        {
            MapAction.damageWorld(World.world.GetTile((int)pPos.x, (int)pPos.y), range, AssetManager.terraform.get("pvz_bomb"));
            EffectsLibrary.spawnAt("fx_explosion_small", pPos, pScale);
        }

        public static void action_flash(Vector3 pPos, float pScale)
        {
            EffectsLibrary.spawn("fx_nuke_flash", World.world.GetTile((int)pPos.x, (int)pPos.y), null, null, 0f, -1f, -1f);
        }
        public static List<WorldTile> action_to_tile(Actor actor, BaseSimObject obj, UnityAction<Actor, BaseSimObject, WorldTile> action, AStarParam pParam)
        {
            List<WorldTile> tiles = new();
            WorldTile tile1 = World.world.GetTile((int)obj.currentPosition.x, (int)obj.currentPosition.y);
            WorldTile tile2 = World.world.GetTile((int)actor.currentPosition.x, (int)actor.currentPosition.y);
            if (tile1 == null)
            {
                tile1 = obj.currentTile;
            }
            if (tile2 == null)
            {
                tile2 = actor.currentTile;
            }
            if (PathfinderTools.tryToGetSimplePath(tile2, tile1, tiles, actor.asset, pParam))
            {
                foreach (WorldTile tile in tiles)
                {
                    action(actor, obj, tile);
                }
            }
            return tiles;
        }

    }
}