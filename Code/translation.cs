using ReflectionUtility;
using System.Collections.Generic;
using NCMS.Utils;

namespace K_mod
{
    class translation
    {
        
        public static void init()
        {
            string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
            if (language == "cz")
            {
                //文化
                addCultureTechToLocalizedLibrary("plateArmor_technology", "他们发明了板甲", "板甲");
                addCultureTechToLocalizedLibrary("Catapultfactory_tec1", "将石头与车结合，这将是主宰战场的工具", "投石车工坊1级");
                addCultureTechToLocalizedLibrary("Ballistafactory_tec1", "将弓箭与车结合，这将是主宰战场的工具", "弩车工坊1级");
                addCultureTechToLocalizedLibrary("Catapultfactory_tec2", "将石头与车结合，这将是主宰战场的工具", "投石车工坊2级");
                addCultureTechToLocalizedLibrary("Ballistafactory_tec2", "将弓箭与车结合，这将是主宰战场的工具", "弩车工坊2级");
                addCultureTechToLocalizedLibrary("AW_hufuqishe", "胡服骑射", "胡服骑射");
                //item
                addItemsToLocalizedLibrary("armour1", "无臂铁铠");
                addItemsToLocalizedLibrary("armour2", "铁铠");
                addItemsToLocalizedLibrary("armour3", "将军铠甲");
                addItemsToLocalizedLibrary("chainMail1", "锁子甲");
                addItemsToLocalizedLibrary("leatherArmor1", "普通皮甲");
                addItemsToLocalizedLibrary("leatherArmor2", "高级皮甲");
                addItemsToLocalizedLibrary("plateArmor1", "板甲 型号1");
                addItemsToLocalizedLibrary("plateArmor2", "板甲 型号2");
                addItemsToLocalizedLibrary("mainzGladius", "美因茨短剑");
                addItemsToLocalizedLibrary("gladius", "罗马短剑");
                addItemsToLocalizedLibrary("Arabic_Scimitar", "弯刀");
                addItemsToLocalizedLibrary("crossbow", "弩");
                addItemsToLocalizedLibrary("RomanHelmet", "罗马头盔");
                addItemsToLocalizedLibrary("armorPiercingCrossbow", "破甲弩");
                addItemsToLocalizedLibrary("RomanJavelin", "罗马标枪");
                addItemsToLocalizedLibrary("CrutchGun", "拐子铳");
                addItemsToLocalizedLibrary("ThreeEyeBlunderbuss", "三眼铳");
                addItemsToLocalizedLibrary("YongleHandGun", "手铳");
                //effect
                easyTranslate("breakingArmor", "破甲");
                //race
                easyTranslate("unit_Rome", "罗马人");
                easyTranslate("baby_Rome", "罗马小孩");
                easyTranslate("unit_Arab", "阿拉伯人");
                easyTranslate("baby_Arab", "阿拉伯小孩");
                easyTranslate("unit_Russia", "斯拉夫人");
                easyTranslate("baby_Russia", "斯拉夫孩");
                easyTranslate("Catapult", "投石车");
                easyTranslate("Ballista", "弩车");
                easyTranslate("horse", "马");
                //button
                ButtonTranslate("spawn_Rome", "罗马人", "在意大利半岛上他们走向荣耀");
                ButtonTranslate("spawn_Arab", "阿拉伯人", "");
                ButtonTranslate("Catapultfactory_drop", "投石车工坊", "投石车工坊组装中...");
                ButtonTranslate("Ballistafactory_drop", "弩车工坊", "弩车工坊组装中...");
                ButtonTranslate("EquipmentAddition", "装备编辑", "选择要撒下的装备");
                ButtonTranslate("Item_drop_K", "装备添加", "为小人撒上装备");
                ButtonTranslate("Horse_drop", "马匹添加", "成为骑兵...");
            }
            else if (language == "ch")
            {
                //文化                
                addCultureTechToLocalizedLibrary("plateArmor_technology", "他們發明了板甲", "板甲");
                addCultureTechToLocalizedLibrary("Catapultfactory_tec1", "將石頭與車結合，這將是主宰戰場的工具", "投石車工坊1級");
                addCultureTechToLocalizedLibrary("Ballistafactory_tec1", "將弓箭與車結合，這將是主宰戰場的工具", "弩車工坊1級");
                addCultureTechToLocalizedLibrary("Catapultfactory_tec2", "將石頭與車結合，這將是主宰戰場的工具", "投石車工坊2級");
                addCultureTechToLocalizedLibrary("Ballistafactory_tec2", "將弓箭與車結合，這將是主宰戰場的工具", "弩車工坊2級");
                addCultureTechToLocalizedLibrary("AW_hufuqishe", "胡服骑射", "胡服骑射");
                //item
                addItemsToLocalizedLibrary("armour1", "無臂鐵鎧");
                addItemsToLocalizedLibrary("armour2", "鐵鎧");
                addItemsToLocalizedLibrary("armour3", "將軍鐵鎧");
                addItemsToLocalizedLibrary("chainMail1", "鎖子甲");
                addItemsToLocalizedLibrary("chainMail2", "十字軍鎖子甲");
                addItemsToLocalizedLibrary("leatherArmor1", "普通皮甲");
                addItemsToLocalizedLibrary("leatherArmor2", "高級皮甲");
                addItemsToLocalizedLibrary("plateArmor1", "板甲 型號1");
                addItemsToLocalizedLibrary("plateArmor2", "板甲 型號2");
                addItemsToLocalizedLibrary("mainzGladius", "美因茨短劍");
                addItemsToLocalizedLibrary("gladius", "羅馬短劍");
                addItemsToLocalizedLibrary("Arabic_Scimitar", "弯刀");
                addItemsToLocalizedLibrary("RomanHelmet", "羅馬頭盔");
                addItemsToLocalizedLibrary("crossbow", "弩");
                addItemsToLocalizedLibrary("armorPiercingCrossbow", "破甲弩");
                addItemsToLocalizedLibrary("RomanJavelin", "羅馬標槍");
                addItemsToLocalizedLibrary("CrutchGun", "大明拐子铳");
                addItemsToLocalizedLibrary("YongleHandGun", "永乐手铳");
                //effect
                easyTranslate("breakingArmor", "破甲");
                //race
                easyTranslate("unit_Rome", "羅馬人");
                easyTranslate("baby_Rome", "羅馬小孩");
                easyTranslate("unit_Arab", "阿拉伯人");
                easyTranslate("baby_Arab", "阿拉伯小孩");
                easyTranslate("unit_Russia", "斯拉夫人");
                easyTranslate("baby_Russia", "斯拉夫孩");
                easyTranslate("Catapult", "投石車");
                easyTranslate("Ballista", "弩車");
                easyTranslate("horse", "马");

                //button
                ButtonTranslate("spawn_Rome", "羅馬人", "在義大利半島上他們走向榮耀");
                ButtonTranslate("spawn_Arab", "阿拉伯人", "");
                ButtonTranslate("Catapultfactory_drop", "投石車工坊", "投石車工坊組裝中...");
                ButtonTranslate("Ballistafactory_drop", "弩車工坊", "弩車工坊組裝中...");
                ButtonTranslate("EquipmentAddition", "装备编辑", "选择要撒下的装备");
                ButtonTranslate("Item_drop_K", "装备添加", "为小人撒上装备");
                ButtonTranslate("Horse_drop", "马匹添加", "成为骑兵...");

            }

            else
            {
                //文化
                addCultureTechToLocalizedLibrary("plateArmor_technology", "They invented the Plate Armor", "Chain mail");
                addCultureTechToLocalizedLibrary("Catapultfactory_tec1", "Combining stones with cars will be the tool that dominates the battlefield", "Catapult factory Level 1");
                addCultureTechToLocalizedLibrary("Ballistafactory_tec1", "Combining bows and arrows with vehicles will be the tool that dominates the battlefield", "Ballista factory Level 1");
                addCultureTechToLocalizedLibrary("Catapultfactory_tec2", "Combining stones with cars will be the tool that dominates the battlefield", "Catapult factory Level 2");
                addCultureTechToLocalizedLibrary("Ballistafactory_tec2", "Combining bows and arrows with vehicles will be the tool that dominates the battlefield", "Ballista factory Level 2");
                addCultureTechToLocalizedLibrary("AW_hufuqishe", "Hu Fu equestrian archery", "Hu Fu equestrian archery");
                //item
                addItemsToLocalizedLibrary("armour1", "Armless iron armor");
                addItemsToLocalizedLibrary("armour2", "iron armor");
                addItemsToLocalizedLibrary("armour3", "General iron armor");
                addItemsToLocalizedLibrary("chainMail1", "chain mail");
                addItemsToLocalizedLibrary("chainMail2", "Crusader Chain mail");
                addItemsToLocalizedLibrary("leatherArmor2", "Advanced leather armor");
                addItemsToLocalizedLibrary("leatherArmor1", "Ordinary leather armor");
                addItemsToLocalizedLibrary("plateArmor1", "Plate armor model 1");
                addItemsToLocalizedLibrary("plateArmor2", "Plate armor model 2");
                addItemsToLocalizedLibrary("mainzGladius", "Mainz gladius");
                addItemsToLocalizedLibrary("gladius", "gladius");
                addItemsToLocalizedLibrary("Arabic_Scimitar", "Arabic Scimitar");
                addItemsToLocalizedLibrary("crossbow", "crossbow");
                addItemsToLocalizedLibrary("RomanHelmet", "Roman helmet");
                addItemsToLocalizedLibrary("armorPiercingCrossbow", "Armor piercing crossbow");
                addItemsToLocalizedLibrary("RomanJavelin", "Roman Javelin");
                addItemsToLocalizedLibrary("CrutchGun", "Crutch Gun");
                addItemsToLocalizedLibrary("YongleHandGun", "Yongle Hand Gun");
                //effect
                easyTranslate("breakingArmor", "Breaking Armor");
                //actor
                easyTranslate("unit_Rome", "Roman");
                easyTranslate("baby_Rome", "Roman children");
                easyTranslate("unit_Arab", "Arab");
                easyTranslate("baby_Arab", "Arab children");
                easyTranslate("unit_Russia", "Slavs");
                easyTranslate("baby_Russia", "Slavs children");
                easyTranslate("Catapult", "Catapult");
                easyTranslate("Ballista", "Ballista");
                easyTranslate("horse", "马");
                //button
                ButtonTranslate("spawn_Rome", "Roman", "On the Italian Peninsula, they went to glory");
                ButtonTranslate("spawn_Arab", "Arab", "Arab");
                ButtonTranslate("Catapultfactory_drop", "Catapult factory", "The assembly of the catapult workshop is underway...");
                ButtonTranslate("Ballistafactory_drop", "Ballista factory", "The crossbow truck workshop is being assembled...");
                ButtonTranslate("EquipmentAddition", "item Editing", "Select the item to be dropped");
                ButtonTranslate("Item_drop_K", "item addition", "Sprinkle item on the actors");
                ButtonTranslate("Horse_drop", "Horse Addition", "Becoming a Cavalry...");

                English_name();

            }
            easyTranslate("en", "upkeep_增税收", "领导者的调控");
            easyTranslate("cz", "upkeep_增税收", "领导者的调控");
            easyTranslate("ch", "upkeep_增税收", "领导者的调控");
            easyTranslate("en", "upkeep_骑兵", "骑兵训练");
            easyTranslate("cz", "upkeep_骑兵", "骑兵训练");
            easyTranslate("ch", "upkeep_骑兵", "骑兵训练");
            easyTranslate("en", "upkeep_Bayt al-Hikma", "智慧宫");
            easyTranslate("cz", "upkeep_Bayt al-Hikma", "智慧宫");
            easyTranslate("ch", "upkeep_Bayt al-Hikma", "智慧宫");
            easyTranslate("en", "rules", "rules");
            easyTranslate("cz", "rules", "罗马民法");
            easyTranslate("ch", "rules", "罗马民法");
            addCultureTechToLocalizedLibrary("en", "Roman Civil Law", "Roman Civil Law", "维护社会稳定");
            addCultureTechToLocalizedLibrary("cz", "Roman Civil Law", "罗马民法", "维护社会稳定");
            addCultureTechToLocalizedLibrary("ch", "Roman Civil Law", "罗马民法", "维护社会稳定");
            addCultureTechToLocalizedLibrary("en", "Bayt al-Hikma", "Bayt al-Hikma", "宫廷翻译研究机构");
            addCultureTechToLocalizedLibrary("cz", "Bayt al-Hikma", "智慧宫", "宫廷翻译研究机构");
            addCultureTechToLocalizedLibrary("ch", "Bayt al-Hikma", "智慧宫", "宫廷翻译研究机构");
        }
        public static void English_name()
        {
            NameGeneratorAsset Catapult_name = AssetManager.nameGenerator.get("Catapult_name");
            Catapult_name.part_groups.Clear();
            Catapult_name.part_groups.Add("Catapult");
            Catapult_name.templates.Add("part_group");
            AssetManager.nameGenerator.add(Catapult_name);
            NameGeneratorAsset Ballista_name = AssetManager.nameGenerator.get("Ballista_name");
            Ballista_name.part_groups.Clear();
            Ballista_name.part_groups.Add("Ballista");
            Ballista_name.templates.Add("part_group");
            AssetManager.nameGenerator.add(Ballista_name);
        }

        
        public static void easyTranslate(string pLanguage, string id, string name)
        {
            string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
            if (language is not "en" and not "ch" and not "cz")
            {
                language = "en";
            }
            if (pLanguage != language)
            {
                return;
            }
            Dictionary<string, string> localizedText = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText") as Dictionary<string, string>;
            localizedText.Add(id, name);
        }

        
        public static void easyTranslateWithDescription(string pLanguage, string id, string name)
        {
            string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
            if (language is not "en" and not "ch" and not "cz")
            {
                pLanguage = "en";
            }
            if (pLanguage != language)
            {
                return;
            }
            Localization.addLocalization(id, name);
        }

        
        public static void AchievementsTranslate(string name, string localName, string localDescription, bool location = false)
        {
            ButtonTranslate(name, localName, localDescription);
            ButtonTranslate(name + "s", localName, localDescription);
        }

        
        public static void HandbookTranslate(string name, string localName, string localDescription)
        {
            ButtonTranslate(name + "s", localName, localDescription);
        }

        
        public static void TipTranslate(string name, string localName, string localDescription, string localDescription2)
        {
            Localization.AddOrSet(name, localName);
            Localization.AddOrSet(name + " Description", localDescription);
            Localization.AddOrSet(name + " Description2", localDescription2);
        }

        
        public static void SZSetTranslate(string id, string localName, string localB1, string localB1Description, string localB2, string localB2Description)
        {
            Localization.AddOrSet($"#{id}BLSZ", localName);
            Localization.AddOrSet(id + "ZILeftButton", localB1);
            Localization.AddOrSet(id + "ZILeftButton" + " Description", localB1Description);
            Localization.AddOrSet(id + "ZIRightButton", localB2);
            Localization.AddOrSet(id + "ZIRightButton" + " Description", localB2Description);
        }

        
        public static void KButtonTranslate(string name, string localName, string localDescription)
        {
            Localization.AddOrSet("GM" + name, localName);
            Localization.AddOrSet("GM" + name + " Description", localDescription);
            Localization.AddOrSet("spawn" + name, localName);
            Localization.AddOrSet("spawn" + name + " Description", localDescription);
            Localization.AddOrSet(name + "K", localName);
            Localization.AddOrSet(name + "K Description", localDescription);
        }

        
        public static void ButtonTranslate(string name, string localName, string localDescription)
        {
            Localization.AddOrSet(name, localName);
            Localization.AddOrSet(name + " Description", localDescription);
        }

        
        public static void easyTranslate(string id, string name)
        {
            Localization.AddOrSet(id, name);
        }

        
        public static void addCultureTechToLocalizedLibrary(string id, string description, string name)
        {
            Localization.AddOrSet("tech_" + id, name);
            Localization.AddOrSet("tech_info_" + id, description);
        }

        
        private static void addCultureTechToLocalizedLibrary(string pLanguage, string id, string name, string description)
        {
            string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
            if (language is not "en" and not "ch" and not "cz")
            {
                pLanguage = "en";
            }
            if (pLanguage == language)
            {
                Dictionary<string, string> localizedText = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText") as Dictionary<string, string>;
                localizedText.Add("tech_" + id, name);
                localizedText.Add("tech_info_" + id, description);
            }
        }

        
        public static void addTraitToLocalizedLibrary(string id, string description, string name)
        {
            Localization.AddOrSet("trait_" + id, name);
            Localization.AddOrSet("trait_" + id + "_info", description);
        }

        
        public static void WinTextTranslate(string id, string Text)
        {
            if (Windows.AllWindows.ContainsKey(id))
            {
                var Window = Windows.GetWindow(id);
                if (Text != Window.titleText.text)
                {
                    Window.titleText.text = Text;
                }
            }
        }

        
        private static void addItemsToLocalizedLibrary(string id, string name)
        {
            Localization.AddOrSet("item_" + id, name);
        }
    }
}