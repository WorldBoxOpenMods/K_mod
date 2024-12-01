using System;
using System.Collections.Generic;
using NCMS.Utils;
using UnityEngine;

namespace K_mod
{
    class WindowManager
    {
        public static Dictionary<string, GameObject> windowContents = new();
        public static Dictionary<string, ScrollWindow> createdWindows = new();

        
        public static void init()
        {
            newWindow("EquipmentAdditionWindow", "装备添加");
            EquipmentAdditionWindow.init();
            newWindow("EquipmentSelectionWindow", "选择");
            EquipmentSelectionWindow.init();
            newWindow("EquipmentAattributeAdditionWindow", "选择");
            EquipmentAattributeAdditionWindow.init();


            //newWindow("itemTypeWindow", "装备种类");
            // ItemTypeWindow.init();
        }

        
        private static void newWindow(string id, string title)
        {
            ScrollWindow window;
            GameObject content;
            window = Windows.CreateNewWindow(id, title);
            createdWindows.Add(id, window);

            GameObject scrollView = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/{window.name}/Background/Scroll View");
            scrollView.gameObject.SetActive(true);

            content = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/{window.name}/Background/Scroll View/Viewport/Content");
            if (content != null)
            {
                windowContents.Add(id, content);
            }
        }

        public static void updateScrollRect(GameObject content, int count, int size)
        {
            var scrollRect = content.GetComponent<RectTransform>();
            scrollRect.sizeDelta = new Vector2(0, count * size);
        }
    }
}