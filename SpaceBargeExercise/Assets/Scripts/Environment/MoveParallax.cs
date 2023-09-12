using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveParallax : MonoBehaviour
{
    public Vector2 moveDelta = new Vector2(1, 0);
    public bool enableRandomDirection = false;
    public float randomDirectionChangeInterval = 3.0f;
    public float directionLerpSpeed = 0.1f;
    [SerializeField] private ParallaxCamera parallaxCamera;
    private Vector2 parallaxOffset = Vector2.zero;


    private float nextChangeTime = 0;
    private Vector2 randomMoveDelta => lerpDirection.normalized * moveDelta.magnitude;
    private Vector2 direction = Vector2.right;
    private Vector2 lerpDirection = Vector2.right;
    private void Start()
    {
        nextChangeTime = Time.time + randomDirectionChangeInterval;
    }

    // Update is called once per frame
    void Update()
    {
        lerpDirection = Vector2.Lerp(lerpDirection, direction, directionLerpSpeed * Time.deltaTime);
        parallaxOffset += enableRandomDirection ? randomMoveDelta : moveDelta;
        parallaxCamera.MoveLayers(parallaxOffset);
        if (enableRandomDirection)
            UpdateInterval();
    }

    private void UpdateInterval() 
    {
        if (Time.time >= nextChangeTime)
        {
            SetRandomDirection();
            nextChangeTime = Time.time + randomDirectionChangeInterval;
        }
    }
    private void SetRandomDirection() 
    {
        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

    }
}
