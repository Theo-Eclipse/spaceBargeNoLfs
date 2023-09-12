using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(Button))]
    public class SelectLevelButton : MonoBehaviour
    {
        public int LevelIndexInManager = 0;
        private Button button;
        // Start is called before the first frame update
        void Start()
        {
            button.onClick.AddListener(LoadLevel);
        }

        // Update is called once per frame
        private void LoadLevel()
        {
            GameManager.instance.LoadLevel(LevelIndexInManager);
        }
    }
}
