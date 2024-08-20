using System;
using ReflectionUtility;


namespace K_mod
{
    class NewGodPowers
    {
        public static bool action_Drop(WorldTile pTile, string pPower) { AssetManager.powers.CallMethod("spawnDrops", pTile, pPower); return true; }
        public static bool action_1(WorldTile pTile, string pPower) { AssetManager.powers.CallMethod("loopWithCurrentBrushPower", pTile, pPower); return true; }
        public static bool action_Drop(WorldTile pTile, GodPower pPower)
        {
            AssetManager.powers.CallMethod("spawnDrops", pTile, pPower);
            return true;
        }
        public static bool action_1(WorldTile pTile, GodPower pPower)
        {
            AssetManager.powers.CallMethod("loopWithCurrentBrushPower", pTile, pPower);
            return true;
        }
        public static bool action_spawn(WorldTile pTile, string pPowerID) { Actor actor = spawnUnit(pTile, pPowerID); if (actor == null) { return false; } return true; }

        [Obsolete]
        public static void init()
        {
            initPowers();
            initBuildingPower("Ballistafactory_drop", "b_drop");
            initBuildingPower("Catapultfactory_drop", "b_drop");
            // initStats();
        }

        public static Actor spawnUnit(WorldTile pTile, string pPowerID)
        {
            GodPower godPower = AssetManager.powers.get(pPowerID);
            MusicBox.playSound("event:/SFX/UNIQUE/SpawnWhoosh", (float)pTile.pos.x, (float)pTile.pos.y, false, false);
            if (godPower.id == SA.sheep && pTile.Type.lava)
            {
                AchievementLibrary.achievementSacrifice.check(null, null, null);
            }
            EffectsLibrary.spawn("fx_spawn", pTile, null, null, 0f, -1f, -1f);
            string text;
            if (godPower.actor_asset_ids.Count > 0)
            {
                text = godPower.actor_asset_ids.GetRandom<string>();
            }
            else
            {
                text = godPower.actor_asset_id;
            }
            Actor actor = World.world.units.spawnNewUnit(text, pTile, true, godPower.actorSpawnHeight);
            actor.addTrait("miracle_born", false);
            actor.data.age_overgrowth = 18;
            actor.data.had_child_timeout = 8f;
            return actor;
        }

        [Obsolete]
        public static void initPowers()
        {

            GodPower Russiapower = AssetManager.powers.clone("spawn_Russia", "_spawnActor");
            Russiapower.name = "spawn_Russia";
            Russiapower.actor_asset_id = "unit_Russia";
            Russiapower.click_action = new PowerActionWithID(action_spawn);
            AssetManager.powers.add(Russiapower);

            GodPower Romepower = AssetManager.powers.clone("spawn_Rome", "_spawnActor");
            Romepower.name = "spawn_Rome";
            Romepower.actor_asset_id = "unit_Rome";
            Romepower.click_action = new PowerActionWithID(action_spawn);
            AssetManager.powers.add(Romepower);

            GodPower Arabpower = AssetManager.powers.clone("spawn_Arab", "_spawnActor");
            Arabpower.name = "spawn_Arab";
            Arabpower.actor_asset_id = "unit_Arab";
            Arabpower.click_action = new PowerActionWithID(action_spawn);
            AssetManager.powers.add(Arabpower);

            GodPower horsepower = AssetManager.powers.clone("spawn_horse", "_spawnActor");
            horsepower.name = "spawn_horse";
            horsepower.actor_asset_id = "horse";
            horsepower.click_action = new PowerActionWithID(action_spawn);
            AssetManager.powers.add(horsepower);


            DropAsset addItemsDrop = AssetManager.drops.clone("addItems", "blessing");
            addItemsDrop.id = "addItems";
            addItemsDrop.default_scale = 0.1f;
            addItemsDrop.action_landed = new DropsAction(NewActions.action_give_item);
            AssetManager.drops.add(addItemsDrop);

            DropAsset addHorseDrop = AssetManager.drops.clone("addHorse", "blessing");
            addHorseDrop.id = "addHorse";
            addHorseDrop.default_scale = 0.1f;
            addHorseDrop.action_landed = new DropsAction(NewActions.action_give_Horse);
            AssetManager.drops.add(addHorseDrop);

            GodPower addItems = new()
            {
                id = "Item_drop_K",
                holdAction = true,
                showToolSizes = true,
                unselectWhenWindow = true,
                name = "Item_drop_K",
                dropID = "addItems",
                fallingChance = 0.01f,
                click_power_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("spawnDrops", pTile, pPower); }),
                click_power_brush_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("loopWithCurrentBrushPower", pTile, pPower); })
            };
            AssetManager.powers.add(addItems);

            GodPower addHorse = new()
            {
                id = "Horse_drop",
                holdAction = true,
                showToolSizes = true,
                unselectWhenWindow = true,
                name = "Horse_drop",
                dropID = "addHorse",
                fallingChance = 0.01f,
                click_power_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("spawnDrops", pTile, pPower); }),
                click_power_brush_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("loopWithCurrentBrushPower", pTile, pPower); })
            };
            AssetManager.powers.add(addHorse);

        }




        public static void initBuildingPower(string powerID, string dropID)
        {
            GodPower buildingPower = new()
            {
                id = powerID,
                name = powerID,
                rank = PowerRank.Rank3_good
            };
            buildingPower.select_button_action = (PowerButtonClickAction)Delegate.Combine(
                buildingPower.select_button_action,
                new PowerButtonClickAction(NewActions.selectBuildingPower)
            );
            buildingPower.click_power_action = new PowerAction(NewActions.action_spawn_building);
            AssetManager.powers.add(buildingPower);
        }
        private static GodPower createDropPower(string id, string dropID, DropsAction call)
        {
            DropAsset warriorDrop = AssetManager.drops.clone(dropID, "blessing");
            warriorDrop.action_landed = new DropsAction(call);

            GodPower warriorPower = AssetManager.powers.clone(id, SD.blessing);
            warriorPower.name = id;
            warriorPower.dropID = dropID;
            warriorPower.fallingChance = 0.01f;
            warriorPower.click_power_brush_action = new PowerAction(action_Drop);
            warriorPower.click_power_action = new PowerAction(action_1);
            return warriorPower;
        }
    }
}