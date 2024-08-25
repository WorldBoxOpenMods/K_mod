using System;
using System.Collections.Generic;
using NCMS.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace K_mod
{
    class EquipmentAattributeAdditionWindow : MonoBehaviour
    {
        private static GameObject contents;
        private static GameObject scrollView;
        private static Vector2 originalSize;
        public static int currentButtonID;

        
        public static void init()
        {
            contents = WindowManager.windowContents["EquipmentAattributeAdditionWindow"];
            scrollView = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/EquipmentAattributeAdditionWindow/Background/Scroll View");
            originalSize = contents.GetComponent<RectTransform>().sizeDelta;
            Button submitButton = NewUI.createBGWindowButton(
                scrollView,
                50,
                "refresh_icon",
                "EquipmentModClearButton",
                "重置",
                "重置装备修饰词条",
                clearToggleButtons
            );
            loadItemMods();
        }

        
        private static void loadItemMods()
        {
            foreach (Transform child in contents.transform)
            {
                Destroy(child.gameObject);
            }
            contents.GetComponent<RectTransform>().sizeDelta = new Vector2(0, AssetManager.items_modifiers.list.Count / 16 * originalSize.y) + originalSize;

            int index = 0;
            int indexY = 0;
            foreach (ItemAsset mod in AssetManager.items_modifiers.list)
            {
                if (PowerButtons.CustomButtons.ContainsKey($"{mod.id}_modifier_K"))
                {
                    PowerButtons.CustomButtons.Remove($"{mod.id}_modifier_K");
                }
                Sprite iconSprite = Sprites.LoadSprite($"{Mod.Info.Path}/icon.png");
                if (mod.mod_type != "mastery")
                {
                    iconSprite = Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icons.ItemIcons.{mod.id}.png");
                }
                PowerButtons.CreateButton(
                    $"{mod.id}_modifier_K",
                    iconSprite,
                    mod.id,
                    mod.id,
                    new Vector2(60 + (index * 35), -40 + (indexY * -40)),
                    ButtonType.Toggle,
                    contents.transform,
                    () => onModClick(mod)
                );
                increaseIndex(ref index, ref indexY);
            }
        }

        private static void increaseIndex(ref int index, ref int indexY)
        {
            index++;
            if (index > 4)
            {
                index = 0;
                indexY++;
            }
        }

        
        public static void openWindow(int buttonID)
        {
            currentButtonID = buttonID;
            checkToggleButtons();
            Windows.ShowWindow("EquipmentAattributeAdditionWindow");
        }

        
        private static void onModClick(ItemAsset mod)
        {
            if (!PowerButtons.GetToggleValue($"{mod.id}_modifier_K") && EquipmentAdditionWindow.itemModifiers.ContainsKey(currentButtonID.ToString()))
            {
                EquipmentAdditionWindow.itemModifiers[currentButtonID.ToString()].Remove(mod);
            }
            else if (EquipmentAdditionWindow.itemModifiers.ContainsKey(currentButtonID.ToString()))
            {
                EquipmentAdditionWindow.itemModifiers[currentButtonID.ToString()].Add(mod);
            }
            else
            {
                EquipmentAdditionWindow.itemModifiers.Add(currentButtonID.ToString(), new List<ItemAsset> { mod });
            }
        }

        
        private static void checkToggleButtons()
        {
            foreach (KeyValuePair<string, PowerButton> kv in PowerButtons.CustomButtons)
            {
                if (!kv.Key.Contains("_modifier_K"))
                {
                    continue;
                }
                string itemID = kv.Key.Remove(kv.Key.IndexOf("_modifier_K"));
                ItemAsset asset = AssetManager.items_modifiers.get(itemID);
                if (!PowerButtons.GetToggleValue(kv.Key) && !EquipmentAdditionWindow.itemModifiers[currentButtonID.ToString()].Contains(asset))
                {
                    continue;
                }
                if (PowerButtons.GetToggleValue(kv.Key) && EquipmentAdditionWindow.itemModifiers[currentButtonID.ToString()].Contains(asset))
                {
                    continue;
                }
                PowerButtons.ToggleButton(kv.Key);
            }
        }

        
        private static void clearToggleButtons()
        {
            foreach (KeyValuePair<string, PowerButton> kv in PowerButtons.CustomButtons)
            {
                if (!kv.Key.Contains("_modifier_K"))
                {
                    continue;
                }
                if (!PowerButtons.GetToggleValue(kv.Key))
                {
                    continue;
                }
                string itemID = kv.Key.Remove(kv.Key.IndexOf("_modifier_K"));
                ItemAsset asset = AssetManager.items_modifiers.get(itemID);
                EquipmentAdditionWindow.itemModifiers[currentButtonID.ToString()].Remove(asset);
                PowerButtons.ToggleButton(kv.Key);
            }
        }
    }
}