using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flier.Controls.AI
{
    [RequireComponent(typeof(BasicFlier))]
    public class CruiseControl : MonoBehaviour
    {
        public bool showGizmos = false;
        [Space, Space]
        [Space, Header("Cruise Settings")]
        public Vector3 destinationPoint = Vector3.zero; // the finnal point to be reached.
        public Vector3 wayPoint = Vector3.zero;// Temporary move point, used to avoid obstacles, until gets to the destinationPoint.
        public float maxThrustDistance = 12.5f;// Disance at which the engine works at it's maximum thrust.
        public float reachPointDistance = 0.5f;// At which distance, the waypoint counts as reached.
        public float minimumThrust = 0.2f;// What's the minimum thrust, when a wayPoint is very close.
        public bool move = false;// used to start, and stop the process of reaching the final destination.

        [Space, Header("Obstacle Avoidance")]
        [Range(4, 24)] public int raysCount = 6;
        public float spreadAngle = 60.0f;
        public float castDistance = 10.0f;
        public float castRadius = 1.4f;
        public LayerMask obstacleLayerMask;
        public float checkInterval = 1.0f;
        public float castDirOffset = 0.0f;

        private int rayIndex = 0;
        private RaycastHit hit;
        private Ray sphereCastRay;
        private Vector3 bestResult;

        private BasicFlier flier
        {
            get 
            {
                if (!m_flier)
                    m_flier = GetComponent<BasicFlier>();
                return m_flier;
            }
        }
        private BasicFlier m_flier;

        private Vector2 InputAxis => new Vector2(wayPoint.x - transform.position.x, wayPoint.z - transform.position.z);
        private Vector3 LastCastPosition => transform.position + sphereCastRay.direction.normalized * castDistance;
        private bool DestinationIsObscured() => Physics.CheckSphere(destinationPoint, castRadius, obstacleLayerMask);
        private float TargetThrust()
        {
            if (flier.lockAtTarget)
                return Mathf.Clamp(InputAxis.magnitude / maxThrustDistance, minimumThrust, 1.0f);
            else if (Mathf.Abs(flier.AngleToAxisInput) < 5.0f)
                return Mathf.Clamp(InputAxis.magnitude / maxThrustDistance, minimumThrust, 1.0f);
            else return minimumThrust;
        }
        private float intervalHit = 0;

        // Update is called once per frame
        protected virtual void Update()
        {
            if (InputAxis.magnitude > 0.1f)
            {
                flier.HandleMoveInput(InputAxis);
                flier.thrustPower = move ? TargetThrust() : 0.0f;
            }
            if (Vector3.Distance(wayPoint, destinationPoint) > 0.5f)
                UpdateWayPointRealtime();
            if (move && Vector3.Distance(wayPoint, destinationPoint) < 0.5f && InputAxis.magnitude < reachPointDistance)
                OnWayPointReached();
        }

        private void UpdateWayPointRealtime()
        {
            if (move && intervalHit <= Time.time)
            {
                intervalHit = Time.time + checkInterval;
                UpdateWayPoint();
            }
        }

        private void OnWayPointReached()
        {
            move = false;
        }

        private void UpdateWayPoint()
        {
            move = true;
            wayPoint = destinationPoint - transform.position;
            /*if (wayPoint.magnitude < castDistance && DestinationIsObscured())
                OnWayPointReached();
            else*/ if (CastTest(wayPoint.normalized))
                wayPoint = GetAvoidObstaclePoint(destinationPoint);
            else if (wayPoint.magnitude < castDistance && !DestinationIsObscured())
                wayPoint = transform.position + wayPoint;
            else
                wayPoint = transform.position + wayPoint.normalized * castDistance;
        }

        private Vector3 GetAvoidObstaclePoint(Vector3 finalDestination)
        {
            float distanceCache = float.MaxValue;
            Vector3 resultPoint = Vector3.zero;
            for (rayIndex = 0; rayIndex < raysCount; rayIndex++)
            {
                if (CastTest(AngleToDirection(-spreadAngle * 0.5f + (spreadAngle / raysCount) * rayIndex)))
                    continue;// we want to contine to find a way, without obstacles.
                if (Vector3.Distance(LastCastPosition, finalDestination) < distanceCache)
                {
                    resultPoint = LastCastPosition;
                    distanceCache = Vector3.Distance(LastCastPosition, finalDestination);
                }
            }
            if (resultPoint != Vector3.zero)
                return resultPoint;

            if (!CastTest(-transform.forward))// We are stuck. let's try going back.
                return LastCastPosition;
            else
                return (finalDestination - transform.position).normalized * castDistance;// no way back. let's try to push that obstacle.
        }

        public void MoveToPoint(Vector3 worldPosition)
        {
            destinationPoint = worldPosition;
            UpdateWayPoint();
        }

        private bool CastTest(Vector3 direction)
        {
            sphereCastRay = new Ray(transform.position + castDirOffset * direction.normalized, direction);
            return Physics.SphereCast(sphereCastRay, castRadius, out hit, castDistance, obstacleLayerMask);
        }

        protected virtual void OnDrawGizmos()
        {
            if (!showGizmos)
                return;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(wayPoint, 0.5f);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, wayPoint);
            Gizmos.DrawWireSphere(wayPoint, reachPointDistance);
            DrawSphereCastGizmo();
        }

        private Vector3 AngleToDirection(float angle)
        {
            angle += Vector3.SignedAngle(Vector3.forward, transform.forward, Vector3.up);
            return new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
        }

        private void DrawSphereCastGizmo()
        {
            for (rayIndex = 0; rayIndex < raysCount; rayIndex++)
                if (CastTest(AngleToDirection(-spreadAngle * 0.5f + (spreadAngle / raysCount) * rayIndex)))
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawRay(sphereCastRay.origin, sphereCastRay.direction.normalized * hit.distance);
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(sphereCastRay.origin + sphereCastRay.direction.normalized * hit.distance, castRadius);
                    Gizmos.DrawRay(sphereCastRay.origin + sphereCastRay.direction.normalized * hit.distance, sphereCastRay.direction.normalized * (castDistance - hit.distance));
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawRay(sphereCastRay.origin, sphereCastRay.direction.normalized * castDistance);
                    Gizmos.DrawWireSphere(sphereCastRay.origin + sphereCastRay.direction.normalized * castDistance, castRadius);
                }
        }
    }
}
