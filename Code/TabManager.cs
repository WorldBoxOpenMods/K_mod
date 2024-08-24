using System;
using NCMS.Utils;
using UnityEngine;
using UnityEngine.UI;
using ReflectionUtility;

namespace K_mod
{
    class TabManager : MonoBehaviour
    {
        public static string buildingid = "";

        [Obsolete]
        public static void init()
        {
            createTab("Button Tab_K_mod", "Tab_K_mod", "罗马", "罗马", -362);
            loadButtons();
        }

        [Obsolete]
        public static void createTab(string buttonID, string tabID, string name, string desc, int xPos)
        {
            GameObject OtherTabButton = GameObjects.FindEvenInactive("Button_Other");
            if (OtherTabButton != null)
            {
                Localization.AddOrSet(buttonID, name);
                Localization.AddOrSet($"{buttonID} Description", desc);
                Localization.AddOrSet("K_mod_mod_creator", "Mod by 空星漫漫\nTab By: Dej#0594");
                Localization.AddOrSet(tabID, name);


                GameObject newTabButton = GameObject.Instantiate(OtherTabButton);
                newTabButton.transform.SetParent(OtherTabButton.transform.parent);
                Button buttonComponent = newTabButton.GetComponent<Button>();
                TipButton tipButton = buttonComponent.gameObject.GetComponent<TipButton>();
                tipButton.textOnClick = buttonID;
                tipButton.textOnClickDescription = $"{buttonID} Description";
                tipButton.text_description_2 = "K_mod_mod_creator";



                newTabButton.transform.localPosition = new Vector3(xPos, 49.62f);
                newTabButton.transform.localScale = new Vector3(1f, 1f);
                newTabButton.name = buttonID;

                Sprite spriteForTab = Sprites.LoadSprite($"{Mod.Info.Path}/icon.png");
                newTabButton.transform.Find("Icon").GetComponent<Image>().sprite = spriteForTab;


                GameObject OtherTab = GameObjects.FindEvenInactive("Tab_Other");
                foreach (Transform child in OtherTab.transform)
                {
                    child.gameObject.SetActive(false);
                }

                GameObject additionalPowersTab = GameObject.Instantiate(OtherTab);

                foreach (Transform child in additionalPowersTab.transform)
                {
                    if (child.gameObject.name is "tabBackButton" or "-space")
                    {
                        child.gameObject.SetActive(true);
                        continue;
                    }

                    GameObject.Destroy(child.gameObject);
                }

                foreach (Transform child in OtherTab.transform)
                {
                    child.gameObject.SetActive(true);
                }


                additionalPowersTab.transform.SetParent(OtherTab.transform.parent);
                PowersTab powersTabComponent = additionalPowersTab.GetComponent<PowersTab>();
                powersTabComponent.powerButton = buttonComponent;
                powersTabComponent.powerButtons.Clear();


                additionalPowersTab.name = tabID;
                powersTabComponent.powerButton.onClick = new Button.ButtonClickedEvent();
                powersTabComponent.powerButton.onClick.AddListener(() => tabOnClick(tabID));
                Reflection.SetField<GameObject>(powersTabComponent, "parentObj", OtherTab.transform.parent.parent.gameObject);

                additionalPowersTab.SetActive(true);
                powersTabComponent.powerButton.gameObject.SetActive(true);
            }
        }

        [Obsolete]
        public static void tabOnClick(string tabID)
        {
            GameObject AdditionalTab = GameObjects.FindEvenInactive(tabID);
            PowersTab AdditionalPowersTab = AdditionalTab.GetComponent<PowersTab>();

            AdditionalPowersTab.showTab(AdditionalPowersTab.powerButton);
        }

        [Obsolete]
        private static void loadButtons()
        {
            PowersTab collectionTab = getPowersTab("K_mod");
            int index = 0;
            int xPos = 72;
            int yPos = 18;
            int gap = 35;

            _ = PowerButtons.CreateButton(
                "spawn_Rome",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.units.iconRome.png"),
                "Rome",
                "Spawn Rome",
                new Vector2(xPos + (index * gap), yPos),
                ButtonType.GodPower,
                collectionTab.transform,
                null
            );
            index++;
            _ = PowerButtons.CreateButton(
                "spawn_Arab",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.units.iconArab.png"),
                "Arab",
                "Spawn Arab",
                new Vector2(xPos + (index * gap), yPos),
                ButtonType.GodPower,
                collectionTab.transform,
                null
            );
            index++;
            PowerButtons.CreateButton(
                "spawn_Russia",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.units.iconRussia.png"),
                "Russia",
                "Spawn Russia",
                new Vector2(xPos + (index * gap), yPos),
                ButtonType.GodPower,
                collectionTab.transform,
                null
            );
            index++;
            _ = PowerButtons.CreateButton(
                "spawn_horse",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.units.horse.png"),
                "马",
                "生成马",
                new Vector2(xPos + (index * gap), yPos),
                ButtonType.GodPower,
                collectionTab.transform,
                null
            );
            index++;
            // PowerButtons.CreateButton(
            //     "spawn_races", 
            //     Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.races.png"), 
            //     "spawn_races", 
            //     "spawn_races", 
            //     new Vector2(xPos + (index*gap), yPos), 
            //     ButtonType.Click, 
            //     collectionTab.transform, 
            //     RacesAdditionWindow.openWindow
            // );
            // index++;
            _ = PowerButtons.CreateButton(
                "Ballistafactory_drop",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.buildings.Ballistafactory.png"),
                "Rome",
                "Spawn Rome",
                new Vector2(xPos + (index * gap), yPos),
                ButtonType.GodPower,
                collectionTab.transform,
                () => setBuildingPower("Ballistafactory")
            );
            index++;
            _ = PowerButtons.CreateButton(
                "Catapultfactory_drop",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.buildings.Catapultfactory.png"),
                "Rome",
                "Spawn Rome",
                new Vector2(xPos + (index * gap), yPos),
                ButtonType.GodPower,
                collectionTab.transform,
                () => setBuildingPower("Catapultfactory")
            );
            index++;

            _ = PowerButtons.CreateButton(
                "EquipmentAddition",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.weapons.png"),
                "装备编辑器",
                "",
                new Vector2(xPos + (index * gap), yPos),
                ButtonType.Click,
                collectionTab.transform,
                EquipmentAdditionWindow.openWindow
            );
            index++;

            _ = PowerButtons.CreateButton(
                "Item_drop_K",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.weapons.png"),
                "装备添加",
                "撒下去为小人添加装备",
                new Vector2(xPos + (index * gap), yPos),
                ButtonType.GodPower,
                collectionTab.transform,
                null
            );
            index++;

            _ = PowerButtons.CreateButton(
                "Horse_drop",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.iconcavalry.png"),
                "马匹添加",
                "成为骑兵……",
                new Vector2(xPos + (index * gap), yPos),
                ButtonType.GodPower,
                collectionTab.transform,
                null
            );
            index++;

            _ = PowerButtons.CreateButton(
                "debug_K",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icons.iconDebug.png"),
                "debug",
                "debug",
                new Vector2(xPos + (index * gap), yPos),
                ButtonType.Click,
                collectionTab.transform,
                () => Windows.ShowWindow("debug")
            );
            index++;

            index = 0;
            _ = PowerButtons.CreateButton(
                "national_strength",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.经济.png"),
                "国力强化",
                "根据个人的属性给予王国不同的强化",
                new Vector2(xPos + (index * gap), yPos - gap),
                ButtonType.Toggle,
                collectionTab.transform,
                null
            );
            index++;
            _ = PowerButtons.CreateButton(
                "texture Cavalry",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.iconcavalry.png"),
                "禁用贴图骑兵",
                "将不会根据状态分配给骑兵贴图",
                new Vector2(xPos + (index * gap), yPos - gap),
                ButtonType.Toggle,
                collectionTab.transform,
                null
            );
            index++;
        }

        [Obsolete]
        private static PowersTab getPowersTab(string id)
        {
            GameObject gameObject = GameObjects.FindEvenInactive("Tab_" + id);
            return gameObject.GetComponent<PowersTab>();
        }
        private static void setBuildingPower(string buildingID)
        {
            buildingid = buildingID;
            _ = NewActions.selectBuildingPower($"{buildingID}_drop");
        }


    }
}