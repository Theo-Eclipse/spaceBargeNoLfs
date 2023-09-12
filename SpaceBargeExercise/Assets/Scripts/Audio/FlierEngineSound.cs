using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flier;
using UnityEngine.Audio;

public class FlierEngineSound : MonoBehaviour
{
    public string loopSfxName = "";
    private BasicFlier flier;
    private LoopSfx loopSfx;
    // Start is called before the first frame update
    void Start()
    {
        flier = GetComponentInParent<BasicFlier>();
        loopSfx = AudioManager.CreateLoopSoundInstance(loopSfxName, transform);
    }

    // Update is called once per frame
    void Update()
    {
        loopSfx.targetVolume = flier.isAlive ? flier.thrustPower : 0;
    }
}
