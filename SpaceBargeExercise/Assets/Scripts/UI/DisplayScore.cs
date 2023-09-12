using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    [SerializeField] private TMP_Text score;
    // Update is called once per frame
    void Update()
    {
        score.text = $"<b>Score:</b>\n<size=24>{PlayerStats.instance.playerScore}</size>";
    }
}
