using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCamera : MonoBehaviour
{
    [SerializeField] private Transform moveTarget;
    private List<ParallaxLayer> layers = new();
    public float layersMoveSpeed = 2.0f;
    public float sensetivity = 0.1f;
    public float distanceStep = 1;
    private float distance = 1;
    public bool overrideSettings = false;
    private Vector2 VectorXZ(Vector3 vector) => new Vector2(vector.x, vector.z);
    // Start is called before the first frame update
    void Start()
    {
        GetComponentsInChildren(false, layers);
        distance = 1;
        if (overrideSettings)
            foreach (var layer in layers)
            {
                layer.layerSpeed = layersMoveSpeed;
                layer.layerDistance = distance;
                distance += distanceStep;
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (overrideSettings)
            distance = 1;
        foreach (var layer in layers)
        {
            if(!moveTarget)
                layer.MoveLayer(new Vector2(Input.GetAxis("Mouse X"), 0) * sensetivity);
            else layer.SetLayer(VectorXZ(moveTarget.transform.position) * sensetivity);
            if (overrideSettings) 
            {
                layer.layerSpeed = layersMoveSpeed;
                layer.layerDistance = distance;
                distance += distanceStep;
            }
        }
    }

    public void MoveLayers(Vector2 moveXY) 
    {
        foreach (var layer in layers)
            layer.MoveLayer(moveXY * sensetivity);
    }
}
