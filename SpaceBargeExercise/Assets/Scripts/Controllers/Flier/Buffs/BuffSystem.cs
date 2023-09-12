using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flier.BuffSystem
{
    //
    // 
    //
    [Serializable]
    public class BuffInfo 
    {
        [Header("Buff Info")]
        public string buffName = "BlankBuff";
        [TextArea] public string description = "Blank Buff - Gives you whole a lot of nothing.";
        public Sprite buffIcon;
    }
    [Serializable]
    public abstract class BuffEffect<T>
    {
        [Header("Effect")]
        public float duration;
        public StackEffect stackEffect = StackEffect.DiscardAdded;
        public abstract void EnableEffect(T flier);
        public abstract void DisableEffect(T flier);
    }

    [Serializable]
    public class Buff 
    {
        public BuffScriptableObject buffData;
        private BasicFlier flierInstance;
        [HideInInspector]
        public float activeTime = 0;
        private BuffEffect<BasicFlier> effect;
        public void AddBuff(BasicFlier flierInstance)
        {
            this.flierInstance = flierInstance;
            buffData.GetEffect(out effect);
            Type buffType = effect.GetType();
            if (effect.duration == 0) 
                effect.EnableEffect(flierInstance);// 0 duration - means permanent - activate the buff without adding it to effects list.
            else if (flierInstance.effects.ContainsKey(buffType))
                StackOverExistingEffect(flierInstance.effects[buffType]);
            else
            {
                flierInstance.effects.Add(buffType, this);
                effect.EnableEffect(flierInstance);
                activeTime = effect.duration;
            }

        }
        public void UpdateTimer(float deltaTime) 
        {
            if (activeTime == 0 || effect.duration == 0)
                return;
            activeTime = activeTime - deltaTime >= 0 ? activeTime - deltaTime : 0;
            if (activeTime == 0)
                RemoveBuff();
        }
        public void RemoveBuff() 
        {
            Type buffType = effect.GetType();
            effect.DisableEffect(flierInstance);
            if(!flierInstance.removeEffects.Contains(buffType))
                flierInstance.removeEffects.Add(buffType);
            flierInstance = null;
        }
        private void StackOverExistingEffect(Buff newBuff)
        {
            switch (effect.stackEffect)
            {
                case StackEffect.DiscardAdded:
                    return;
                case StackEffect.OverrideExisting:
                    newBuff.buffData = buffData;
                    newBuff.activeTime = effect.duration;
                    effect.EnableEffect(flierInstance);
                    return;
                case StackEffect.OverrideAndIncreaseDuration:
                    newBuff.buffData = buffData;
                    newBuff.activeTime += effect.duration;
                    effect.EnableEffect(flierInstance);
                    return;
                case StackEffect.IncreaseDuration:
                    newBuff.activeTime += effect.duration;
                    return;
                case StackEffect.ResetDuration:
                    newBuff.activeTime = effect.duration;
                    return;
            }
        }
    }
    public enum ValueEffectType 
    {
        Addition,
        Multiplication,
        Constant
    }
    public enum StackEffect
    {
        DiscardAdded,
        ResetDuration,
        OverrideExisting,
        IncreaseDuration,
        OverrideAndIncreaseDuration
    }
}
