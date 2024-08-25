using System.Linq;
using System.Collections.Generic;
using K_mod.Utils;
using UnityEngine;
namespace K_mod
{
    class K_update
    {
        public static void update_actions(bool paused)
        {
            List<K_action> destroyAction = new();
            for (int i = 0; i < Main.k_actions.Count; i++)
            {
                K_action action = Main.k_actions[i];
                if (action.destroy)
                {
                    destroyAction.Add(action);
                }
                else { action.update(paused); }
            }
            foreach (K_action action in destroyAction)
            {
                Actor a = action.a;
                if (Main.Actor_Action.ContainsKey(a))
                {
                    if (Main.Actor_Action[a].Any() && a.Any())
                    {
                        Main.Actor_Action[a].Remove(action);
                    }
                    else { Main.Actor_Action.Remove(a); }
                }
                Main.k_actions.Remove(action);
                if (a.Any()) { a.setStatsDirty(); }
            }
        }
        public static void update_horse(bool paused)
        {
            if(!paused)
            {
                return;
            }
            for (int i = 0; i < Main.Rider.Count; i++)
            {
                Actor rider = Main.Rider[i];
                Actor horse = Main.Rider_horse[rider];
                rider.setShowShadow(false);
                if (horse != null && rider != null && horse.data != null && rider.data != null
                    && horse.data.alive && rider.data.alive && horse.isAlive() && rider.isAlive())
                {
                    horse.curTransformPosition = new Vector3(rider.curTransformPosition.x, rider.curTransformPosition.y - Main.Rider_z[rider]);
                    horse.transform.position = new Vector3(rider.transform.position.x, rider.transform.position.y - Main.Rider_z[rider]);
                    horse.currentPosition = rider.currentPosition;
                    horse.currentTile = rider.currentTile;
                }
                else { Main.Dismount_horse(rider); }
            }
            for (int i = 0; i < Main.Horse.Count; i++)
            {
                Actor horse = Main.Horse[i];
                Actor rider = Main.Horse_rider[horse];
                rider.setShowShadow(false);
                if (horse != null && rider != null && horse.data != null && rider.data != null
                    && horse.data.alive && rider.data.alive && horse.isAlive() && rider.isAlive())
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