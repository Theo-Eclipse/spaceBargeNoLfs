using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flier.Modules;

namespace Flier.BuffSystem
{
    [System.Serializable]
    public class ExtraProjectiles : BuffEffect<BasicFlier>
    {
        [Space, SerializeField] private int value;
        [SerializeField] private ValueEffectType effectType;
        private List<WeaponModule> weapons = new();
        public override void EnableEffect(BasicFlier flier)
        {
            if(weapons.Count == 0) flier.GetComponentsInChildren(false, weapons);
            foreach(WeaponModule weapon in weapons)
                switch (effectType)
                {
                    case ValueEffectType.Addition:
                        Debug.Log($"Adding projectiles to flier {flier.gameObject.name}");
                        Debug.Log($"Adding projectiles count before effect {weapon.sFinnal.projectileCount}");
                        weapon.sFinnal.projectileCount += value;
                        Debug.Log($"Adding projectiles count After effect {weapon.sFinnal.projectileCount}");
                        return;
                    case ValueEffectType.Multiplication:
                        weapon.sFinnal.projectileCount *= value;
                        return;
                    case ValueEffectType.Constant:
                        weapon.sFinnal.projectileCount = value;
                        return;
                }
        }
        public override void DisableEffect(BasicFlier flier)
        {
            Debug.Log($"Canceling projectiles effect.");
            if (weapons.Count == 0) flier.GetComponentsInChildren(false, weapons);
            foreach (WeaponModule weapon in weapons)
                weapon.sFinnal.projectileCount = weapon.sDefault.projectileCount;
        }
    }
}
