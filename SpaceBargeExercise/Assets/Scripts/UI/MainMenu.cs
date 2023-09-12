using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.UI
{
    public class MainMenu : UiMenu
    {
        [Header("Menu Buttons")]
        [Space] public Button play;
        public Button settings;
        public Button exit;

        public void Init()
        {
            play.onClick.AddListener(() => GameManager.instance.LoadLevel(0));
            exit.onClick.AddListener(Application.Quit);
        }
    }
}