using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public bool vertical = false;
    public bool horizontal = true;
    public float layerDistance = 1;
    public float layerSpeed = 10;
    public Vector2 targetOffset = Vector2.zero;
    private Material matControl;

    private Vector2 initialOffset = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        matControl = GetComponent<Renderer>().sharedMaterial;
        initialOffset = matControl.mainTextureOffset;
        targetOffset = initialOffset;
    }

    // Update is called once per frame
    void Update()
    {
        initialOffset = Vector2.Lerp(initialOffset, targetOffset, layerSpeed * Time.deltaTime);
        matControl.mainTextureOffset = new Vector2(
            horizontal ? initialOffset.x : matControl.mainTextureOffset.x,
            vertical ? initialOffset.y : matControl.mainTextureOffset.y);
    }

    public void MoveLayer(Vector2 delta) => targetOffset += delta / layerDistance;

    public void SetLayer(Vector2 target) => targetOffset = target / layerDistance;

}
