using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using NCMS.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using ReflectionUtility;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using HarmonyLib;
using System.Reflection;
using System.Reflection.Emit;
using K_mod;
using ai.behaviours;


namespace K_mod
{

    class RacesLibrary
    {
        internal static List<string> defaultRaces = new(){
            "dwarf","Arab","elf","orc","human",
        };
        internal static List<string> human = new(){
            "human"
        };
        internal static List<string> additionalRaces = new(){
            "Rome","Arab","Russia"
        };
        private static Race tRace;
        internal void init()
        {


            Race Rome = AssetManager.raceLibrary.clone("Rome", "human");

            tRace = Rome;
            Rome.civ_baseCities = 5;
            Rome.civ_base_army_mod = 1.5f;
            Rome.civ_base_zone_range = 15;
            //Rome.build_order_id = "Rome";
            // Rome.color = Toolbox.makeColor("#548CFE");
            Rome.build_order_id = "kingdom_base";
            Rome.path_icon = "ui/Icons/iconRome";
            Rome.nameLocale = "Rome";
            Rome.banner_id = "human";
            Rome.main_texture_path = "races/Rome/";
            addraceToLocalizedLibrary("Rome", "Rome");




            Rome.name_template_city = "human_city";
            Rome.name_template_kingdom = "human_kingdom";
            Rome.name_template_culture = "human_culture";
            Rome.name_template_clan = "human_clan";
            Rome.hateRaces = List.Of<string>(new string[]
            {
                SK.orc,SK.dwarf,SK.elf
            });
            Rome.production = new string[] { "bread", "pie", "tea" };
            Rome.skin_citizen_male = List.Of<string>(new string[] {
            "unit_male_1",
            "unit_male_2",
            "unit_male_3",
            "unit_male_4",
            "unit_male_5",
            "unit_male_6",
            "unit_male_7",
            "unit_male_8",
            "unit_male_9",
            "unit_male_10"});//"unit_male_0",
            Rome.skin_citizen_female = List.Of<string>(new string[] {
            "unit_female_1",
            "unit_female_2",
            "unit_female_3",
            "unit_female_4",
            "unit_female_5",
            "unit_female_6",
            "unit_female_7",
            "unit_female_8",
            "unit_female_9",
            "unit_female_10"});
            Rome.skin_warrior = List.Of<string>(new string[] {
            "unit_warrior_3",
            "unit_warrior_1",
            "unit_warrior_2",
            "unit_warrior_4",
            "unit_warrior_5",
            "unit_warrior_6",
            "unit_warrior_7",
            "unit_warrior_8",
            "unit_warrior_9",
            "unit_warrior_10"});
            Rome.nomad_kingdom_id = $"nomads_{Rome.id}";
            AssetManager.raceLibrary.CallMethod("setPreferredStatPool", "diplomacy#1,warfare#1,stewardship#0,intelligence#1");
            AssetManager.raceLibrary.CallMethod("setPreferredFoodPool", "bread#1,fish#1,tea#1");
            AssetManager.raceLibrary.CallMethod("addPreferredWeapon", "sword", 10);
            AssetManager.raceLibrary.CallMethod("addPreferredWeapon", "bow", 5);
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_tent, "tent_Rome");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_0, "house_Rome");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_1, "1house_Rome");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_2, "2house_Rome");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_3, "3house_Rome");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_4, "4house_Rome");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_5, "5house_Rome");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_hall_0, "hall_Rome");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_hall_1, "1hall_Rome");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_hall_2, "2hall_Rome");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_windmill_0, SB.windmill_dwarf_0);
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_windmill_1, SB.windmill_dwarf_1);
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_docks_0, "fishing_docks_Rome");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_docks_1, "docks_Rome");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_watch_tower, "watch_tower_Rome");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_barracks, "barracks_Rome");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_temple, "temple_Rome");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_statue, SB.statue);
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_well, SB.well);
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_bonfire, SB.bonfire);
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_mine, SB.mine);
            Rome.preferred_weapons.Add("crossbow");
            Rome.preferred_weapons.Add("gladius");
            Rome.preferred_weapons.Add("mainzGladius");
            Rome.preferred_weapons.Add("RomanHelmet");
            Rome.preferred_weapons.Add("plateArmor2");
            Rome.preferred_weapons.Add("plateArmor1");
            Rome.preferred_weapons.Add("leatherArmor2");
            Rome.preferred_weapons.Add("leatherArmor1");
            Rome.preferred_weapons.Add("chainMail2");
            Rome.preferred_weapons.Add("chainMail1");
            Rome.preferred_weapons.Add("armour3");
            Rome.preferred_weapons.Add("armour2");
            Rome.preferred_weapons.Add("armour1");
            Rome.preferred_weapons.Add("armorPiercingCrossbow");
            Rome.preferred_weapons.Add("RomanJavelin");



            Race Arab = AssetManager.raceLibrary.clone("Arab", "human");

            tRace = Arab;
            Arab.civ_baseCities = 3;
            Arab.civ_base_army_mod = 1.6f;
            Arab.build_order_id = "kingdom_base";
            Arab.path_icon = "ui/Icons/iconArab";
            Arab.nameLocale = "Arab";
            Arab.banner_id = "human";
            Arab.main_texture_path = "races/Arab/";
            addraceToLocalizedLibrary("Arab", "Arab");

            Arab.name_template_city = "dwarf_city";
            Arab.name_template_kingdom = "dwarf_kingdom";
            Arab.name_template_culture = "dwarf_culture";
            Arab.name_template_clan = "human_clan";
            Arab.hateRaces = List.Of<string>(new string[]
            {
                SK.orc,SK.dwarf,SK.elf
            });
            Arab.production = new string[] { "bread", "pie", "tea" };
            Arab.skin_citizen_male = List.Of<string>(new string[] {
            "unit_male_1",
            "unit_male_2",
            "unit_male_3",
            "unit_male_4",
            "unit_male_5",
            "unit_male_6",
            "unit_male_7",
            "unit_male_8",
            "unit_male_9",
            "unit_male_10"});//"unit_male_0",
            Arab.skin_citizen_female = List.Of<string>(new string[] {
             "unit_female_1",
            "unit_female_2",
            "unit_female_3",
            "unit_female_4",
            "unit_female_5",
            "unit_female_6",
            "unit_female_7",
            "unit_female_8",
            "unit_female_9",
            "unit_female_10"});
            Arab.skin_warrior = List.Of<string>(new string[] {
            "unit_warrior_3",
              "unit_warrior_1",
            "unit_warrior_2",
            "unit_warrior_4",
            "unit_warrior_5",
            "unit_warrior_6",
            "unit_warrior_7",
            "unit_warrior_8",
            "unit_warrior_9",
            "unit_warrior_10"});
            Arab.nomad_kingdom_id = $"nomads_{Arab.id}";
            AssetManager.raceLibrary.CallMethod("setPreferredStatPool", "diplomacy#0,warfare#1,stewardship#1,intelligence#0");
            AssetManager.raceLibrary.CallMethod("setPreferredFoodPool", "bread#1,fish#1,tea#1");
            AssetManager.raceLibrary.CallMethod("addPreferredWeapon", "sword", 10);
            AssetManager.raceLibrary.CallMethod("addPreferredWeapon", "bow", 15);
            AssetManager.raceLibrary.CallMethod("addPreferredWeapon", "Arabic_Scimitar", 10);
            AssetManager.raceLibrary.CallMethod("addPreferredWeapon", "crossbow", 10);
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_tent, "tent_Arab");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_0, "house_Arab");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_1, "1house_Arab");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_2, "2house_Arab");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_3, "3house_Arab");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_4, "4house_Arab");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_5, "5house_Arab");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_hall_0, "hall_Arab");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_hall_1, "1hall_Arab");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_hall_2, "2hall_Arab");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_windmill_0, SB.windmill_dwarf_0);
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_windmill_1, SB.windmill_dwarf_1);
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_docks_0, "fishing_docks_Arab");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_docks_1, "docks_Arab");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_watch_tower, "watch_tower_Arab");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_barracks, "barracks_Arab");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_temple, "temple_Arab");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_statue, SB.statue);
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_well, SB.well);
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_bonfire, SB.bonfire);
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_mine, SB.mine);

            Race Russia = AssetManager.raceLibrary.clone("Russia", "human");

            tRace = Russia;
            Russia.civ_baseCities = 3;
            Russia.civ_base_army_mod = 1.6f;
            Russia.build_order_id = "kingdom_base";
            Russia.path_icon = "ui/Icons/iconRussia";
            Russia.nameLocale = "Russia";
            Russia.banner_id = "human";
            Russia.main_texture_path = "races/Russia/";
            addraceToLocalizedLibrary("Russia", "Russia");

            Russia.name_template_city = "human_city";
            Russia.name_template_kingdom = "human_kingdom";
            Russia.name_template_culture = "human_culture";
            Russia.name_template_clan = "human_clan";
            Russia.hateRaces = List.Of<string>(new string[]
            {
                SK.orc,SK.dwarf,SK.elf,"Rome","Arab"
            });
            Russia.production = new string[] { "bread", "pie", "tea" };
            Russia.skin_citizen_male = List.Of<string>(new string[] {
            "unit_male_1",
            "unit_male_2",
            "unit_male_3",
            "unit_male_4",
            "unit_male_5",
            "unit_male_6",
            "unit_male_7",
            "unit_male_8",
            "unit_male_9",
            "unit_male_10"
            });//"unit_male_0",
            Russia.skin_citizen_female = List.Of<string>(new string[] {
            "unit_female_1",
            "unit_female_2",
            "unit_female_3",
            "unit_female_4",
            "unit_female_5",
            "unit_female_6",
            "unit_female_7",
            "unit_female_8",
            "unit_female_9",
            "unit_female_10"
            });
            Russia.skin_warrior = List.Of<string>(new string[] {
            "unit_warrior_3",
            "unit_warrior_1",
            "unit_warrior_2",
            "unit_warrior_4",
            "unit_warrior_5",
            "unit_warrior_6",
            "unit_warrior_7",
            "unit_warrior_8",
            "unit_warrior_9",
            "unit_warrior_10"
            });
            Russia.nomad_kingdom_id = $"nomads_{Russia.id}";
            AssetManager.raceLibrary.CallMethod("setPreferredStatPool", "diplomacy#1,warfare#1,stewardship#0,intelligence#0");
            AssetManager.raceLibrary.CallMethod("setPreferredFoodPool", "bread#1,fish#1,tea#1");
            AssetManager.raceLibrary.CallMethod("addPreferredWeapon", "sword", 10);
            AssetManager.raceLibrary.CallMethod("addPreferredWeapon", "bow", 15);
            AssetManager.raceLibrary.CallMethod("addPreferredWeapon", "crossbow", 10);
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_tent, "tent_Russia");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_0, "house_Russia");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_1, "1house_Russia");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_2, "2house_Russia");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_3, "3house_Russia");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_4, "4house_Russia");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_5, "5house_Russia");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_hall_0, "hall_Russia");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_hall_1, "1hall_Russia");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_hall_2, "2hall_Russia");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_windmill_0, SB.windmill_human_0);
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_windmill_1, SB.windmill_human_1);
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_docks_0, "fishing_docks_Russia");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_docks_1, "docks_Russia");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_watch_tower, "watch_tower_Russia");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_barracks, "barracks_Russia");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_temple, "temple_Russia");
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_statue, SB.statue);
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_well, SB.well);
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_bonfire, SB.bonfire);
            AssetManager.raceLibrary.addBuildingOrderKey(SB.order_mine, SB.mine);
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_tent, SB.tent_human);
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_0, SB.house_human_0);
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_1, SB.house_human_1);
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_2, SB.house_human_2);
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_3, SB.house_human_3);
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_4, SB.house_human_4);
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_house_5, SB.house_human_5);
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_hall_0, "hall_Russia");
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_hall_1, "1hall_Russia");
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_hall_2, "2hall_Russia");
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_windmill_0, SB.windmill_human_0);
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_windmill_1, SB.windmill_human_1);
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_docks_0, "fishing_docks_Russia");
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_docks_1, "docks_Russia");
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_watch_tower, "watch_tower_Russia");
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_barracks, "barracks_Russia");
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_temple, "temple_Russia");
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_statue, SB.statue);
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_well, SB.well);
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_bonfire, SB.bonfire);
            // AssetManager.raceLibrary.addBuildingOrderKey(SB.order_mine, SB.mine);

            foreach (Race innerRac in AssetManager.raceLibrary.list)
            {
                // var innerRac = AssetManager.raceLibrary.get(srac);
                if (innerRac.id != "Rome")
                {
                    innerRac.culture_forbidden_tech.Add("plateArmor_technology");
                }
                if (innerRac.id != "Arab" && innerRac.id != "Rome" && innerRac.id != "Xia" && innerRac.id != "Russia" && innerRac.id != "human")
                {
                    innerRac.culture_forbidden_tech.Add("Catapultfactory_tec1");
                    innerRac.culture_forbidden_tech.Add("Ballistafactory_tec1");
                    innerRac.culture_forbidden_tech.Add("Catapultfactory_tec2");
                    innerRac.culture_forbidden_tech.Add("Ballistafactory_tec2");
                }
            }
            var rac = AssetManager.raceLibrary.get("orc");
            var rac2 = AssetManager.raceLibrary.get("Arab");
            rac.culture_forbidden_tech.Add("AW_hufuqishe");
            rac2.culture_forbidden_tech.Add("AW_hufuqishe");
        }
        public static void addraceToLocalizedLibrary(string id, string text)
        {
            string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
            Dictionary<string, string> localizedText = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText") as Dictionary<string, string>;
            localizedText.Add(id, text);
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ActorAnimationLoader), "loadAnimationBoat")]
        public static bool loadAnimationBoat_Prefix(ref string pTexturePath, ActorAnimationLoader __instance)
        {
            if (pTexturePath.EndsWith("_"))
            {
                pTexturePath = pTexturePath + "human";
                return true;
            }
            foreach (string race in Main.instance.addRaces)
            {
                if (pTexturePath.Contains("Rome"))
                {
                    pTexturePath = pTexturePath.Replace(race, "Rome");
                    return true;

                }
                else if (pTexturePath.Contains(race))
                {
                    pTexturePath = pTexturePath.Replace(race, "human");
                    return true;
                }
            }
            return true;
        }





        [HarmonyPrefix]
        [HarmonyPatch(typeof(BannerLoaderClans), "create")]
        public static bool Prefix(BannerLoaderClans __instance)
        {

            if (__instance._created)
            {
                return true;
            }
            else
            {
                Debug.Log("改变clan框架");
                GameObject frame = __instance.transform.Find("Frame").gameObject;


                Image frameImage = frame.GetComponent<Image>();


                Sprite replacementSprite = Resources.Load<Sprite>("ui/Icons/clan_frame");
                frameImage.sprite = replacementSprite;


                return true;
            }
        }








        public static Sprite[] loadAllSprite(string path, bool withFolders = false)//路径
        {
            string p = $"{Main.mainPath}/EmbededResources/{path}";
            DirectoryInfo folder = new(p);
            List<Sprite> res = new();
            foreach (FileInfo file in folder.GetFiles("*.png"))
            {
                Sprite sprite = Sprites.LoadSprite($"{file.FullName}");
                sprite.name = file.Name.Replace(".png", "");
                res.Add(sprite);
            }
            foreach (DirectoryInfo cFolder in folder.GetDirectories())
            {
                foreach (FileInfo file in cFolder.GetFiles("*.png"))
                {
                    Sprite sprite = Sprites.LoadSprite($"{file.FullName}");
                    sprite.name = file.Name.Replace(".png", "");
                    res.Add(sprite);
                }
            }
            return res.ToArray();
        }
    }
}