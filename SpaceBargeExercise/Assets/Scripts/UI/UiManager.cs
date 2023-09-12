using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flier.Controls;
using Flier;

namespace UnityEngine.UI
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager instance { get; private set; }
        public MainMenu mainMenu;
        public GameOrtho ortho;

        // Start is called before the first frame update
        void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            mainMenu.Init();
            mainMenu.play.onClick.AddListener(() => mainMenu.SwitchToMenu(ortho));
            ShowMain();
        }

        public void ShowMain() 
        {
            mainMenu.Show();
            ortho.Hide();
        }
    }
}
