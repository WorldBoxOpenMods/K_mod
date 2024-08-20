using System;
using System.Collections.Generic;
using NCMS.Utils;
using UnityEngine;
using ReflectionUtility;
namespace K_mod
{
    internal class k_actors
    {
        public static Dictionary<string, AttackAction> AttackAction = new();
        public static Dictionary<string, List<string>> ActorAnimationTextures = new();
        public static Dictionary<string, KActionSave> ActorAction = new();
        public static Dictionary<string, List<KAction<bool, Actor>>> ActorAnimationBool = new();
        [Obsolete]

        public void init()
        {
            ColorSetAsset Rome_default = new()

            {
                id = "Rome_default",
                shades_from = "#f6c195",
                shades_to = "#a8662f",
                is_default = true
            };
            AssetManager.skin_color_set_library.add(Rome_default);

            var RomeAsset = AssetManager.actor_library.clone("unit_Rome", "unit_human");
            RomeAsset.nameLocale = "Romes";
            RomeAsset.body_separate_part_head = true;
            RomeAsset.heads = 11;
            RomeAsset.oceanCreature = false;
            RomeAsset.nameTemplate = "human_name";
            RomeAsset.race = "Rome";
            RomeAsset.base_stats[S.max_age] = 70f;
            RomeAsset.icon = "iconRome";
            RomeAsset.color = Toolbox.makeColor("#33724D");
            RomeAsset.setBaseStats(120, 5, 60, 5, 90, 5);
            RomeAsset.fmod_spawn = "event:/SFX/UNITS/Human/HumanSpawn";
            RomeAsset.fmod_attack = "event:/SFX/UNITS/Human/HumanAttack";
            RomeAsset.fmod_idle = "event:/SFX/UNITS/Human/HumanIdle";
            RomeAsset.fmod_death = "event:/SFX/UNITS/Human/HumanDeath";
            RomeAsset.disableJumpAnimation = true;
            AssetManager.actor_library.addColorSet("Rome_default");
            AssetManager.actor_library.CallMethod("loadShadow", RomeAsset);
            AssetManager.actor_library.add(RomeAsset);
            Localization.addLocalization(RomeAsset.nameLocale, "罗马人");

            var Romebaby = AssetManager.actor_library.clone("baby_Rome", "unit_Rome");
            Romebaby.body_separate_part_head = false;
            Romebaby.body_separate_part_hands = false;
            Romebaby.take_items = false;
            Romebaby.base_stats[S.speed] = 10f;
            Romebaby.baby = true;
            Romebaby.animation_idle = "walk_1";
            Romebaby.growIntoID = "unit_Rome";
            Romebaby.fmod_spawn = "event:/SFX/UNITS/Human/HumanSpawn";
            Romebaby.fmod_attack = "event:/SFX/UNITS/Human/HumanAttack";
            Romebaby.fmod_idle = "event:/SFX/UNITS/Human/HumanIdle";
            Romebaby.fmod_death = "event:/SFX/UNITS/Human/HumanDeath";
            AssetManager.actor_library.CallMethod("addTrait", "peaceful");
            Romebaby.disableJumpAnimation = true;
            Romebaby.color_sets = RomeAsset.color_sets;
            AssetManager.actor_library.CallMethod("loadShadow", Romebaby);

            ColorSetAsset Arab_default = new()

            {
                id = "Arab_default",
                shades_from = "#dfad84",
                shades_to = "#bb7134",
                is_default = true
            };
            AssetManager.skin_color_set_library.add(Arab_default);

            var ArabAsset = AssetManager.actor_library.clone("unit_Arab", "unit_human");
            ArabAsset.nameLocale = "Arabs";
            ArabAsset.body_separate_part_head = true;
            ArabAsset.heads = 11;
            ArabAsset.oceanCreature = false;
            ArabAsset.nameTemplate = "dwarf_name";
            ArabAsset.race = "Arab";
            ArabAsset.base_stats[S.max_age] = 80f;
            ArabAsset.icon = "iconArab";
            ArabAsset.color = Toolbox.makeColor("#33724D");
            ArabAsset.setBaseStats(120, 5, 60, 0, 90, 5);
            ArabAsset.fmod_spawn = "event:/SFX/UNITS/Human/HumanSpawn";
            ArabAsset.fmod_attack = "event:/SFX/UNITS/Human/HumanAttack";
            ArabAsset.fmod_idle = "event:/SFX/UNITS/Human/HumanIdle";
            ArabAsset.fmod_death = "event:/SFX/UNITS/Human/HumanDeath";
            ArabAsset.disableJumpAnimation = true;
            AssetManager.actor_library.addColorSet("Arab_default");
            AssetManager.actor_library.CallMethod("loadShadow", ArabAsset);
            AssetManager.actor_library.add(ArabAsset);
            Localization.addLocalization(ArabAsset.nameLocale, "阿拉伯人");
            //ArabAssetraits.Add("zho");

            var Arabbaby = AssetManager.actor_library.clone("baby_Arab", "unit_Arab");
            Arabbaby.body_separate_part_head = false;
            Arabbaby.body_separate_part_hands = false;
            Arabbaby.take_items = false;
            Arabbaby.base_stats[S.speed] = 10f;
            // Arabbaby.timeToGrow = 60;
            Arabbaby.baby = true;
            Arabbaby.animation_idle = "walk_1";
            Arabbaby.growIntoID = "unit_Arab";
            Arabbaby.fmod_spawn = "event:/SFX/UNITS/Human/HumanSpawn";
            Arabbaby.fmod_attack = "event:/SFX/UNITS/Human/HumanAttack";
            Arabbaby.fmod_idle = "event:/SFX/UNITS/Human/HumanIdle";
            Arabbaby.fmod_death = "event:/SFX/UNITS/Human/HumanDeath";
            AssetManager.actor_library.CallMethod("addTrait", "peaceful");
            Arabbaby.disableJumpAnimation = true;
            Arabbaby.color_sets = ArabAsset.color_sets;
            AssetManager.actor_library.CallMethod("loadShadow", Arabbaby);

            ColorSetAsset Russia_default = new()

            {
                id = "Russia_default",
                shades_from = "#ed9752",
                shades_to = "#9b5b26",
                is_default = true
            };
            AssetManager.skin_color_set_library.add(Russia_default);
            var RussiaAsset = AssetManager.actor_library.clone("unit_Russia", "unit_human");
            RussiaAsset.nameLocale = "Russias";
            RussiaAsset.body_separate_part_head = true;
            RussiaAsset.heads = 11;
            RussiaAsset.oceanCreature = false;
            RussiaAsset.nameTemplate = "human_name";
            RussiaAsset.race = "Russia";
            RussiaAsset.base_stats[S.max_age] = 70f;
            RussiaAsset.icon = "iconRussia";
            RussiaAsset.color = Toolbox.makeColor("#33724D");
            RussiaAsset.setBaseStats(120, 5, 60, 5, 90, 5);
            RussiaAsset.fmod_spawn = "event:/SFX/UNITS/Human/HumanSpawn";
            RussiaAsset.fmod_attack = "event:/SFX/UNITS/Human/HumanAttack";
            RussiaAsset.fmod_idle = "event:/SFX/UNITS/Human/HumanIdle";
            RussiaAsset.fmod_death = "event:/SFX/UNITS/Human/HumanDeath";
            RussiaAsset.disableJumpAnimation = true;
            AssetManager.actor_library.addColorSet("Russia_default");
            AssetManager.actor_library.CallMethod("loadShadow", RussiaAsset);
            AssetManager.actor_library.add(RussiaAsset);
            Localization.addLocalization(RussiaAsset.nameLocale, "斯拉夫人");

            var Russiababy = AssetManager.actor_library.clone("baby_Russia", "unit_Russia");
            Russiababy.body_separate_part_head = false;
            Russiababy.body_separate_part_hands = false;
            Russiababy.take_items = false;
            Russiababy.base_stats[S.speed] = 10f;
            Russiababy.baby = true;
            Russiababy.animation_idle = "walk_1";
            Russiababy.growIntoID = "unit_Russia";
            Russiababy.fmod_spawn = "event:/SFX/UNITS/Human/HumanSpawn";
            Russiababy.fmod_attack = "event:/SFX/UNITS/Human/HumanAttack";
            Russiababy.fmod_idle = "event:/SFX/UNITS/Human/HumanIdle";
            Russiababy.fmod_death = "event:/SFX/UNITS/Human/HumanDeath";
            AssetManager.actor_library.CallMethod("addTrait", "peaceful");
            Russiababy.disableJumpAnimation = true;
            Russiababy.color_sets = RussiaAsset.color_sets;
            AssetManager.actor_library.CallMethod("loadShadow", Russiababy);

            // var RussiaAsset = AssetManager.actor_library.clone("unit_Russia", "unit_Rome");
            // RussiaAsset.nameLocale = "Russias";
            // RussiaAsset.body_separate_part_head = false;
            // RussiaAsset.heads = 11;
            // RussiaAsset.oceanCreature = false;
            // RussiaAsset.nameTemplate = "human_name";
            // RussiaAsset.race = "Russia";
            // RussiaAsset.fmod_spawn = "event:/SFX/UNITS/Human/HumanSpawn";
            // RussiaAsset.fmod_attack = "event:/SFX/UNITS/Human/HumanAttack";
            // RussiaAsset.fmod_idle = "event:/SFX/UNITS/Human/HumanIdle";
            // RussiaAsset.fmod_death = "event:/SFX/UNITS/Human/HumanDeath";
            // RussiaAsset.base_stats[S.max_age] = 80f;
            // RussiaAsset.icon = "iconRussias";
            // RussiaAsset.color = Toolbox.makeColor("#33724D");
            // RussiaAsset.setBaseStats(120, 5, 60, 0, 90, 5);
            // // RussiaAsset.animation_idle = "walk_0";
            // // RussiaAsset.animation_walk = "walk_0,walk_1,walk_2,walk_3";
            // // RussiaAsset.animation_swim = "swim_0,swim_1,swim_2,swim_3";
            // RussiaAsset.disableJumpAnimation = true;
            // AssetManager.actor_library.addColorSet("Russia_default");
            // AssetManager.actor_library.CallMethod("loadShadow", RussiaAsset);
            // AssetManager.actor_library.add(RussiaAsset);
            // Localization.addLocalization(RussiaAsset.nameLocale, "斯拉夫人");

            // var babyRussia = AssetManager.actor_library.clone("baby_Russia", "unit_Russia");
            // babyRussia.body_separate_part_head = false;
            // babyRussia.body_separate_part_hands = false;
            // babyRussia.take_items = false;
            // babyRussia.base_stats[S.speed] = 10f;
            // babyRussia.baby = true;
            // babyRussia.animation_idle = "walk_1";
            // babyRussia.growIntoID = "unit_Russia";
            // AssetManager.actor_library.CallMethod("addTrait", "peaceful");
            // babyRussia.disableJumpAnimation = true;
            // babyRussia.color_sets = RussiaAsset.color_sets;
            // AssetManager.actor_library.CallMethod("loadShadow", babyRussia);


            var Ballista = AssetManager.actor_library.clone("Ballista", "_mob");//new ActorAsset();
            Ballista.id = "Ballista";
            Ballista.nameLocale = "Ballista";
            Ballista.nameTemplate = "Ballista_name";
            Ballista.job = "attacker";
            Ballista.kingdom = "tame";
            Ballista.canBeKilledByStuff = true;
            Ballista.canBeKilledByLifeEraser = true;
            Ballista.canAttackBuildings = true;
            Ballista.unit = false;
            Ballista.canTurnIntoZombie = false;
            Ballista.inspect_children = false;
            Ballista.canBeMovedByPowers = true;
            Ballista.canBeHurtByPowers = true;
            Ballista.canTurnIntoZombie = true;
            Ballista.can_edit_traits = false;
            Ballista.canBeCitizen = true;
            Ballista.updateZ = true;
            Ballista.effectDamage = true;
            Ballista.canFlip = true;
            Ballista.actorSize = ActorSize.S13_Human;
            Ballista.canBeInspected = true;
            Ballista.hideOnMinimap = true;
            Ballista.needFood = false;
            Ballista.diet_meat = false;
            Ballista.deathAnimationAngle = true;
            Ballista.take_items = false;
            Ballista.use_items = false;
            Ballista.disablePunchAttackAnimation = true;
            Ballista.base_stats[S.speed] = 20f;
            Ballista.base_stats[S.health] = 90f;
            Ballista.base_stats[S.size] = 0.5f;
            Ballista.base_stats[S.critical_chance] = 0.1f;
            Ballista.base_stats[S.knockback_reduction] = 0.7f;
            Ballista.base_stats[S.knockback] = 0.3f;
            Ballista.base_stats[S.fertility] = 0f;
            Ballista.cost = new ConstructionCost(15, 1, 0, 120);
            Ballista.icon = "Ballista";
            Ballista.texture_path = "Ballista/Walk";
            Ballista.animation_idle = "walk_0";
            Ballista.animation_walk = "walk_0,walk_1";
            Ballista.animation_swim = "swim_0,swim_1";
            Ballista.sound_hit = "event:/SFX/HIT/HitWood";
            Ballista.defaultAttack = "Ballista_Arrows";
            ActorAction.Add("Ballista", new KActionSave { id = "Ballista_Shoot", interval = 0.1f, intervals = 0.1f, animation = true, textures = Main.NewTextures("actors/Ballista/Ballista_Shoot", 6) });
            AttackAction.Add("Ballista", delegate (BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
            {
                // Debug.LogError("work");
                Main.NewAction(pSelf.a, ActorAction["Ballista"]);
                return true;
            });
            AssetManager.actor_library.add(Ballista);
            Localization.addLocalization("Ballista", "弩车");
            AssetManager.actor_library.CallMethod("loadShadow", Ballista);

            var Catapult = AssetManager.actor_library.clone("Catapult", "Ballista");//new ActorAsset();
            Catapult.id = "Catapult";
            Catapult.nameLocale = "Catapult";
            Catapult.defaultAttack = "stones";
            Catapult.nameTemplate = "Catapult_name";
            Catapult.job = "attacker";
            Catapult.needFood = false;
            Catapult.diet_meat = false;
            Catapult.canTurnIntoZombie = false;
            Catapult.disablePunchAttackAnimation = true;
            Catapult.base_stats[S.critical_chance] = 0f;
            Catapult.icon = "Catapult";
            Catapult.texture_path = "Catapult/Walk";
            Catapult.animation_idle = "walk_0";
            Catapult.animation_walk = "walk_0,walk_1";
            Catapult.animation_swim = "swim_0,swim_1";
            ActorAction.Add("Catapult", new KActionSave { id = "Catapult_Shoot", interval = 0.08f, intervals = 0.07f, animation = true, textures = Main.NewTextures("actors/Catapult/Catapult_Shoot", 7) });
            AttackAction.Add("Catapult", delegate (BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
            {
                // Debug.LogError("work");
                Main.NewAction(pSelf.a, ActorAction["Catapult"]);
                return true;
            });
            AssetManager.actor_library.add(Catapult);
            Localization.addLocalization("Catapult", "投石车");
            AssetManager.actor_library.CallMethod("loadShadow", Catapult);

            var horse = AssetManager.actor_library.clone("horse", "_herbivore");
            horse.nameLocale = "horse";
            horse.kingdom = "tame";
            horse.nameTemplate = "horse_name";
            horse.nameLocale = "horse";
            horse.base_stats[S.speed] = 100f;
            // horse.race = SK.cow;
            horse.source_meat = false;
            horse.actorSize = ActorSize.S14_Cow;
            horse.shadowTexture = "unitShadow_6";
            horse.icon = "horse";
            horse.base_stats[S.damage] = 5f;
            horse.base_stats[S.health] = 100f;
            horse.base_stats[S.scale] += 0.02f;
            horse.job = "animal";
            horse.texture_path = "K_horse";
            horse.canTurnIntoZombie = false;
            horse.sound_hit = "event:/SFX/HIT/HitFlesh";
            AssetManager.actor_library.addColorSet("horse_default");
            AssetManager.actor_library.CallMethod("addTrait", "可驯服");
            AssetManager.actor_library.CallMethod("loadShadow", horse);
            Localization.addLocalization("horse", "马");

            ColorSetAsset horse_default = new()

            {
                id = "horse_default",
                shades_from = "#e6aba7",
                shades_to = "#d7928d",
                is_default = true
            };
            AssetManager.skin_color_set_library.add(horse_default);
            NameGeneratorAsset Catapult_name = new()

            {
                id = "Catapult_name"
            };
            Catapult_name.part_groups.Add("投石车");
            Catapult_name.templates.Add("part_group");
            AssetManager.nameGenerator.add(Catapult_name);
            NameGeneratorAsset Ballista_name = new()

            {
                id = "Ballista_name"
            };
            Ballista_name.part_groups.Add("弩车");
            Ballista_name.templates.Add("part_group");
            AssetManager.nameGenerator.add(Ballista_name);
            NameGeneratorAsset horse_name = new()

            {
                id = "horse_name"
            };
            horse_name.part_groups.Add("马");
            horse_name.templates.Add("part_group");
            AssetManager.nameGenerator.add(horse_name);

            LoadMultiraceUnitsSprites();

        }


        public static void NewAnimation(string id, List<string> pTextures, List<KAction<bool, Actor>> pBools)
        {
            ActorAnimationTextures.Add(id, pTextures);
            ActorAnimationBool.Add(id, pBools);
        }
        private static void LoadMultiraceUnitsSprites()
        {
            var spritesFoldersList = new List<string>(){
                "unit_child", "unit_king", "unit_leader",
                "heads_special"
            };

            var spritesHeadsList = new List<string>(){
                "heads_female", //"heads_female_santa",
                "heads_male", //"heads_male_santa",
            };

            var spritesFoldersSpecialList = new List<string>(){
                "unit_female_","unit_male_", "unit_warrior_"
            };

            foreach (var raceName in RacesLibrary.human)
            {
                var races = raceName.Split('-');

                int i = 1;
                foreach (var race in races)
                {
                    foreach (var spritesFolder in spritesFoldersList)
                    {
                        addUnitsSpritesToResources(spritesFolder, raceName, race);
                    }

                    foreach (var spritesFolder in spritesHeadsList)
                    {
                        addUnitsSpritesToResources(spritesFolder, raceName, race, true);
                    }

                    foreach (var spritesFolder in spritesFoldersSpecialList)
                    {
                        addUnitsSpritesToResources($"{spritesFolder}{i}", raceName, race);
                    }

                    i++;
                }


            }
        }

        private static void addUnitsSpritesToResources(string spritesFolder, string raceName, string race, bool isHeads = false, bool isBodies = false)
        {
            var unitsSprites = Resources.LoadAll<Sprite>($"actors/races/{race}/{spritesFolder}");
            var addedSrites = Resources.LoadAll<Sprite>($"actors/races/{raceName}/{spritesFolder}");

            var addedCount = addedSrites is null ? 0 : addedSrites.Length;

            //headIndex = headIndex == -1 ? -1 : headIndex * unitsSprites.Length;
            var headIndexOffset = !isHeads ? -1 : addedCount;
            //headIndex = !headIndex ? -1 : addedCount;

            var pivot = new Vector2(0.5f, 0);
            foreach (var sprite in unitsSprites)
            {
                var spriteName = !isHeads ? sprite.name : sprite.name.Split('_')[0];

                if (headIndexOffset != -1)
                {


                    //pivot = sprite.pivot;
                    pivot = new Vector2(0, 0);
                    var newHeadName = sprite.name.Split('_')[0];
                    var newHeadIndex = Int32.Parse(sprite.name.Split('_')[1]);

                    spriteName = $"{newHeadName}_{newHeadIndex + headIndexOffset}";

                    // if(raceName == "orc-dwarf"){
                    //     Debug.LogWarning($"path: actors/races/{race}/{spritesFolder}/{spriteName}, rect: {sprite.rect}, pivot: {sprite.pivot}");
                    // }
                }

                var texture = Resources.Load<Texture2D>($"actors/races/{race}/");
                var newSprite = Sprite.Create(texture, sprite.rect, pivot, sprite.pixelsPerUnit);
                //var newSprite = Sprite.Create(texture, sprite.rect, sprite.pivot, sprite.pixelsPerUnit);
                //newSprite.name = spriteName;

                var newPath = $"actors/races/{raceName}/{spritesFolder}/{spriteName}";



            }
        }







    }

}
