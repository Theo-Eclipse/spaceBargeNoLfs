using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flier;

public class FlierPartsExplode : MonoBehaviour
{
    public float partsRotationSpeed = 300.0f;
    public float partsLinearSpeed = 5;
    // Start is called before the first frame update
    [SerializeField] private BasicFlier flierEvents;
    [SerializeField] private Transform flierGraphics;
    [SerializeField] private Transform flierDebrisGraphics;
    private List<Collider> shipParts = new List<Collider>();
    private Rigidbody partRigidbody;
    private MeshRenderer partRenderer;
    private Dictionary<Collider, FlierDebris> parts = new Dictionary<Collider, FlierDebris>();
    void Start()// runs before Start() function.
    {
        if (!flierDebrisGraphics) 
            CreateMissing();
        SubscribeToEvents();
        OnFlierRespawned();
    }
    private void CreateMissing() 
    {
        flierDebrisGraphics = Instantiate(flierGraphics, flierGraphics.parent);
        flierDebrisGraphics.name = $"DebrisGraphics";
    }
    private void SubscribeToEvents() 
    {
        flierEvents.onFlierDestroyed.AddListener(OnFlierDestroyed);
        flierEvents.onFlierRespawned.AddListener(OnFlierRespawned);
    }
    private void OnFlierDestroyed() 
    {
        flierGraphics.gameObject.SetActive(false);
        flierDebrisGraphics.gameObject.SetActive(true);
        ResetAndExplodeDebris();
    }
    private void OnFlierRespawned()
    {
        flierGraphics.gameObject.SetActive(true);
        flierDebrisGraphics.gameObject.SetActive(false);
    }
    private void ResetAndExplodeDebris() 
    {
        flierDebrisGraphics.GetComponentsInChildren(true, shipParts);
        foreach (var collider in shipParts)
        {
            partRenderer = collider.GetComponent<MeshRenderer>();
            partRigidbody = collider.GetComponent<Rigidbody>();
            if (partRenderer && !partRigidbody)
            {
                partRigidbody = CreateRigidBody(collider);
            }
            else if (parts.ContainsKey(collider)) 
            {
                ResetPart(collider);
                partRigidbody = parts[collider].rigidbody;
            }
            ApplyForceAndTorque(partRigidbody);
        }
    }
    private Rigidbody CreateRigidBody(Collider collider) 
    {
        partRigidbody = collider.gameObject.AddComponent<Rigidbody>();
        partRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        partRigidbody.useGravity = false;
        parts.Add(collider, new FlierDebris
        {
            rigidbody = partRigidbody,
            initialPosition = collider.transform.localPosition,
            initialRotation = collider.transform.localRotation
        });
        return partRigidbody;
    }
    private void ApplyForceAndTorque(Rigidbody part) 
    {
        if (!part)
            return;
        part.AddTorque(new Vector3((Random.value - 0.5f) * partsRotationSpeed, (Random.value - 0.5f) * partsRotationSpeed, (Random.value - 0.5f) * partsRotationSpeed) * 2, ForceMode.Impulse);
        part.AddForce((flierDebrisGraphics.position - part.position).normalized * Random.value * partsLinearSpeed, ForceMode.Impulse);
    }

    private void ResetPart(Collider collider)
    {
        parts[collider].rigidbody.velocity = Vector3.zero;
        parts[collider].rigidbody.angularVelocity = Vector3.zero;
        collider.transform.localPosition = parts[collider].initialPosition;
        collider.transform.localRotation = parts[collider].initialRotation;
    }

    private struct FlierDebris 
    {
        public Rigidbody rigidbody;
        public Vector3 initialPosition;
        public Quaternion initialRotation;
    }
}
