using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Audio
{
    public class LoopSfx : MonoBehaviour
    {
        [SerializeField] private AudioSource loopAudioSource;
        [SerializeField, Tooltip("Audio will fade in and out")] private float fadeSpeed = 1.5f;
        public float baseVolume = 0.75f;
        [Range(0, 1)]public float targetVolume = 1.0f;
        private float loopEndFlag = 0.1f;
        private float loopStartFlag = 0.1f;
        public bool LoopEndReached => loopAudioSource.time > loopAudioSource.clip.length * (1.0f - loopEndFlag);
        public System.Action onEndReached;

        public void SetAudioClipAndVolume(BaseSound sound) 
        {
            loopAudioSource.clip = sound.audioClip;
            baseVolume = sound.volume;
        }

        void Update()
        {
            if (!loopAudioSource.isPlaying && targetVolume > 0)
                loopAudioSource.Play();
            if (loopAudioSource.isPlaying && loopAudioSource.volume < 0.01f)
                loopAudioSource.Pause();
            if (loopAudioSource.clip && LoopEndReached)
                OnEndReached();
            loopAudioSource.volume = Mathf.Lerp(loopAudioSource.volume, targetVolume * baseVolume, fadeSpeed * Time.deltaTime);
        }

        private void OnEndReached()
        {
            loopAudioSource.time = loopAudioSource.clip.length * loopStartFlag;
        }
    }
}
