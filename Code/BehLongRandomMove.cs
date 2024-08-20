using ai.behaviours;
using System;
using UnityEngine;
using System.Collections.Generic;
using ReflectionUtility;
using K_mod.Utils;

namespace K_mod
{
	public class BehLongRandomMove : BehaviourActionActor
	{
		public override BehResult execute(Actor pActor)
		{
			Debug.Log("启动！");

			if (pActor.currentTile == null)
			{
				Debug.LogError("pActor.currentTile is null.");
				return BehResult.Stop;
			}
			float MaxTDJL = 0f;
			WorldTile worldTile = null;
			List<WorldTile> WTList = getTilesInRange(pActor.currentTile, 6);
			for (int i = 0; i < WTList.Count; i++)
			{
				if (WTList[i] != null && pActor.currentTile.isSameIsland(WTList[i]))
				{
					float TDJL = Toolbox.DistTile(pActor.currentTile, WTList[i]);
					if (TDJL > MaxTDJL)
					{
						MaxTDJL = TDJL;
						if (i == WTList.Count - 1) break; // 如果已经找到最大距离，跳出循环
					}
				}
			}


			if (worldTile == null)
			{
				Debug.Log("搜索失败");
				return BehResult.Stop;
			}

			pActor.beh_tile_target = worldTile;
			return BehResult.Continue;
		}
		public static List<WorldTile> getTilesInRange(WorldTile tile, int range)
		{
			List<WorldTile> list = new() { tile };
			for (int i = 1; i <= range; i++)
			{
				for (int l = 0; l < i; l++)
				{
					WorldTile tile2 = World.world.GetTile(tile.x - l, tile.y + i - l);
					if (tile2 != null) list.Add(tile2);

					WorldTile tile3 = World.world.GetTile(tile.x - i + l, tile.y - l);
					if (tile3 != null) list.Add(tile3);

					WorldTile tile4 = World.world.GetTile(tile.x + l, tile.y - i + l);
					if (tile4 != null) list.Add(tile4);

					WorldTile tile5 = World.world.GetTile(tile.x + i - l, tile.y + l);
					if (tile5 != null) list.Add(tile5);
				}
			}
			return list;
		}
	}

}