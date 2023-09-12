using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(ParticleSystem))]
public class ParticlesLoopSfx : MonoBehaviour
{
    public string loopSfxName = "";
    private ParticleSystem loopParticleSystem;
    private LoopSfx loopSfx;
    // Start is called before the first frame update
    void Start()
    {
        loopParticleSystem = GetComponent<ParticleSystem>();
        if (!string.IsNullOrEmpty(loopSfxName))
            loopSfx = AudioManager.CreateLoopSoundInstance(loopSfxName, transform);
    }

    // Update is called once per frame
    void Update()
    {
        loopSfx.targetVolume = loopParticleSystem.isPlaying ? 1.0f : 0.0f;
    }
}
