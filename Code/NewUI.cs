using System;
using NCMS.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace K_mod
{
    class NewUI : MonoBehaviour
    {
        private static GameObject avatarRef;
        private static GameObject textRef;

        public static void init()
        {
            avatarRef = GameObject.Find(
                $"Canvas Container Main/Canvas - Windows/windows/inspect_unit/Background/Scroll View/Viewport/Content/Part 1/BackgroundAvatar"
            );
        }

        public static void createActorUI(Actor actor, GameObject parent, Vector3 pos)
        {
            GameObject GO = Instantiate(avatarRef);
            GO.transform.SetParent(parent.transform);
            var avatarElement = GO.GetComponent<UiUnitAvatarElement>();
            avatarElement.show_banner_clan = true;
            avatarElement.show_banner_kingdom = true;
            avatarElement.show(actor);
            RectTransform GORect = GO.GetComponent<RectTransform>();
            GORect.localPosition = pos;
            GORect.localScale = new Vector3(1, 1, 1);


        }

        [Obsolete]
        public static Button createBGWindowButton(GameObject parent, int posY, string iconName, string buttonName, string buttonTitle,
        string buttonDesc, UnityAction call)
        {
            PowerButton button = PowerButtons.CreateButton(
                buttonName,
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icons.{iconName}.png"),
                buttonTitle,
                buttonDesc,
                new Vector2(118, posY),
                ButtonType.Click,
                parent.transform,
                call
            );

            Image buttonBG = button.gameObject.GetComponent<Image>();
            buttonBG.sprite = Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.UI.backgroundTabButton.png");
            Button buttonButton = button.gameObject.GetComponent<Button>();
            buttonBG.rectTransform.localScale = Vector3.one;

            return buttonButton;
        }

        public static Text addText(string textString, GameObject parent, int sizeFont, Vector3 pos, Vector2 addSize = default(Vector2))
        {
            textRef = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/leaderBoardWindow/Background/Title");
            GameObject textGo = Instantiate(textRef, parent.transform);
            textGo.SetActive(true);

            var textComp = textGo.GetComponent<Text>();
            textComp.fontSize = sizeFont;
            textComp.resizeTextMaxSize = sizeFont;
            var textRect = textGo.GetComponent<RectTransform>();
            textRect.position = new Vector3(0, 0, 0);
            textRect.localPosition = pos + new Vector3(0, -50, 0);
            textRect.sizeDelta = new Vector2(100, 100) + addSize;
            textGo.AddComponent<GraphicRaycaster>();
            textComp.text = textString;

            return textComp;
        }

        [Obsolete]
        public static void createTextButtonWSize(string name, string title, Vector2 pos, Color color, Transform parent, UnityAction callback, Vector2 size)
        {
            Button textButton = PowerButtons.CreateTextButton(
                name,
                title,
                pos,
                color,
                parent,
                callback
            );
            if (title.Length > 7)
            {
                textButton.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta += new Vector2(0, 10);
            }
            textButton.gameObject.GetComponent<RectTransform>().sizeDelta += size;
        }

        [Obsolete]
        public static Button createTextButton(string name, string title, Vector2 pos, Color color, Transform parent, UnityAction callback, Vector2 size)
        {
            Button textButton = PowerButtons.CreateTextButton(
                name,
                title,
                pos,
                color,
                parent,
                callback
            );
            if (title.Length > 7)
            {
                textButton.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta += new Vector2(0, 10);
            }
            textButton.gameObject.GetComponent<RectTransform>().sizeDelta += size;
            return textButton;
        }

    }
}