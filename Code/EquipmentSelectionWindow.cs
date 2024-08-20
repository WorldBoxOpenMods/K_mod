using System;
using System.Collections.Generic;
using NCMS.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace K_mod
{
    class EquipmentSelectionWindow : MonoBehaviour
    {
        private static GameObject contents;
        private static GameObject scrollView;
        private static Vector2 originalSize;
        public static int currentButtonID;
        private static List<string> wrongItems = new()
        {
            "base",
            "claws",
            "hands",
            "fire_hands",
            "jaws",
            "bite",
            "rocks",
            "snowball",
            "stones",
            "Ballista_Arrows"
        };

        public static void init()
        {
            contents = WindowManager.windowContents["EquipmentSelectionWindow"];
            scrollView = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/EquipmentSelectionWindow/Background/Scroll View");
            originalSize = contents.GetComponent<RectTransform>().sizeDelta;
            GridLayoutGroup layoutGroup = contents.AddComponent<GridLayoutGroup>();
            layoutGroup.cellSize = new Vector2(30, 30);
            layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            layoutGroup.constraintCount = 4;
            layoutGroup.childAlignment = TextAnchor.UpperCenter;
            layoutGroup.spacing = new Vector2(15, 5);
        }

        [Obsolete]
        private static void loadItemTypes()
        {
            foreach (Transform child in contents.transform)
            {
                Destroy(child.gameObject);
            }
            contents.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (AssetManager.items.list.Count / 16) * originalSize.y) + originalSize;
            contents.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

            foreach (ItemAsset item in AssetManager.items.list)
            {
                if (item.id[0] == '_' || item.equipmentType != (EquipmentType)currentButtonID || wrongItems.Contains(item.id))
                {
                    continue;
                }
                if (item.materials.Count <= 0)
                {
                    if (PowerButtons.CustomButtons.ContainsKey($"{item.id}_K"))
                    {
                        PowerButtons.CustomButtons.Remove($"{item.id}_K");
                    }
                    PowerButtons.CreateButton(
                        $"{item.id}_item_K",
                        Resources.Load<Sprite>($"ui/Icons/items/icon_{item.id}"),
                        item.id,
                        item.id,
                        new Vector2(0, 0),
                        ButtonType.Click,
                        contents.transform,
                        () => onItemClick(item)
                    );
                    continue;
                }
                foreach (string material in item.materials)
                {
                    if (PowerButtons.CustomButtons.ContainsKey($"{item.id}_item_K_{material}"))
                    {
                        PowerButtons.CustomButtons.Remove($"{item.id}_item_K_{material}");
                    }
                    Sprite pSprite = null;
                    if (material != "base")
                    {
                        pSprite = Resources.Load<Sprite>($"ui/Icons/items/icon_{item.id}_{material}");
                    }
                    else
                    {
                        pSprite = Resources.Load<Sprite>($"ui/Icons/items/icon_{item.id}");
                    }
                    PowerButtons.CreateButton(
                        $"{item.id}_item_K_{material}",
                        pSprite,
                        $"{item.id}_{material}",
                        $"{item.id}_{material}",
                        new Vector2(0, 0),
                        ButtonType.Click,
                        contents.transform,
                        () => onItemClick(item, material)
                    );
                }
            }
        }

        [Obsolete]
        public static void openWindow(int buttonID)
        {
            currentButtonID = buttonID;
            loadItemTypes();
            Windows.ShowWindow("EquipmentSelectionWindow");
        }

        [Obsolete]
        private static void onItemClick(ItemAsset asset, string material = null)
        {
            EquipmentAdditionWindow.itemAssets[currentButtonID.ToString()] = new ItemOption
            {
                asset = asset,
                active = true,
                material = material,
                id = currentButtonID
            };
            GameObject icon = PowerButtons.CustomButtons[$"Item_type_K_{currentButtonID.ToString()}"].gameObject.transform.GetChild(0).gameObject;
            string icon_path = $"ui/Icons/items/icon_{asset.id}";
            if (material != null)
            {
                icon_path = $"ui/Icons/items/icon_{asset.id}_{material}";
            }
            icon.GetComponent<Image>().sprite = Resources.Load<Sprite>(icon_path);
            Windows.ShowWindow("EquipmentAdditionWindow");
            return;
        }
    }
}