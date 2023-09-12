using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleSwitchSfx : MonoBehaviour
    {
        [SerializeField] private string soundOnName;
        [SerializeField] private string soundOffName;
        private Toggle button;
        // Start is called before the first frame update
        void Start()
        {
            button = GetComponent<Toggle>();
            button.onValueChanged.AddListener(Play);
        }
        void Play(bool isOn) => AudioManager.PlaySfx(isOn ? soundOnName : soundOffName, Vector3.zero);
        // Start is called before the first frame update

    }
}
