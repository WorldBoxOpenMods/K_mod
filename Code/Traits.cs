using System;
using ReflectionUtility;
using System.Collections.Generic;

namespace K_mod
{
    class Traits
    {
        [Obsolete]
        public static void init()
        {
            ActorTrait 防御 = new()
            {
                id = "防御",
                path_icon = "ui/Icons/traits/防御",
                birth = 0.2f,
                group_id = trait_group.kmod
            };
            AssetManager.traits.add(防御);
            addTraitToLocalizedLibrary("cz", 防御.id, "组织防御", "防御");
            addTraitToLocalizedLibrary("en", 防御.id, "defense", "defense");
            PlayerConfig.unlockTrait(防御.id);

            ActorTrait 经济 = new()
            {
                id = "经济",
                path_icon = "ui/Icons/traits/经济",
                birth = 0.2f,
                group_id = trait_group.kmod
            };
            AssetManager.traits.add(经济);
            addTraitToLocalizedLibrary("cz", 经济.id, "调控经济", "经济");
            addTraitToLocalizedLibrary("en", 经济.id, "economy", "economy");
            PlayerConfig.unlockTrait(经济.id);

            ActorTrait 军备 = new()
            {
                id = "军备",
                path_icon = "ui/Icons/traits/军备",
                birth = 0.2f,
                group_id = trait_group.kmod
            };
            AssetManager.traits.add(军备);
            addTraitToLocalizedLibrary("cz", 军备.id, "掌管武器制作和分配", "军备");
            addTraitToLocalizedLibrary("en", 军备.id, "economy", "economy");
            PlayerConfig.unlockTrait(军备.id);

            ActorTrait 开采 = new()
            {
                id = "开采",
                path_icon = "ui/Icons/traits/开采",
                // 开采.birth = 0.2f;
                group_id = trait_group.kmod
            };
            AssetManager.traits.add(开采);
            addTraitToLocalizedLibrary("cz", 开采.id, "开采矿石", "开采");
            addTraitToLocalizedLibrary("en", 开采.id, "defense", "defense");
            // PlayerConfig.unlockTrait(开采.id); 

            ActorTrait 粮食 = new()
            {
                id = "粮食",
                path_icon = "ui/Icons/traits/粮食",
                // 粮食.birth = 0.2f;
                group_id = trait_group.kmod
            };
            AssetManager.traits.add(粮食);
            addTraitToLocalizedLibrary("cz", 粮食.id, "屯田", "田地");
            addTraitToLocalizedLibrary("en", 粮食.id, "economy", "economy");
            // PlayerConfig.unlockTrait(粮食.id); 

            ActorTrait 石料 = new()
            {
                id = "石料",
                path_icon = "ui/Icons/traits/石料",
                // 石料.birth = 0.2f;
                group_id = trait_group.kmod
            };
            AssetManager.traits.add(石料);
            addTraitToLocalizedLibrary("cz", 石料.id, "石料", "石料");
            addTraitToLocalizedLibrary("en", 石料.id, "economy", "economy");
            // PlayerConfig.unlockTrait(石料.id); 

            ActorTrait Tame = new()
            {
                id = "驯服",
                path_icon = "icon_ZAR"
            };
            AssetManager.traits.add(Tame);
            addTraitToLocalizedLibrary("cz", Tame.id, "驯服", "驯服");
            addTraitToLocalizedLibrary("en", Tame.id, "Tame", "Tame");
            PlayerConfig.unlockTrait("驯服");

            ActorTrait canTame = new()
            {
                id = "可驯服",
                path_icon = "icon_ZAR"
            };
            AssetManager.traits.add(canTame);
            addTraitToLocalizedLibrary("cz", canTame.id, "可驯服", "可驯服");
            addTraitToLocalizedLibrary("en", canTame.id, "canTame", "canTame");
            PlayerConfig.unlockTrait("可驯服");
            canTame.action_special_effect = (WorldAction)Delegate.Combine(canTame.action_special_effect, new WorldAction(canTameEffect));
        }

        [Obsolete]
        public static bool canTameEffect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor horse = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
            if (horse == null || !horse.isAlive()) { return false; }
            if (Main.Horse_rider.ContainsKey(horse) || Main.Horse.Contains(horse)) { { return false; } }
            if (Main.Rider_horse.ContainsKey(horse) || Main.Rider_z.ContainsKey(horse) || Main.Rider_x.ContainsKey(horse)
            || Main.Rider.Contains(horse)) { { return false; } }
            World.world.getObjectsInChunks(pTile, 4, MapObjectType.Actor);
            for (int i = 0; i < World.world.temp_map_objects.Count; i++)
            {
                Actor a = (Actor)World.world.temp_map_objects[i];
                if (!(a == pTarget.a) && a.asset.unit && a != null && a.city != null && !a.kingdom.isEnemy(horse.kingdom) && a.asset.race != "Pig")
                {
                    if (Main.Rider_horse.ContainsKey(a) || Main.Rider_z.ContainsKey(a) || Main.Rider_x.ContainsKey(a)
                    || Main.Rider.Contains(a)) { continue; }
                    if (Main.Horse_rider.ContainsKey(a) || Main.Horse.Contains(a)) { continue; }
                    a.goTo(horse.currentTile, true, true);
                    // horse.setKingdom(a.kingdom);
                    if (Toolbox.randomChance(0.1f))
                    {
                        break;
                    }
                    horse.removeTrait("peaceful");
                    horse.addTrait("驯服");
                    Main.Mount_horse(a, horse, 0.6f);
                    break;
                }

            }
            return true;
        }

        [Obsolete]
        private static void addTraitToLocalizedLibrary(string pLanguage, string id, string description, string name)
        {
            string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
            if (language is not "en" and not "ch" and not "cz")
            {
                pLanguage = "en";
            }
            if (pLanguage == language)
            {
                Dictionary<string, string> localizedText = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText") as Dictionary<string, string>;
                localizedText.Add("trait_" + id, name);
                localizedText.Add("trait_" + id + "_info", description);
            }
        }
    }
}