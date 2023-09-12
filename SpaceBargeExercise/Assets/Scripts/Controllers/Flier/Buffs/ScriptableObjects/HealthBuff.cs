using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flier.BuffSystem;
using Flier;

[System.Serializable, CreateAssetMenu(fileName = "Health Buff", menuName = "Scriptable Objects/Flier/Buffs/Health Buff", order = 1)]
public class HealthBuff : BuffScriptableObject
{
    public HealthEffect buffEffect;
    public override void GetEffect(out BuffEffect<BasicFlier> effect) => effect = buffEffect;
}
