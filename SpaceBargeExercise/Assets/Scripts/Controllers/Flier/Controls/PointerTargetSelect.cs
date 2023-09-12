using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flier.Controls.AI;
using UnityEngine.EventSystems;

namespace Flier.Controls
{
    public class PointerTargetSelect : MonoBehaviour
    {
        public bool showGizmos = false;
        [Space, Space, Space]
        [SerializeField] private DefaultFlierControl playerFlier;
        [SerializeField] private LayerMask castLayer;
        [SerializeField] private float castRadius = 1.5f;
        public bool autoLockTarget = false;
        public float autoLockRadius = 10.0f;
        public float auoCheckInterval = 1;
        private Collider[] collidersClicked = new Collider[10];
        private Vector3 lastWorldClickPosition = Vector3.zero;
        private Ray worldPoint;
        private float rayDistance = 10;
        private Plane floor;
        private PointerEventData pointerData;
        private List<RaycastResult> results = new List<RaycastResult>(10);
        private float nextAutocheckTime = 0;

        private void Awake()
        {
            pointerData = new PointerEventData(EventSystem.current);
        }
        private void Start()
        {

            playerFlier.onFlierDestroyed.AddListener(ForgetTarget);
            nextAutocheckTime = Time.time + auoCheckInterval;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                CheckSphereForTargets(InputToWorldPosition(), castRadius);
            if (!playerFlier.lockAtTarget && autoLockTarget)
                UpdateAutocheckTimer();


        }

        private void UpdateAutocheckTimer() 
        {
            if (nextAutocheckTime > 0 && Time.time >= nextAutocheckTime) 
            {
                CheckSphereForTargets(playerFlier.transform.position, autoLockRadius);
                nextAutocheckTime = Time.time + auoCheckInterval;
            }
        }

        private Vector3 InputToWorldPosition() 
        {
            worldPoint = CameraFollow.camInstance.ScreenPointToRay(Input.mousePosition);
            floor.normal = Vector3.up;
            if (floor.Raycast(worldPoint, out rayDistance))
                return worldPoint.origin + worldPoint.direction * rayDistance;
            return Vector3.zero;
        }

        private void CheckSphereForTargets(Vector3 position, float checkRadius) 
        {
            if (getMouseOverUi())
                return;
            collidersClicked = Physics.OverlapSphere(position, checkRadius, castLayer);
            foreach (var collider in collidersClicked)
                if (collider.GetComponentInParent<BasicAiFlier>())
                {
                    LockTarget(collider.GetComponentInParent<BasicAiFlier>());
                    return;
                }
            if (playerFlier.lockAtTarget)
                ForgetTarget();
        }

        private void LockTarget(BasicAiFlier aiFlier) 
        {
            if (playerFlier.lockAtTarget)
                ForgetTarget();
            playerFlier.lockAtTarget = aiFlier.transform;
            aiFlier.onFlierDestroyed.AddListener(ForgetTarget);
        }

        private void ForgetTarget()
        {
            if (!playerFlier.lockAtTarget)
                return;
            playerFlier.lockAtTarget.GetComponent<BasicAiFlier>().onFlierDestroyed.RemoveListener(ForgetTarget);
            playerFlier.lockAtTarget = null;
            nextAutocheckTime = Time.time + auoCheckInterval;
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos)
                return;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(lastWorldClickPosition, castRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerFlier.transform.position, autoLockRadius);
        }
        public bool getMouseOverUi()
        {
            pointerData.position = Input.mousePosition;
            EventSystem.current.RaycastAll(pointerData, results);
            return results.Count > 0;
        }
    }
}
