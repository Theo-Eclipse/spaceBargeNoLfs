using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityEngine.Audio
{
    [System.Serializable]
    public class BaseSound
    {
        public string name = "soundName";
        public float volume = 1.0f;
        public AudioClip audioClip;
    }
}
