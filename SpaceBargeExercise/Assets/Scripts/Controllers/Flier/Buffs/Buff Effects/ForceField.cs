using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flier.BuffSystem
{
    [System.Serializable]
    public class ForceFieldEffect : BuffEffect<BasicFlier>
    {
        [Space, SerializeField] GameObject forceFieldPrefab;
        private GameObject instance;
        public override void EnableEffect(BasicFlier flier)
        {
            flier.isImmortal = true;
            instance = Object.Instantiate(forceFieldPrefab, flier.transform);
            instance.transform.localPosition = Vector3.zero;
        }
        public override void DisableEffect(BasicFlier flier)
        {
            flier.isImmortal = false;
            Object.Destroy(instance);
        }
    }
}
