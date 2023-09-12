using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Flier;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private BasicFlier flier;
    [SerializeField] private Image circleBar;
    private GameObject barParent;
    private float currentHealth => Mathf.Clamp01(flier.sFinnal.health / flier.sDefault.health);
    private void Start()
    {
        barParent = circleBar.transform.parent.gameObject;
        flier.onFlierDestroyed.AddListener(Hide);
        flier.onFlierRespawned.AddListener(Show);
    }
    private void Update()
    {
        barParent.SetActive(currentHealth < 1);
        circleBar.fillAmount = currentHealth;
    }
    private void LateUpdate()
    {
        transform.forward = Vector3.forward;
    }

    private void Hide() => gameObject.SetActive(false);
    private void Show() => gameObject.SetActive(true);
}
