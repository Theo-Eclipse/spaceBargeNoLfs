using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flier.Controls.AI
{
    public class AiMoveTest : MonoBehaviour
    {
        [SerializeField] private Camera eventCamera;
        [SerializeField] private BasicAiFlier ai;
        public Vector3 cursorWorldPos = Vector3.zero;
        public Ray worldPoint;
        public float rayDistance = 10;
        private Plane floor;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                worldPoint = eventCamera.ScreenPointToRay(Input.mousePosition);
                floor.normal = Vector3.up;
                if (floor.Raycast(worldPoint, out rayDistance))
                    ai.MoveToPoint(worldPoint.origin + worldPoint.direction * rayDistance);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (eventCamera)
            {
                worldPoint = eventCamera.ScreenPointToRay(Input.mousePosition);
                floor.normal = Vector3.up;
                if (floor.Raycast(worldPoint, out rayDistance))
                {
                    Gizmos.DrawRay(worldPoint.origin, worldPoint.direction * rayDistance);
                }
                Gizmos.DrawWireSphere(worldPoint.origin + worldPoint.direction * rayDistance, 0.25f);
            }
        }
    }
}
