using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource globalAudioSource;
    [SerializeField, Tooltip("Audio will fade in and out")] private float fadeTime = 0.5f;
    public float baseVolume = 0.75f;
    public bool targetPlayState = false;
    public bool EndReached => (globalAudioSource.clip.length - globalAudioSource.time) <= 0.1f && fadeVolume > 0;
    public System.Action onEndReached;
    private float fadeVolume = 0.0f;
    private bool DoUpdateFade => (targetPlayState && fadeVolume < 1) || (!targetPlayState && fadeVolume > 0);
    private bool FullyFadedOut => !targetPlayState && fadeVolume <= 0;
    private bool AudioIsPaused => globalAudioSource.time > 0 && !globalAudioSource.isPlaying;
    private System.Action onPaused;

    // Update is called once per frame
    void Update()
    {
        globalAudioSource.volume = baseVolume * fadeVolume;
        if (AudioIsPaused && targetPlayState)
            globalAudioSource.Play();
        if (DoUpdateFade)
        {
            fadeVolume = Mathf.Clamp01(fadeVolume + Time.deltaTime * (targetPlayState ? 1 : -1) / fadeTime);
            if (FullyFadedOut)
                OnMusicFadedOut();
        }
        if (globalAudioSource.clip && EndReached) 
        {
            OnEndReached();
        }
    }
    private void OnMusicFadedOut()
    {
        globalAudioSource.Pause();
        onPaused?.Invoke();
    }

    private void OnEndReached()
    {
        globalAudioSource.Stop();
        fadeVolume = 0;
        onEndReached?.Invoke();
    }

    public void PlayNext(AudioClip clip) 
    {
        if (!globalAudioSource.isPlaying)
            Play(clip);
        else 
        {
            onPaused += () => Play(clip);
            targetPlayState = false;
        }
    }
    private void Play(AudioClip clip)
    {
        globalAudioSource.clip = clip;
        globalAudioSource.Play();
        targetPlayState = true;
        onPaused = null;
    }
}
