using NCMS;
using System;
using NCMS.Utils;
using System.IO;
using System.Linq;
using UnityEngine;
using ReflectionUtility;
using HarmonyLib;
using System.Reflection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using UnityEngine.UI;
using pathfinding;

namespace K_mod
{
  class Buildings
  {
    public static void init()
    {


      BuildingAsset Catapultfactory = AssetManager.buildings.clone("Catapultfactory", "!city_building");
      Catapultfactory.id = "Catapultfactory";
      Catapultfactory.type = "Catapultfactory";
      Catapultfactory.base_stats[S.health] = 500f;
      Catapultfactory.base_stats[S.size] = 0.5f;
      Catapultfactory.material = "building";
      Catapultfactory.setShadow(1f, 2f, 0.12f);
      Catapultfactory.fundament = new BuildingFundament(4, 4, 2, 0);
      Catapultfactory.cost = new ConstructionCost(30, 2, 0, 300);
      Catapultfactory.buildingType = BuildingType.None;
      Catapultfactory.canBeDamagedByTornado = true;
      // Catapultfactory.canBePlacedOnBlocks = false;
      // Catapultfactory.canBePlacedOnLiquid = false;
      // Catapultfactory.destroyOnLiquid = true;
      Catapultfactory.auto_remove_ruin = false;
      // Catapultfactory.destroyOnLiquid = true;
      Catapultfactory.affectedByAcid = true;
      // Catapultfactory.canBeAbandoned = true;
      Catapultfactory.sparkle_effect = true;
      Catapultfactory.burnable = true;
      Catapultfactory.spawnUnits = true;
      Catapultfactory.spawnUnits_asset = "Catapult";
      Catapultfactory.sprite_path = "buildings/Catapultfactory";
      AssetManager.buildings.add(Catapultfactory);
      AssetManager.buildings.loadSprites(Catapultfactory);

      BuildingAsset Ballistafactory = AssetManager.buildings.clone("Ballistafactory", "Catapultfactory");
      Ballistafactory.id = "Ballistafactory";
      Ballistafactory.type = "Ballistafactory";
      Ballistafactory.material = "building";
      Ballistafactory.sprite_path = "buildings/Ballistafactory";
      Ballistafactory.spawnUnits = true;
      Ballistafactory.spawnUnits_asset = "Ballista";
      AssetManager.buildings.add(Ballistafactory);
      AssetManager.buildings.loadSprites(Ballistafactory);

      BuildingAsset Arab_Market = AssetManager.buildings.clone("Arab_Market", "!city_building");
      Arab_Market.id = "Arab_Market";
      Arab_Market.type = "Market";
      Arab_Market.race = "Arab";
      Arab_Market.base_stats[S.health] = 400f;
      Arab_Market.base_stats[S.size] = 0.5f;
      Arab_Market.material = "building";
      Arab_Market.setShadow(1f, 2f, 0.12f);
      Arab_Market.fundament = new BuildingFundament(4, 4, 2, 0);
      Arab_Market.cost = new ConstructionCost(20, 0, 0, 10);
      // Arab_Market.buildingType = "Arab_Market";
      Arab_Market.canBeDamagedByTornado = true;
      Arab_Market.canBePlacedOnBlocks = false;
      Arab_Market.canBePlacedOnLiquid = false;
      Arab_Market.destroyOnLiquid = true;
      Arab_Market.auto_remove_ruin = false;
      Arab_Market.destroyOnLiquid = true;
      Arab_Market.affectedByAcid = true;
      Arab_Market.canBeAbandoned = true;
      Arab_Market.sparkle_effect = true;
      Arab_Market.burnable = true;
      Arab_Market.sprite_path = "buildings/Arab_Market";
      AssetManager.buildings.add(Arab_Market);
      AssetManager.buildings.loadSprites(Arab_Market);


      RaceBuildOrderAsset human = AssetManager.race_build_orders.get("kingdom_base");
      human.addBuilding("Arab_Market", 1, pPop: 30, pBuildings: 10);
      human.addBuilding("order_Catapultfactory", 1, pPop: 100, pBuildings: 30);
      human.addBuilding("order_Ballistafactory", 1, pPop: 100, pBuildings: 30);
      Race humanRace = AssetManager.raceLibrary.get("human");
            humanRace.building_order_keys.Add("Arab_Market", "Arab_Market");
      humanRace.building_order_keys.Add("order_Catapultfactory", "Catapultfactory");
      humanRace.building_order_keys.Add("order_Ballistafactory", "Ballistafactory");
      Race Race2 = AssetManager.raceLibrary.get("Arab");

      Race2.building_order_keys.Add("Arab_Market", "Arab_Market");




    }
    private static Dictionary<string, Sprite[]> cached_sprite_list;
    internal static void loadSprites(BuildingAsset pTemplate)
    {
      if (cached_sprite_list is null)
      {
        cached_sprite_list = Reflection.GetField(typeof(SpriteTextureLoader), null, "cached_sprite_list") as Dictionary<string, Sprite[]>;
      }
      string pPath = pTemplate.sprite_path;
      if (String.IsNullOrEmpty(pPath))
      {
        pPath = $"buildings/{pTemplate.id}";
      }
      if (cached_sprite_list.ContainsKey(pPath))
      {
        cached_sprite_list.Remove(pPath);
      }
      AssetManager.buildings.loadSprites(pTemplate);
    }
  }
}