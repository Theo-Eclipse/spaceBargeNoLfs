using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Flier.BuffSystem
{
    [System.Serializable]
    public class HealthEffect : BuffEffect<BasicFlier>
    {
        [Space, SerializeField] private float value;
        [SerializeField] private ValueEffectType effectType;
        public override void EnableEffect(BasicFlier flier)
        {
                switch (effectType)
                {
                    case ValueEffectType.Addition:
                        flier.sFinnal.health += value;
                        return;
                    case ValueEffectType.Multiplication:
                        flier.sFinnal.health *= value;
                        return;
                    case ValueEffectType.Constant:
                        flier.sFinnal.health = value;
                        return;
                }
        }
        public override void DisableEffect(BasicFlier flier)
        {
            flier.sFinnal.health = flier.sDefault.health;
        }
    }
}
