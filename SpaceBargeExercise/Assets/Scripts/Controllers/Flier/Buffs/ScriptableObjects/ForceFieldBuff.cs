using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flier.BuffSystem;
using Flier;

[System.Serializable, CreateAssetMenu(fileName = "Force Field Buff", menuName = "Scriptable Objects/Flier/Buffs/Force Field", order = 1)]
public class ForceFieldBuff : BuffScriptableObject
{
    public ForceFieldEffect buffEffect;
    public override void GetEffect(out BuffEffect<BasicFlier> effect) => effect = buffEffect;
}
