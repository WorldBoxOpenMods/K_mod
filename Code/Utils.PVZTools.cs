using System.Collections.Generic;
using UnityEngine;


namespace K_mod.Utils
{
    //感谢寒海赠送的tools，真的谢谢寒海
    public static class PVZTools
    {
        public static int Min(this int num, int num2)
		{
			if (num < num2) { return num2; }
			return num;
		}
        public static bool IsID(this Actor a, string id)
        {
            return a.asset != null && a.asset.id == id;
        }
        public static bool IsID(this ActorBase a, string id)
        {
            return a.asset != null && a.asset.id == id;
        }
        public static bool IsID(this Building b, string id)
        {
            return b.asset != null && b.asset.id == id;
        }
        public static bool IsID(this Projectile p, string id)
        {
            return p.asset != null && p.asset.id == id;
        }
        public static bool Any(this Actor a)
        {
            return a != null && a.isAlive() && a.data != null && a.data.alive;
        }
        public static bool Any(this City c)
        {
            return c != null && c.isAlive() && c.data != null && c.data.alive;
        }
        public static Vector2 AddAngle(this Vector2 pos, Vector2 pos2, float angle)
        {
            return new Vector2((pos.x - pos2.x) * Mathf.Cos(angle * Mathf.Deg2Rad) - (pos.y - pos2.y) * Mathf.Sin(angle * Mathf.Deg2Rad) + pos2.x,
            (pos.y - pos2.y) * Mathf.Cos(angle * Mathf.Deg2Rad) + (pos.x - pos2.x) * Mathf.Sin(angle * Mathf.Deg2Rad) + pos2.y);
        }
        public static Vector3 AddAngle(this Vector3 pos, Vector3 pos2, float angle)
        {
            return new Vector3((pos.x - pos2.x) * Mathf.Cos(angle * Mathf.Deg2Rad) - (pos.y - pos2.y) * Mathf.Sin(angle * Mathf.Deg2Rad) + pos2.x,
            (pos.y - pos2.y) * Mathf.Cos(angle * Mathf.Deg2Rad) + (pos.x - pos2.x) * Mathf.Sin(angle * Mathf.Deg2Rad) + pos2.y, pos.z);
        }
        public static void AddPos(this Actor a, float x, float y, bool setTile = true)
        {
            a.currentPosition.x = a.currentPosition.x + x;
            a.currentPosition.y = a.currentPosition.y + y;
            a.curShadowPosition.x = a.curShadowPosition.x + x;
            a.curShadowPosition.y = a.curShadowPosition.y + y;
            a.curTransformPosition.x = a.curTransformPosition.x + x;
            a.curTransformPosition.y = a.curTransformPosition.y + y;
            a.transform.position = new Vector3(a.currentPosition.x, a.currentPosition.y, a.transform.position.z);
            if (!setTile) { return; }
            WorldTile currentTile2 = World.world.GetTile((int)a.currentPosition.x, (int)a.currentPosition.y);
            if (currentTile2 != null) { a.setCurrentTile(currentTile2); }
        }
        public static void SetPos(this Actor a, float x, float y, bool setTile = true)
        {
            a.currentPosition.x = x;
            a.currentPosition.y = y;
            a.curShadowPosition.x = x;
            a.curShadowPosition.y = y;
            a.curTransformPosition.x = x;
            a.curTransformPosition.y = y;
            a.transform.position = new Vector3(x, y, a.transform.position.z);
            if (!setTile) { return; }
            WorldTile currentTile2 = World.world.GetTile((int)a.currentPosition.x, (int)a.currentPosition.y);
            if (currentTile2 != null) { a.setCurrentTile(currentTile2); }
        }
        public static List<K_action> getAnimations(this Actor a, bool all = false)
		{
			List<K_action> actions = new();
			List<K_action> results = new();
			if (all)
			{
				actions = Main.k_actions;
			}
			else if (Main.Actor_Action.ContainsKey(a))
			{
				actions = Main.Actor_Action[a];
			}
			else
			{
				return results;
			}
			for (int i = 0; i < actions.Count; i++)
			{
				K_action action = actions[i];
				if (!action.destroy && action.animation && action.a == a)
				{
					results.Add(action);
				}
			}
			return results;
		}
		public static K_action getAnimation(this Actor a, bool all = false)
		{
			List<K_action> actions = new();
			if (all)
			{
				actions = Main.k_actions;
			}
			else if (Main.Actor_Action.ContainsKey(a))
			{
				actions = Main.Actor_Action[a];
			}
			else
			{
				return null;
			}
			for (int i = 0; i < actions.Count; i++)
			{
				K_action action = actions[i];
                
				if (!action.destroy && action.animation && action.a == a)
				{
					return action;
				}
			}
			return null;
		}
        public static void removeStatusEffect(this BaseSimObject obj, string pID)
        {
            if (obj == null) { return; }
            if (!obj.base_data.alive) { return; }
            if (obj.activeStatus_dict == null) { return; }
            if (!obj.activeStatus_dict.ContainsKey(pID)) { return; }
            obj.setStatsDirty();
            obj.activeStatus_dict.Remove(pID);
        }
        public static bool hasResource(this Actor act, string pID)
        {
            ResourceAsset pResource = AssetManager.resources.get(pID);
            if (pResource == null) { return false; }
            if (act.data != null && act.data.alive && !act.data.inventory.isEmpty())
            {
                if (act.data.inventory.dict.ContainsKey(pID) && act.data.inventory.dict[pID].amount > 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static void addResource(this Actor act, string pID, int SL)
        {
            ResourceAsset pResource = AssetManager.resources.get(pID);
            if (pResource == null || SL < 1) { return; }
            ResourceContainer RC = new()
            {
                id = pID,
                amount = SL
            };
            if (act.data != null && act.data.alive && act.data.inventory != null)
            {
                if (act.data.inventory.dict == null)
                {
                    act.data.inventory.dict = new Dictionary<string, ResourceContainer>();
                }
                if (act.data.inventory.dict.ContainsKey(pID))
                {
                    act.data.inventory.dict[pID].amount += SL;
                }
                else { act.data.inventory.dict.Add(pID, RC); }
                if (act.data.inventory.dict[pID].amount > pResource.storage_max)
                {
                    act.data.inventory.dict[pID].amount = pResource.storage_max;
                }
            }
        }

    
        public static bool hasItem(this Actor act, string pID)
        {
            if (act.equipment != null)
            {
                List<ActorEquipmentSlot> AESKList = ActorEquipment.getList(act.equipment);
                if (AESKList != null && AESKList.Count > 0)
                {
                    foreach (ActorEquipmentSlot AE in AESKList)
                    {
                        if (AE.data != null && AE.data.id == pID) { return true; }
                    }
                }
            }
            return false;
        }
        public static ItemData addItem(this Actor a, string pID, string materials = null)
        {
            ItemData item_data = null;
            if (a.equipment != null)
            {
                ItemAsset item_asset = AssetManager.items.get(pID);
                if (item_asset != null)
                {
                    if (materials == null) { materials = Toolbox.getRandom<string>(item_asset.materials); }
                    item_data = ItemGenerator.generateItem(item_asset, materials, World.world.mapStats.year, a.kingdom, a.getName(), 1, a);
                    a.equipment.getSlot(item_asset.equipmentType).setItem(item_data);
                }
            }
            a.setStatsDirty();
            return item_data;
        }
    }
}

