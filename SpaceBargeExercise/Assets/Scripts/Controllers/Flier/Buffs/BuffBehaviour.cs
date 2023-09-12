using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flier.BuffSystem
{
    public class BuffBehaviour : MonoBehaviour
    {
        [SerializeField] private Buff data;
        [SerializeField] private string pickupSound = "deBuff";
        private BasicFlier flireInstance;
        private BuffEffect<BasicFlier> effect;

        private void OnTriggerEnter(Collider other)
        {
            flireInstance = other.GetComponentInParent<BasicFlier>();
            if (flireInstance && flireInstance.isAlive)
            {
                data.buffData.GetEffect(out effect);
                Debug.Log($"Buff \"{effect.GetType()}\" picked up by: {flireInstance.gameObject.name}");
                data.AddBuff(flireInstance);
                AudioManager.PlaySfx(pickupSound, transform.position);
                gameObject.SetActive(false);
            }
        }


    }
}
