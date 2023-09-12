using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace Flier.Controls
{
    public class DefaultFlierControl : BasicFlier
    {
        public UiJoystick moveJoystick;
        public UiJoystick turnJoystick;
        public ToggleFire fireToggle;
        [Tooltip("Destroy the flier on collision with other flier.")]public bool destroyOnCollision = false;
        private Vector2 LegacyInput => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        private Vector2 MoveInput, LookInput;
        private BasicFlier otherFlier;
        private NewFlierController defultControls;

        private void Awake()
        {
            defultControls = new NewFlierController();
            defultControls.FlierControls.Shoot.performed += ctx => fireToggle.fireAllToggle.isOn = true;
            defultControls.FlierControls.Shoot.canceled += ctx => fireToggle.fireAllToggle.isOn = false;

            defultControls.FlierControls.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
            defultControls.FlierControls.Move.canceled += ctx => MoveInput = Vector2.zero;

            defultControls.FlierControls.MoveUp.performed += ctx => MoveInput = new Vector2(MoveInput.x, 1);
            defultControls.FlierControls.MoveUp.canceled += ctx => MoveInput = new Vector2(MoveInput.x, 0);

            defultControls.FlierControls.MoveDown.performed += ctx => MoveInput = new Vector2(MoveInput.x, -1);
            defultControls.FlierControls.MoveDown.canceled += ctx => MoveInput = new Vector2(MoveInput.x, 0);

            defultControls.FlierControls.MoveLeft.performed += ctx => MoveInput = new Vector2(-1, MoveInput.y);
            defultControls.FlierControls.MoveLeft.canceled += ctx => MoveInput = new Vector2(0, MoveInput.y);

            defultControls.FlierControls.MoveRight.performed += ctx => MoveInput = new Vector2(1, MoveInput.y);
            defultControls.FlierControls.MoveRight.canceled += ctx => MoveInput = new Vector2(0, MoveInput.y);

            defultControls.FlierControls.Look.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
            defultControls.FlierControls.Look.canceled += ctx => LookInput = Vector2.zero;
        }

        private void OnEnable()
        {
            defultControls.FlierControls.Enable();
        }

        private void OnDisable()
        {
            defultControls.FlierControls.Disable();
        }

        // Update is called once per frame
        protected override void Update()
        {
            if (LegacyInput.magnitude > 0.1f)
                HandleLegacyControls();
            else if (turnJoystick.dragging || moveJoystick.dragging)
                HandleUiJoistickControls();
            else if (MoveInput.magnitude > 0.1f || LookInput.magnitude > 0.1f)
                HandleNewInputs();
            base.Update();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!destroyOnCollision)
                return;
            otherFlier = collision.collider.GetComponentInParent<BasicFlier>();
            if (otherFlier && destroyOnCollision)
                Destroy();
        }

        void HandleLegacyControls()
        {
            thrustPower = Mathf.Clamp01(LegacyInput.magnitude);
            HandleMoveInput(LegacyInput);

            if (Input.GetKeyDown(KeyCode.Space))
                fireToggle.fireAllToggle.isOn = true;
            else if (Input.GetKeyUp(KeyCode.Space))
                fireToggle.fireAllToggle.isOn = false;
        }

        void HandleUiJoistickControls() 
        {
            if (turnJoystick.dragging)
                HandleLookInput(turnJoystick.GetInput());
            if (moveJoystick.dragging)
            {
                thrustPower = moveJoystick.GetThrustPower();
                HandleMoveInput(moveJoystick.GetInput());
            }
        }

        void HandleNewInputs() 
        {
            HandleLookInput(LookInput);
            thrustPower = MoveInput.magnitude;
            HandleMoveInput(MoveInput);
        }
    }
}
