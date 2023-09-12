using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(Slider))]
    public class VolumeSlider : MonoBehaviour, IEndDragHandler
    {
        public string volumeParameter = "MasterVolume";
        public string testSound = "";
        private Slider slider;
        // Start is called before the first frame update
        void Start()
        {
            slider = GetComponent<Slider>();
            slider.SetValueWithoutNotify(AudioManager.GetMixerVolume(volumeParameter));
            slider.onValueChanged.AddListener(UpdateVolume);
        }

        // Update is called once per frame
        void UpdateVolume(float value)
        {
            AudioManager.SetMixerVolume(volumeParameter, value);
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            if (!string.IsNullOrEmpty(testSound))
                AudioManager.PlaySfx(testSound, Vector3.zero);
        }
    }
}
