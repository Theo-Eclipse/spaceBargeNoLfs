using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flier.Controls;

namespace UnityEngine.UI
{
    [System.Serializable]
    public class GameOrtho : UiMenu
    {
        [Header("Ortho Buttons")]
        [Space] public Button inGameMenu;
        [SerializeField] private ToggleFire fireControl;
        [SerializeField] private UiJoystick moveJoystick;
        [SerializeField] private UiJoystick turnAimJoystick;
        public void SetControls(DefaultFlierControl controlTarget)
        {
            controlTarget.moveJoystick = moveJoystick;
            controlTarget.turnJoystick = turnAimJoystick;
            controlTarget.fireToggle = fireControl;
            fireControl.SelectTargetFlier(controlTarget);
        }
    }
}
