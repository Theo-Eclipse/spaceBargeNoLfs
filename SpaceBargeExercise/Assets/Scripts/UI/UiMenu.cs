using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class UiMenu : MonoBehaviour
    {
        [SerializeField] protected RectTransform parentContainer;
        protected virtual void Reset() 
        {
            parentContainer = GetComponent<RectTransform>();
        }
        public virtual void Show() => parentContainer.gameObject.SetActive(true);
        public virtual void Hide() => parentContainer.gameObject.SetActive(false);
        public virtual void SwitchToMenu(UiMenu otherMenu)
        {
            otherMenu.Show();
            Hide();
        }
    }
}
