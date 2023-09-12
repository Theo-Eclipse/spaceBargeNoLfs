using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flier;
using Flier.Modules;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleFire : MonoBehaviour
    {
        [SerializeField] private BasicFlier targetFlier;
        private List<WeaponModule> moduleCache = new List<WeaponModule>();
        public Toggle fireAllToggle;

        // Start is called before the first frame update
        void Start()
        {
            SelectTargetFlier(targetFlier);
        }
        public void SelectTargetFlier(BasicFlier targetFlier)
        {
            this.targetFlier = targetFlier;
            ResetToggle();
            ResetFireModules();
            fireAllToggle.onValueChanged.AddListener(UpdateFireState);
        }
        // Update is called once per frame
        private void UpdateFireState(bool fire)
        {
            foreach (var module in moduleCache)
                module.toggleFire = fire;
        }
        private void ResetToggle() 
        {
            if (!fireAllToggle)
                fireAllToggle = GetComponent<Toggle>();
            fireAllToggle.isOn = false;
            fireAllToggle.onValueChanged.RemoveAllListeners();
        }

        private void ResetFireModules() 
        {
            if (targetFlier == null || moduleCache == null)
                return;
            targetFlier.GetComponentsInChildren(false, moduleCache);
            foreach (var module in moduleCache)
                module.toggleFire = false;
        }
    }
}
