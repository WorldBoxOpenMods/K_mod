using System;
using System.Collections.Generic;
using NCMS.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace K_mod
{
    class EquipmentAdditionWindow : MonoBehaviour
    {
        private static GameObject contents;
        private static GameObject scrollView;
        private static Vector2 originalSize;
        private static Culture currentCulture;
        public static GameObject contentComponent;
        public static Text contentText;
        private static int buttonID = 0;
        public static string Launch_scope = "";
        public static Dictionary<string, string> itemNames = new();
        public static Dictionary<string, ItemOption> itemAssets = new();
        public static Dictionary<string, List<ItemAsset>> itemModifiers = new();

        [Obsolete]
        public static void init()
        {
            contents = WindowManager.windowContents["EquipmentAdditionWindow"];
            scrollView = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/EquipmentAdditionWindow/Background/Scroll View");
            originalSize = contents.GetComponent<RectTransform>().sizeDelta;
            VerticalLayoutGroup layoutGroup = contents.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlHeight = false;
            layoutGroup.childControlWidth = false;
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childScaleHeight = true;
            layoutGroup.childScaleWidth = true;
            layoutGroup.childAlignment = TextAnchor.UpperCenter;
            layoutGroup.spacing = 36;
            // scrollView.gameObject.SetActive(true);
            // contentComponent = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/EquipmentAdditionWindow/Background/Name").gameObject;
            // contentText = contentComponent.GetComponent<Text>();
            // string text = "ddddddddddddddddddddddddddd";
            // contentText.text = text.Replace(text, $"<color=#FFFFFF><b>{text}</b></color>");
            // contentText.supportRichText = true;
            // contentText.transform.SetParent(contents.transform);
            // contentComponent.SetActive(true);

            loadItems();
        }

        [Obsolete]
        private static void loadItems()
        {
            foreach (EquipmentType type in Enum.GetValues(typeof(EquipmentType)))
            {
                addNewItem(type);
            }
        }

        [Obsolete]
        public static void openWindow()
        {
            Windows.ShowWindow("EquipmentAdditionWindow");
        }

        [Obsolete]
        private static void addNewItem(EquipmentType type)
        {
            int currentButtonID = 0;
            currentButtonID += buttonID;
            itemModifiers.Add(currentButtonID.ToString(), new List<ItemAsset>());
            itemAssets.Add(currentButtonID.ToString(), new ItemOption());

            contents.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 150);
            GameObject itemHolder = new("ItemHolder");
            Image itemHolderImg = itemHolder.AddComponent<Image>();
            itemHolderImg.color = new Color(0f, 0f, 0f, 0.01f);
            itemHolder.transform.SetParent(contents.transform);
            itemNames.Add(currentButtonID.ToString(), "");

            PowerButton itemTypeButton = PowerButtons.CreateButton(
                $"Item_type_K_{currentButtonID}",
                Sprites.LoadSprite($"{Mod.Info.Path}/EmbededResources/items.png"),
                $"{type}类型装备",
                "为你的装备设置好类型",
                new Vector2(-200, -100),
                ButtonType.Click,
                itemHolder.transform,
                () => EquipmentSelectionWindow.openWindow(currentButtonID)
            );
            itemTypeButton.gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
            itemTypeButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);

            PowerButton itemModButton = PowerButtons.CreateButton(
                $"Item_modifiers_K_{currentButtonID}",
                Sprites.LoadSprite($"{Mod.Info.Path}/EmbededResources/items.png"),
                "装备词条编辑器",
                "为你的武器选择修饰词条",
                new Vector2(-80, -100),
                ButtonType.Click,
                itemHolder.transform,
                () => EquipmentAattributeAdditionWindow.openWindow(currentButtonID)
            );
            itemModButton.gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
            itemModButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);

            PowerButton itemActiveButton = PowerButtons.CreateButton(
                $"Item_active_K_{currentButtonID}",
                Sprites.LoadSprite($"{Mod.Info.Path}/EmbededResources/items.png"),
                "激活装备",
                "撒给小人（默认开启）",
                new Vector2(40, -100),
                ButtonType.Toggle,
                itemHolder.transform,
                () => toggleItemActive(currentButtonID.ToString())
            );
            itemActiveButton.gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
            itemActiveButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);

            PowerButton itemActiveButton2 = PowerButtons.CreateButton(
                $"Item_city_active_K_{currentButtonID}",
                Sprites.LoadSprite($"{Mod.Info.Path}/EmbededResources/itmecity.png"),
                "激活装备-城市",
                "装备将把范围辐射到整个城市",
                new Vector2(160, -100),
                ButtonType.Toggle,
                itemHolder.transform,
                () => toggleItemCityActive(currentButtonID.ToString())
            );
            itemActiveButton2.gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
            itemActiveButton2.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);
            buttonID++;
        }

        private static void toggleItemActive(string id)
        {
            if (itemAssets.ContainsKey(id))
            {
                itemAssets[id].active = true;
            }
            Launch_scope = "people";
            // PowerButtons.ToggleButton($"Item_city_active_K_{id.ToString()}");
        }

        [Obsolete]
        private static void toggleItemCityActive(string id)
        {
            if (itemAssets.ContainsKey(id))
            {
                itemAssets[id].active = PowerButtons.GetToggleValue($"Item_city_active_K_{id}");
            }
            Launch_scope = "city";
            // PowerButtons.ToggleButton($"Item_active_K_{id.ToString()}");
        }


    }

    [Serializable]
    public class ItemOption
    {
        public ItemAsset asset;
        public bool active;
        public string material;
        public int id;
    }
}