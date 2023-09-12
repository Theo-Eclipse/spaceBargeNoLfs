using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Audio
{
    public class SFX : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        public System.Action<SFX> onEndReached;
        public bool EndReached => (audioSource.clip.length - audioSource.time) <= 0.1f;
        public void SetClipAndPlay(BaseSound clip) 
        {
            audioSource.clip = clip.audioClip;
            audioSource.volume = clip.volume;
            audioSource.time = 0;
            audioSource.Play();
        }

        private void Update()
        {
            if (!audioSource.isPlaying) 
            {
                audioSource.Stop();
                onEndReached?.Invoke(this);
            }
        }
    }
}
