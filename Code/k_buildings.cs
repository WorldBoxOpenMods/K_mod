using System;
using System.Collections.Generic;
using ReflectionUtility;
using UnityEngine;

namespace K_mod
{
    class K_buildings
    {

        private List<BuildingAsset> humanBuildings = new();

        public void init()
        {
            foreach (BuildingAsset humanBuilding in AssetManager.buildings.list)
            {
                if (humanBuilding.race == "human")
                {
                    humanBuildings.Add(humanBuilding);
                }
            }

            foreach (var race in RacesLibrary.additionalRaces)
            {
                if (race == "Rome")
                {
                    initRome();
                }
                if (race == "Arab")
                {
                    initRome();
                }
            }
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
            cached_sprite_list ??= Reflection.GetField(typeof(SpriteTextureLoader), null, "cached_sprite_list") as Dictionary<string, Sprite[]>;
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


        private static void initRome()
        {

            RaceBuildOrderAsset pAsset = new()
            {
                id = "Rome"
            };
            AssetManager.race_build_orders.add(pAsset);
            pAsset.addBuilding("bonfire", 1);
            pAsset.addBuilding("tent_Rome", pHouseLimit: true);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("bonfire");
            pAsset.addUpgrade("tent_Rome");
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("tent_Rome");
            addVariantsUpgrade(pAsset, "house_Rome", List.Of<string>("hall_Rome"));
            addVariantsUpgrade(pAsset, "1house_Rome", List.Of<string>("1hall_Rome"));
            addVariantsUpgrade(pAsset, "2house_Rome", List.Of<string>("1hall_Rome"));
            addVariantsUpgrade(pAsset, "3house_Rome", List.Of<string>("2hall_Rome"));
            addVariantsUpgrade(pAsset, "4house_Rome", List.Of<string>("2hall_Rome"));
            pAsset.addUpgrade("hall_Rome", pPop: 30, pBuildings: 8);
            pAsset.addUpgrade("1hall_Rome", pPop: 100, pBuildings: 20);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("statue", "mine", "barracks_Rome");
            pAsset.addUpgrade("fishing_docks_Rome");
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("fishing_docks_Rome");
            pAsset.addBuilding("windmill_1_Rome", 1, pPop: 6, pBuildings: 5);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("bonfire");
            pAsset.addUpgrade("windmill_1_Rome", pPop: 40, pBuildings: 10);
            pAsset.addBuilding("fishing_docks_Rome", 5, pBuildings: 2);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("bonfire");
            pAsset.addBuilding("well", 1, pPop: 20, pBuildings: 10);
            BuildOrderLibrary.b.requirements_types = List.Of<string>("hall");
            pAsset.addBuilding("hall_Rome", 1, pPop: 10, pBuildings: 6);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("bonfire");
            BuildOrderLibrary.b.requirements_types = List.Of<string>("house");
            pAsset.addBuilding("mine", 1, pPop: 20, pBuildings: 10);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("bonfire", "hall_Rome");
            pAsset.addBuilding("barracks_Rome", 1, pPop: 50, pBuildings: 16, pMinZones: 20);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("1hall_Rome");
            pAsset.addBuilding("watch_tower_Rome", 1, pPop: 30, pBuildings: 10);
            pAsset.addUpgrade("watch_tower_Rome", 0, 0, 3, 3, false, false, 0);

            BuildOrderLibrary.b.requirements_orders = List.Of<string>("bonfire", "hall_Rome");
            pAsset.addBuilding("temple_Rome", 1, pPop: 90, pBuildings: 20, pMinZones: 20);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("bonfire", "1hall_Rome", "statue");
            pAsset.addBuilding("statue", 1, pPop: 70, pBuildings: 15);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("1hall_Rome");


        }
        private static void initArab()
        {

            RaceBuildOrderAsset pAsset = new()
            {
                id = "Arab"
            };
            AssetManager.race_build_orders.add(pAsset);
            pAsset.addBuilding("bonfire", 1);
            pAsset.addBuilding("tent_Arab", pHouseLimit: true);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("bonfire");
            pAsset.addUpgrade("tent_Arab");
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("tent_Arab");
            addVariantsUpgrade(pAsset, "house_Arab", List.Of<string>("hall_Arab"));
            addVariantsUpgrade(pAsset, "1house_Arab", List.Of<string>("1hall_Arab"));
            addVariantsUpgrade(pAsset, "2house_Arab", List.Of<string>("1hall_Arab"));
            addVariantsUpgrade(pAsset, "3house_Arab", List.Of<string>("2hall_Arab"));
            addVariantsUpgrade(pAsset, "4house_Arab", List.Of<string>("2hall_Arab"));
            pAsset.addUpgrade("hall_Arab", pPop: 30, pBuildings: 8);
            //BuildOrderLibrary.b.requirements_orders = List.Of<string>("1house_human");
            pAsset.addUpgrade("1hall_Arab", pPop: 100, pBuildings: 20);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("statue", "mine", "barracks_Arab");
            pAsset.addUpgrade("fishing_docks_Arab");
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("fishing_docks_Arab");
            pAsset.addBuilding("windmill_1_Arab", 1, pPop: 6, pBuildings: 5);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("bonfire");
            pAsset.addUpgrade("windmill_1_Arab", pPop: 40, pBuildings: 10);
            //   addVariantsUpgrade(pAsset, "windmill_0_Arab", List.Of<string>("hall_Arab"));
            pAsset.addBuilding("fishing_docks_Arab", 5, pBuildings: 2);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("bonfire");
            pAsset.addBuilding("well", 1, pPop: 20, pBuildings: 10);
            BuildOrderLibrary.b.requirements_types = List.Of<string>("hall");
            pAsset.addBuilding("hall_Arab", 1, pPop: 10, pBuildings: 6);
            // pAsset.addBuilding("5house_Arab", 1, pPop: 10, pBuildings: 6);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("bonfire");
            BuildOrderLibrary.b.requirements_types = List.Of<string>("house");
            pAsset.addBuilding("mine", 1, pPop: 20, pBuildings: 10);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("bonfire", "hall_Arab");
            pAsset.addBuilding("barracks_Arab", 1, pPop: 50, pBuildings: 16, pMinZones: 20);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("1hall_Arab");
            pAsset.addBuilding("watch_tower_Arab", 1, pPop: 30, pBuildings: 10);
            pAsset.addUpgrade("watch_tower_Arab", 0, 0, 3, 3, false, false, 0);

            BuildOrderLibrary.b.requirements_orders = List.Of<string>("bonfire", "hall_Arab");
            pAsset.addBuilding("temple_Arab", 1, pPop: 90, pBuildings: 20, pMinZones: 20);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("bonfire", "1hall_Arab", "statue");
            pAsset.addBuilding("statue", 1, pPop: 70, pBuildings: 15);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("1hall_Arab");


        }




        private static void addVariantsUpgrade(RaceBuildOrderAsset pAsset, string name, List<string> requirementsBuildings)
        {
            foreach (var race in RacesLibrary.defaultRaces)
            {
                pAsset.addUpgrade($"{name}_{race}");
                BuildOrderLibrary.b.requirements_orders = requirementsBuildings;
            }
        }
    }
}