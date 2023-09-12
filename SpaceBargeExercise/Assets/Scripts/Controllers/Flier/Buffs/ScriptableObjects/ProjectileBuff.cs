using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flier.BuffSystem;
using Flier;

[System.Serializable, CreateAssetMenu(fileName = "Projectiles Buff", menuName = "Scriptable Objects/Flier/Buffs/More Projectile", order = 1)]
public class ProjectileBuff : BuffScriptableObject
{
    public ExtraProjectiles buffEffect;
    public override void GetEffect(out BuffEffect<BasicFlier> effect) => effect = buffEffect;
}
