using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flier.Controls;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private DefaultFlierControl playerFlier;
    // Start is called before the first frame update
    public void Init()
    {
        UiManager.instance.ortho.SetControls(playerFlier);
        PlayerStats.instance.playerScore = 0;
        CameraFollow.instance.SetTarget(playerFlier.transform);
        gameObject.SetActive(true);
        // Force Respawn Player.
        // Force Respawn Enemies.
        // Force Respawn Allies.
    }

    public void Reset()
    {
        Init();
    }
}
