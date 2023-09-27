using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Flier;

namespace UI.Fliers
{
    public class FuelBar : MonoBehaviour
    {
        public static FuelBar Instance { get; private set; }
        public float defaultFuelAmount = 100;
        public float drainSpeed = 2.0f;
        public BasicFlier targetFlier;
        [SerializeField] private float lowFuelSpeed = 2;
        [SerializeField] private Image fillBar;
        [SerializeField] private TMP_Text amount;
        private Color _fullColor = Color.HSVToRGB(123, 100, 58);
        private Color _midColor = Color.HSVToRGB(54, 100, 58);
        private Color _emptyColor = Color.HSVToRGB(7, 100, 58);

        private float _currentFuelAmount = 100;
        // Start is called before the first frame update
        void Awake()
        {
            if(!Instance) Instance = this;
            _currentFuelAmount = defaultFuelAmount;
        }

        private void Start()
        {
            _fullColor = Color.HSVToRGB(123/360.0f, 1, 58/100.0f);
            _midColor = Color.HSVToRGB(54/360.0f, 1, 58/100.0f);
            _emptyColor = Color.HSVToRGB(7/360.0f, 1, 58/100.0f);
            UpdateFuelText();
            SetFill(_currentFuelAmount/defaultFuelAmount);//
        }

        // Update is called once per frame
        void Update()
        {
            if(!targetFlier)
                return;
            if (targetFlier.thrustPower > 0)
            {
                DrainFuel();
            }
        }

        private void DrainFuel()
        {
            if(_currentFuelAmount <= 0 || !targetFlier.isAlive)
                return;
            _currentFuelAmount =
                Mathf.Clamp(_currentFuelAmount - Time.deltaTime * drainSpeed, 0, defaultFuelAmount);
            SetFill(_currentFuelAmount/defaultFuelAmount);
            if(_currentFuelAmount <= 0)
                OnFuelEmpty();
        }

        private void UpdateFuelText()
        {
            amount.text = $"({PlayerStats.instance.fuelCount}x)";
        }

        private void OnFuelEmpty()
        {
            if (PlayerStats.instance.fuelCount > 0)
            {
                PlayerStats.instance.fuelCount--;
                UpdateFuelText();
                Refill();
            }
            else
                targetFlier.sFinnal.maxVelocity *= 0.1f;
        }


        private void Refill()
        {
            _currentFuelAmount = defaultFuelAmount;
            SetFill(_currentFuelAmount/defaultFuelAmount);
            PlayTextAnim();
            targetFlier.sFinnal.maxVelocity = targetFlier.sDefault.maxVelocity;
        }

        private void SetFill(float newValue)
        {
            fillBar.fillAmount = newValue;
            SetBarColor(newValue);
        }

        private void SetBarColor(float fillValue)
        {
            // Sometimes Riders loses reference on some components, like color of an UI Image.
            fillBar.color = fillValue switch
            {
                < 0.3f => new Color(_emptyColor.r, _emptyColor.g, _emptyColor.b, fillBar.color.a),
                < 0.45f => new Color(_midColor.r, _midColor.g, _midColor.b, fillBar.color.a),
                _ => new Color(_fullColor.r, _fullColor.g, _fullColor.b, fillBar.color.a)
            };
        }

        private void PlayTextAnim()
        {
            StartCoroutine(animateTextAmount());
        }

        private IEnumerator animateTextAmount()
        {
            for (float i = 0.0f; i <= 1.0f; i +=  Time.deltaTime * 5.0f)
            {
                amount.transform.localScale = Vector3.one * Mathf.Lerp(1.8f, 1, Mathf.Clamp01(i));
                yield return null;
            }
        }
    }
}

