using System;
using System.Collections.Generic;
using UnityEngine;
using ReflectionUtility;
//using sfx;

namespace K_mod
{
    class k_buildingsLibrary
    {
        public void init()
        {



            loadRaceBuildings();
        }
        private BuildingAsset get(string pID)
        {
            return AssetManager.buildings.get(pID);
        }

        [Obsolete]
        private void loadRaceBuildings()
        {

            var buildingTypesSimple = new string[]{
                "watch_tower",
                "docks",
                "barracks",
                "temple"

            };

            var buildingTypesExtended = new List<(string, string, BuildingFundament, ConstructionCost)>{
                ("tent", "house",   new BuildingFundament(2, 2, 2, 0), new ConstructionCost()),
                ( "house", "1house",  new BuildingFundament(1, 1, 2, 0), new ConstructionCost(5)),
                ( "1house", "2house", new BuildingFundament(1, 2, 2, 0), new ConstructionCost(4)),
                ( "2house", "3house", new BuildingFundament(1, 2, 2, 0), new ConstructionCost(0, 5, 0, 0)),
                ( "3house", "4house", new BuildingFundament(2, 2, 2, 0), new ConstructionCost(0, 10, 0, 0)),
                ( "4house", "5house", new BuildingFundament(2, 2, 2, 0), new ConstructionCost(0, 15, 0, 0)),
                ( "5house", null, new BuildingFundament(2, 3, 3, 0), new ConstructionCost(0, 20, 2, 10)),
                ( "hall", "1hall",   new BuildingFundament(2, 3, 4, 0), new ConstructionCost(10, 0, 0, 10)),
                ( "1hall", "2hall",  new BuildingFundament(3, 4, 4, 0), new  ConstructionCost(0, 10, 1, 20)),
                ( "2hall", null, new BuildingFundament(4, 4, 4, 0), new ConstructionCost(0, 15, 1, 100)),
                ( "fishing_docks", "docks", new BuildingFundament(2, 2, 4, 0), new ConstructionCost(15)),

            };
            //罗马独特建筑消耗
            var buildingTypesExtended_Rome = new List<(string, string, BuildingFundament, ConstructionCost)>{
                ("tent", "house",   new BuildingFundament(2, 2, 2, 0), new ConstructionCost()),
                ( "house", "1house",  new BuildingFundament(1, 1, 2, 0), new ConstructionCost(5)),
                ( "1house", "2house", new BuildingFundament(1, 2, 1, 0), new ConstructionCost(4)),
                ( "2house", "3house", new BuildingFundament(1, 2, 1, 0), new ConstructionCost(10, 0, 0, 0)),
                ( "3house", "4house", new BuildingFundament(1, 2, 1, 0), new ConstructionCost(0, 11, 0, 0)),
                ( "4house", "5house", new BuildingFundament(1, 2, 2, 0), new ConstructionCost(0, 17, 0, 0)),
                ( "5house", null, new BuildingFundament(2, 2, 3, 0), new ConstructionCost(0, 22, 2, 10)),
                ( "hall", "1hall",   new BuildingFundament(2, 3, 4, 0), new ConstructionCost(5, 15, 0, 15)),
                ( "1hall", "2hall",  new BuildingFundament(3, 4, 4, 0), new  ConstructionCost(5, 20, 1, 20)),
                ( "2hall", null, new BuildingFundament(4, 4, 4, 0), new ConstructionCost(5, 25, 1, 100)),
                ( "fishing_docks", "docks", new BuildingFundament(2, 2, 4, 0), new ConstructionCost(5)),
            };


            foreach (var race in RacesLibrary.additionalRaces)
            {
                foreach (var buildingType in buildingTypesSimple)
                {

                    BuildingAsset building_base;
                    string Base_Building = SB.watch_tower_human;
                    if (buildingType == "docks") { Base_Building = SB.docks_human; }
                    if (buildingType == "barracks") { Base_Building = SB.barracks_human; }
                    if (buildingType == "temple")
                    {
                        Base_Building = SB.temple_human;
                        // building2.burnable = false;
                    }


                    else { building_base = AssetManager.buildings.get($"{buildingType}_human"); }
                    var building = AssetManager.buildings.clone($"{buildingType}_{race}", Base_Building);
                    building.race = race;
                    building.canBeUpgraded = false;


                    loadSprites(building);
                }
                if (race != "Rome" && race != "Russia")
                {
                    foreach (var buildingType in buildingTypesExtended)
                    {
                        string Base_Building = SB.tent_human;
                        if (buildingType.Item1 == "house") { Base_Building = SB.house_human_0; }
                        if (buildingType.Item1 == "1house") { Base_Building = SB.house_human_1; }
                        if (buildingType.Item1 == "2house") { Base_Building = SB.house_human_2; }
                        if (buildingType.Item1 == "3house") { Base_Building = SB.house_human_3; }
                        if (buildingType.Item1 == "4house") { Base_Building = SB.house_human_4; }
                        if (buildingType.Item1 == "5house") { Base_Building = SB.house_human_5; }
                        if (buildingType.Item1 == "hall") { Base_Building = SB.hall_human_0; }
                        if (buildingType.Item1 == "1hall") { Base_Building = SB.hall_human_1; }
                        if (buildingType.Item1 == "2hall") { Base_Building = SB.hall_human_2; }
                        if (buildingType.Item1 == "fishing_docks") { Base_Building = SB.fishing_docks_human; }
                        else if (buildingType.Item1 == "windmill_1") { Base_Building = SB.windmill_human_1; }
                        if (buildingType.Item1 == "windmil_0") { Base_Building = SB.windmill_human_0; }
                        var buildingFundament = buildingType.Item3;
                        var building_base = AssetManager.buildings.get(Base_Building);
                        if (building_base != null)
                        {
                            var building = AssetManager.buildings.clone($"{buildingType.Item1}_{race}", Base_Building);
                            building.race = race;
                            building.fundament = buildingFundament;
                            building.cost = buildingType.Item4;
                            building.draw_light_area = true;

                            if (!String.IsNullOrEmpty(buildingType.Item2))
                            {

                                building.canBeUpgraded = true;
                                building.upgradeTo = $"{buildingType.Item2}_{race}";
                            }
                            else
                            {
                                building.canBeUpgraded = false;
                            }
                            AssetManager.buildings.loadSprites(building);
                        }
                    }
                }
                if (race == "Russia")
                {
                    foreach (var buildingType in buildingTypesExtended)
                    {
                        string Base_Building = SB.tent_human;
                        bool flag = false;
                        if (buildingType.Item1 == "house") { flag = true; Base_Building = SB.house_human_0; }
                        if (buildingType.Item1 == "1house") { flag = true; Base_Building = SB.house_human_1; }
                        if (buildingType.Item1 == "2house") { flag = true; Base_Building = SB.house_human_2; }
                        if (buildingType.Item1 == "3house") {  Base_Building = SB.house_human_3; }
                        if (buildingType.Item1 == "4house") { Base_Building = SB.house_human_4; }
                        if (buildingType.Item1 == "5house") { Base_Building = SB.house_human_5; }
                        if (buildingType.Item1 == "hall") { Base_Building = SB.hall_human_0; }
                        if (buildingType.Item1 == "1hall") { Base_Building = SB.hall_human_1; }
                        if (buildingType.Item1 == "2hall") { Base_Building = SB.hall_human_2; }
                        if (buildingType.Item1 == "fishing_docks") { flag = true; Base_Building = SB.fishing_docks_human; }
                        else if (buildingType.Item1 == "windmill_1") { Base_Building = SB.windmill_human_1; }
                        if (buildingType.Item1 == "windmil_0") { Base_Building = SB.windmill_human_0; }
                        var buildingFundament = buildingType.Item3;
                        var building_base = AssetManager.buildings.get(Base_Building);
                        if (building_base != null && !flag)
                        {
                            var building = AssetManager.buildings.clone($"{buildingType.Item1}_{race}", Base_Building);
                            building.race = race;
                            building.fundament = buildingFundament;
                            building.cost = buildingType.Item4;
                            building.draw_light_area = true;

                            if (!String.IsNullOrEmpty(buildingType.Item2))
                            {

                                building.canBeUpgraded = true;
                                building.upgradeTo = $"{buildingType.Item2}_{race}";
                            }
                            else
                            {
                                building.canBeUpgraded = false;
                            }
                            AssetManager.buildings.loadSprites(building);
                        }
                        else if (flag)
                        {
                            var building = AssetManager.buildings.clone($"{buildingType.Item1}_{race}", Base_Building);
                            building.race = race;
                            building.fundament = buildingFundament;
                            building.cost = buildingType.Item4;
                            building.draw_light_area = true;
                            

                            if (!String.IsNullOrEmpty(buildingType.Item2))
                            {
                                building.canBeUpgraded = true;
                                building.upgradeTo = $"{buildingType.Item2}_{race}";
                            }
                            else
                            {
                                building.canBeUpgraded = false;
                            }
                            // AssetManager.buildings.loadSprites(building);
                        }
                    }
                }
                else
                {
                    foreach (var buildingType in buildingTypesExtended_Rome)
                    {
                        string Base_Building = SB.tent_human;
                        if (buildingType.Item1 == "house") { Base_Building = SB.house_human_0; }
                        if (buildingType.Item1 == "1house") { Base_Building = SB.house_human_1; }
                        if (buildingType.Item1 == "2house") { Base_Building = SB.house_human_2; }
                        if (buildingType.Item1 == "3house") { Base_Building = SB.house_human_3; }
                        if (buildingType.Item1 == "4house") { Base_Building = SB.house_human_4; }
                        if (buildingType.Item1 == "5house") { Base_Building = SB.house_human_5; }
                        if (buildingType.Item1 == "hall") { Base_Building = SB.hall_human_0; }
                        if (buildingType.Item1 == "1hall") { Base_Building = SB.hall_human_1; }
                        if (buildingType.Item1 == "2hall") { Base_Building = SB.hall_human_2; }
                        if (buildingType.Item1 == "fishing_docks") { Base_Building = SB.fishing_docks_human; }
                        else if (buildingType.Item1 == "windmill_1") { Base_Building = SB.windmill_human_1; }
                        if (buildingType.Item1 == "windmil_0") { Base_Building = SB.windmill_human_0; }
                        var buildingFundament = buildingType.Item3;
                        var building_base = AssetManager.buildings.get(Base_Building);
                        if (building_base != null)
                        {
                            var building = AssetManager.buildings.clone($"{buildingType.Item1}_{race}", Base_Building);
                            building.race = race;
                            building.fundament = buildingFundament;
                            building.cost = buildingType.Item4;
                            building.draw_light_area = true;

                            if (building.housing > 1)
                            {
                                building.housing += 2;
                            }

                            if (!String.IsNullOrEmpty(buildingType.Item2))
                            {

                                building.canBeUpgraded = true;
                                building.upgradeTo = $"{buildingType.Item2}_{race}";

                            }
                            else
                            {
                                building.canBeUpgraded = false;
                            }
                            AssetManager.buildings.loadSprites(building);

                            // var w = AssetManager.buildings.get("wacth_tower_Rome");  

                        }
                    }
                }
            }
            var building2 = AssetManager.buildings.get("temple_Rome");
            building2.draw_light_area = false;
            building2.fundament = new BuildingFundament(3, 4, 3, 0);
            building2.cost = new ConstructionCost(pStone: 20, pGold: 100);
            BuildingAsset a = AssetManager.buildings.clone("Roman_triumphal_arch", "temple_Rome");
            a.sprite_path = "buildings/Roman_triumphal_arch";
            a.fundament = new BuildingFundament(4, 4, 4, 0);
            a.cost = new ConstructionCost(pStone: 20, pGold: 100);
            AssetManager.buildings.add(a);
            AssetManager.buildings.loadSprites(a);

            BuildingAsset b = AssetManager.buildings.clone("Arc_de_Triomphe", "temple_Rome");
            b.sprite_path = "buildings/Arc_de_Triomphe";
            b.fundament = new BuildingFundament(4, 4, 4, 0);
            b.cost = new ConstructionCost(pStone: 20, pGold: 100);
            AssetManager.buildings.add(b);
            AssetManager.buildings.loadSprites(b);


        }



        private static Dictionary<string, Sprite[]> cached_sprite_list;

        [Obsolete]
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
        // private static void loadSprites(BuildingAsset pTemplate)
        // {   
        //     AssetManager.buildings.CallMethod("loadSprites", pTemplate);


        //     return;

        //     // for(int i = 0; i < pTemplate.sprites.animationData.Count; i++){
        //     //     var sprites = pTemplate.sprites.animationData[i];
        //     //     for(int j = 0; j < sprites.list_main.Count; j++){
        //     //         var sprite = sprites.list_main[j];

        //     //         sprites.list_main[j] = Sprite.Create(sprite.texture, new Rect(0.0f, 0.0f, (float)sprite.texture.width, (float)sprite.texture.height), new Vector2(0.5f, 0f), 1f);
        //     //     }


        //     //     for(int j = 0; j < sprites.list_shadows.Count; j++){
        //     //         var sprite = sprites.list_shadows[j];

        //     //         sprites.list_shadows[j] = Sprite.Create(sprite.texture, new Rect(0.0f, 0.0f, (float)sprite.texture.width, (float)sprite.texture.height), new Vector2(0.5f, 0f), 1f);
        //     //     }


        //     //     for(int j = 0; j < sprites.list_ruins.Count; j++){
        //     //         var sprite = sprites.list_ruins[j];

        //     //         sprites.list_ruins[j] = Sprite.Create(sprite.texture, new Rect(0.0f, 0.0f, (float)sprite.texture.width, (float)sprite.texture.height), new Vector2(0.5f, 0f), 1f);
        //     //     }


        //     //     for(int j = 0; j < sprites.list_special.Count; j++){
        //     //         var sprite = sprites.list_special[j];

        //     //         sprites.list_special[j] = Sprite.Create(sprite.texture, new Rect(0.0f, 0.0f, (float)sprite.texture.width, (float)sprite.texture.height), new Vector2(0.5f, 0f), 1f);
        //     //     }


        //     // }

        //     // foreach (BuildingAnimationDataNew animationDataNew in pTemplate.sprites.animationData)
        //     // {
        //     //     animationDataNew.main = animationDataNew.list_main.ToArray();
        //     //     animationDataNew.ruins = animationDataNew.list_ruins.ToArray();
        //     //     animationDataNew.shadows = animationDataNew.list_shadows.ToArray();
        //     //     animationDataNew.special = animationDataNew.list_special.ToArray();
        //     // }
        // }
    }
}