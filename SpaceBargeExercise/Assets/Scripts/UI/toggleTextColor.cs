using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(Toggle)), ExecuteAlways]
    public class toggleTextColor : MonoBehaviour
    {
        public Color isOnColor = Color.red;
        public Color isOffColor = Color.black;
        [SerializeField] private List<TMP_Text> texts;
        private Toggle toggle;
        // Start is called before the first frame update
        void Start()
        {
            toggle = GetComponent<Toggle>();
            SetTextColor(toggle.isOn);
            toggle.onValueChanged.AddListener(SetTextColor);
        }

        private void Update()
        {
            if (!Application.isEditor && Application.isPlaying)
                return;
            if (!toggle) toggle = GetComponent<Toggle>();
            SetTextColor(toggle.isOn);
        }

        private void SetTextColor(bool isOn)
        {
            foreach (var text in texts)
                if (text)// check if not null.
                    text.color = isOn ? isOnColor : isOffColor;
        }
    }
}
