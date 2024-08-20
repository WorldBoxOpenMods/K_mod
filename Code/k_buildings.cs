using System.Collections.Generic;

namespace K_mod
{
    class k_buildings
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
            //pAsset.addUpgrade("house_Rome");
            addVariantsUpgrade(pAsset, "house_Rome", List.Of<string>("hall_Rome"));
            //BuildOrderLibrary.b.requirements_orders = List.Of<string>("hall_Rome");
            //pAsset.addUpgrade("1house_human");
            addVariantsUpgrade(pAsset, "1house_Rome", List.Of<string>("1hall_Rome"));
            //BuildOrderLibrary.b.requirements_orders = List.Of<string>("1hall_Rome");
            //pAsset.addUpgrade("2house_human");
            addVariantsUpgrade(pAsset, "2house_Rome", List.Of<string>("1hall_Rome"));
            //BuildOrderLibrary.b.requirements_orders = List.Of<string>("1hall_Rome");
            //pAsset.addUpgrade("3house_human");
            addVariantsUpgrade(pAsset, "3house_Rome", List.Of<string>("2hall_Rome"));
            //BuildOrderLibrary.b.requirements_orders = List.Of<string>("2hall_Rome");
            //pAsset.addUpgrade("4house_human");
            addVariantsUpgrade(pAsset, "4house_Rome", List.Of<string>("2hall_Rome"));
            // addVariantsUpgrade(pAsset, "5house_Rome", List.Of<string>("2hall_Rome"));
            //BuildOrderLibrary.b.requirements_orders = List.Of<string>("2hall_Rome");
            pAsset.addUpgrade("hall_Rome", pPop: 30, pBuildings: 8);
            //BuildOrderLibrary.b.requirements_orders = List.Of<string>("1house_human");
            pAsset.addUpgrade("1hall_Rome", pPop: 100, pBuildings: 20);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("statue", "mine", "barracks_Rome");
            pAsset.addUpgrade("fishing_docks_Rome");
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("fishing_docks_Rome");
            pAsset.addBuilding("windmill_1_Rome", 1, pPop: 6, pBuildings: 5);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("bonfire");
            pAsset.addUpgrade("windmill_1_Rome", pPop: 40, pBuildings: 10);
            //   addVariantsUpgrade(pAsset, "windmill_0_Rome", List.Of<string>("hall_Rome"));
            pAsset.addBuilding("fishing_docks_Rome", 5, pBuildings: 2);
            BuildOrderLibrary.b.requirements_orders = List.Of<string>("bonfire");
            pAsset.addBuilding("well", 1, pPop: 20, pBuildings: 10);
            BuildOrderLibrary.b.requirements_types = List.Of<string>("hall");
            pAsset.addBuilding("hall_Rome", 1, pPop: 10, pBuildings: 6);
            // pAsset.addBuilding("5house_Rome", 1, pPop: 10, pBuildings: 6);
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
            //pAsset.addUpgrade("house_Arab");
            addVariantsUpgrade(pAsset, "house_Arab", List.Of<string>("hall_Arab"));
            //BuildOrderLibrary.b.requirements_orders = List.Of<string>("hall_Arab");
            //pAsset.addUpgrade("1house_human");
            addVariantsUpgrade(pAsset, "1house_Arab", List.Of<string>("1hall_Arab"));
            //BuildOrderLibrary.b.requirements_orders = List.Of<string>("1hall_Arab");
            //pAsset.addUpgrade("2house_human");
            addVariantsUpgrade(pAsset, "2house_Arab", List.Of<string>("1hall_Arab"));
            //BuildOrderLibrary.b.requirements_orders = List.Of<string>("1hall_Arab");
            //pAsset.addUpgrade("3house_human");
            addVariantsUpgrade(pAsset, "3house_Arab", List.Of<string>("2hall_Arab"));
            //BuildOrderLibrary.b.requirements_orders = List.Of<string>("2hall_Arab");
            //pAsset.addUpgrade("4house_human");
            addVariantsUpgrade(pAsset, "4house_Arab", List.Of<string>("2hall_Arab"));
            // addVariantsUpgrade(pAsset, "5house_Arab", List.Of<string>("2hall_Arab"));
            //BuildOrderLibrary.b.requirements_orders = List.Of<string>("2hall_Arab");
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