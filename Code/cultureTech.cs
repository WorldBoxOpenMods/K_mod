using System.Collections.Generic;

namespace K_mod
{
    class cultureTech
    {
        public static void init()
        {
            CultureTechAsset plateArmor_technology = new()
            {
                id = "plateArmor_technology",
                required_level = 10,
                priority = true,
                type = TechType.Common,
                requirements = List.Of<string>(new string[] { "material_iron" }),
                path_icon = "tech/icon_plateArmor_technology"
            };
            AssetManager.culture_tech.add(plateArmor_technology);

            CultureTechAsset Catapultfactory_tec1 = new()
            {
                id = "Catapultfactory_tec1",
                required_level = 20,
                priority = true,
                type = TechType.Common,
                requirements = new List<string>(),
                path_icon = "tech/Catapultfactory"
            };
            AssetManager.culture_tech.add(Catapultfactory_tec1);

            CultureTechAsset Ballistafactory_tec1 = new()
            {
                id = "Ballistafactory_tec1",
                required_level = 20,
                priority = true,
                type = TechType.Common,
                requirements = new List<string>(),
                path_icon = "tech/Ballistafactory"
            };
            AssetManager.culture_tech.add(Ballistafactory_tec1);

            // CultureTechAsset Catapultfactory_tec2 = new CultureTechAsset
            // {
            //     id = "Catapultfactory_tec2",
            //     required_level = 30,
            //     priority = true,
            //     type = TechType.Common,
            //     requirements = new List<string>()
            // };
            // Catapultfactory_tec2.requirements.Add("Catapultfactory_tec1");
            // Catapultfactory_tec2.path_icon = "tech/Catapultfactory";
            // AssetManager.culture_tech.add(Catapultfactory_tec2);

            // CultureTechAsset Ballistafactory_tec2 = new CultureTechAsset
            // {
            //     id = "Ballistafactory_tec2",
            //     required_level = 30,
            //     priority = true,
            //     type = TechType.Common,
            //     requirements = new List<string>()
            // };
            // Ballistafactory_tec2.requirements.Add("Ballistafactory_tec1");
            // Ballistafactory_tec2.path_icon = "tech/Ballistafactory";
            // AssetManager.culture_tech.add(Ballistafactory_tec2);

            CultureTechAsset t = new()
            {
                id = "AW_hufuqishe",
                path_icon = "tech/hufuqishe",
                required_level = 23,
                priority = true,
                type = TechType.Rare
            };
            t.stats.bonus_max_army.add(0.1f);
            t.stats.bonus_damage.add(0.05f);
            AssetManager.culture_tech.add(t);

            //可以进一步深化
            CultureTechAsset Law = new()
            {
                id = "Roman Civil Law",
                path_icon = "tech/civil_law",
                required_level = 25,
                priority = true,
                type = TechType.Rare
            };
            Law.stats.culture_spread_speed.add(-0.5f);
            Law.stats.bonus_max_cities.add(1f);
            AssetManager.culture_tech.add(Law);

            CultureTechAsset Bayt_al_Hikma = new()
            {
                id = "Bayt al-Hikma",
                path_icon = "tech/civil_law",
                required_level = 25,
                priority = true,
                type = TechType.Rare
            };
            Bayt_al_Hikma.stats.knowledge_gain.add(0.5f);
            AssetManager.culture_tech.add(Bayt_al_Hikma);

            LoyaltyAsset rule = new()
            {
                id = "rule",
                translation_key = "rules",
                calc = delegate (City pCity)
                {
                    if (pCity.kingdom.king == null)
                    {
                        return 0;
                    }
                    if (pCity.getCulture() != null)
                    {
                        Culture culture = pCity.getCulture();
                        if (pCity.getCulture().hasTech("Roman Civil Law"))
                        {
                            int yearsSince = World.world.getYearsSince(pCity.kingdom.data.timestamp_king_rule);
                            int num = 5;
                            int num2 = 40;
                            int num3 = 25;
                            if (yearsSince < num)
                            {
                                return num3;
                            }
                            if (yearsSince > num2)
                            {
                                return num2 + num3;
                            }
                            return yearsSince + num3;
                        }
                    }
                    return 0;
                }
            };
            AssetManager.loyalty_library.add(rule);
        }
    }
}