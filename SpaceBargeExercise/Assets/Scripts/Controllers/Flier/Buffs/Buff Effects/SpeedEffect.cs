using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flier.BuffSystem
{
    [System.Serializable]
    public class SpeedEffect : BuffEffect<BasicFlier>
    {
        [Space, SerializeField] private float value;
        [SerializeField] private ValueEffectType effectType;
        public override void EnableEffect(BasicFlier flier)
        {
            switch (effectType) 
            {
                case ValueEffectType.Addition:
                    flier.sFinnal.maxVelocity = flier.sDefault.maxVelocity + value;
                    return;
                case ValueEffectType.Multiplication:
                    flier.sFinnal.maxVelocity = flier.sDefault.maxVelocity * value;
                    return;
                case ValueEffectType.Constant:
                    flier.sFinnal.maxVelocity = value;
                    return;
            }
        }
        public override void DisableEffect(BasicFlier flier)
        {
            flier.sFinnal.maxVelocity = flier.sDefault.maxVelocity;
        }
    }
}
