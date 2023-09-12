using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flier.BuffSystem;
using Flier;

[System.Serializable, CreateAssetMenu(fileName = "Speed Buff", menuName = "Scriptable Objects/Flier/Buffs/Speed Buff", order = 1)]
public class SpeedBuffEffect : BuffScriptableObject
{
    public SpeedEffect buffEffect;
    public override void GetEffect(out BuffEffect<BasicFlier> effect) => effect = buffEffect;
}
