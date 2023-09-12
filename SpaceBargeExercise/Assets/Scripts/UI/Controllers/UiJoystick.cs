using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Flier.Controls;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class UiJoystick : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [Header("Joystick Settings")]
        public float deadzoneRadius = 0.05f;
        public float releaseSpeed = 500.0f;
        public bool dragging = false;

        [Header("Joystick Alpha")]
        [SerializeField] private float minimumAlpha = 0.15f;
        [SerializeField] private float maximumAlpha = 0.85f;
        [SerializeField] private float fadeInSpeed = 2.0f;
        [SerializeField] private float fadeOutSpeed = 1.5f;

        private Vector2 debugInput;
        private float debugThrust;

        [Header("Joystick Components")]
        [SerializeField] private RectTransform handle;
        [SerializeField] private CanvasGroup joystickColor;

        // Joyistick Inputs
        public Vector2 GetInput() => joystickMagnitude > deadzone && dragging ? handle.anchoredPosition.normalized : Vector2.zero;
        public float GetThrustPower() => joystickMagnitude > deadzone && dragging ? joystickMagnitude / radius : 0.0f;

        // Private Members
        private RectTransform joystickBase;
        private float radius => Mathf.Min(joystickBase.rect.width, joystickBase.rect.height) * 0.5f;
        private float deadzone => radius * deadzoneRadius;
        private float joystickMagnitude => handle.anchoredPosition.magnitude;
        private float maxTravelRadius => radius - handle.rect.width * 0.5f;


        void Start()
        {
            joystickBase = GetComponent<RectTransform>();
        }
        void Update()
        {
            if (!dragging)
                handle.anchoredPosition = Vector2.MoveTowards(handle.anchoredPosition, Vector2.zero, releaseSpeed * Time.deltaTime);
            UpdateUiTransparency();
            debugInput = GetInput();
            debugThrust = GetThrustPower();
        }
        void UpdateUiTransparency()
        {
            joystickColor.alpha = Mathf.Clamp(joystickColor.alpha + Time.deltaTime * (dragging ? fadeInSpeed : -fadeOutSpeed), minimumAlpha, maximumAlpha);
        }
        public void OnDrag(PointerEventData data)
        {
            dragging = data.dragging;
            if (data.dragging)
                handle.anchoredPosition += data.delta * 0.5f;
            if (joystickMagnitude > maxTravelRadius)
                handle.anchoredPosition = handle.anchoredPosition.normalized * maxTravelRadius;
        }
        public void OnEndDrag(PointerEventData data)
        {
            dragging = false;
        }
    }
}
