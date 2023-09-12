using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flier.BuffSystem;
using Flier;

[System.Serializable, CreateAssetMenu(fileName = "Fire Rate Buff", menuName = "Scriptable Objects/Flier/Buffs/Fire Rate", order = 1)]
public class FireRateBuff : BuffScriptableObject
{
    public FireRateEffect buffEffect;
    public override void GetEffect(out BuffEffect<BasicFlier> effect) => effect = buffEffect;
}