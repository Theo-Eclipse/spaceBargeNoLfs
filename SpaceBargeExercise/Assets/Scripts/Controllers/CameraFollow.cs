using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance { get; private set; }
    [SerializeField] private Transform followTarget;
    public float speed = 100.0f;
    public static Camera camInstance 
    {
        get 
        {
            if (!instance.m_cam)
                instance.m_cam = instance.GetComponentInChildren<Camera>();
            return instance.m_cam;
        }
    }
    private Camera m_cam;

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (followTarget)
            transform.position = Vector3.Lerp(transform.position, followTarget.position, speed * Time.deltaTime);
    }

    public static void SetPosition() 
    {
        instance.transform.position = instance.followTarget.position;
    }

    public void SetTarget(Transform followTarget) 
    {
        this.followTarget = followTarget;
    }
}
