using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Flier.Weapons.Projectiles
{
    public class BasicProjectile : MonoBehaviour
    {
        [Header("Projectile Settings")]
        public float speed = 10;
        public float lifeTime = 10;
        public LayerMask hitMask;
        public string hitSound = "";

        [Header("Events")]
        public ProjectileHitEvent onColliderHit = new ProjectileHitEvent();
        public UnityEvent onDestroy = new UnityEvent();// prefab, instance.
        private float destroyTime = 0;

        private Vector3 positionDelta => transform.forward * speed * Time.deltaTime;
        private Ray projectileUpdate;
        private RaycastHit hit;
        protected virtual void PhysicsCast() 
        {
            if (Physics.Raycast(projectileUpdate, out hit, positionDelta.magnitude, hitMask))
            {
                OnHit();
                return;
            }
        }
        private void OnHit()
        {
            if (!string.IsNullOrEmpty(hitSound))
                AudioManager.PlaySfx(hitSound, hit.point);
            onColliderHit?.Invoke(hit.collider);
            onDestroy?.Invoke();
        }
        protected virtual void OnEnable() 
        {
            destroyTime = Time.time + lifeTime;
        }
        protected virtual void Update()
        {
            if (destroyTime > 0 && Time.time > destroyTime)
            {
                onDestroy?.Invoke();
                return;
            }
            projectileUpdate = new Ray(transform.position, positionDelta);
            transform.position += positionDelta;
            transform.Rotate(0, 0, 400 * Time.deltaTime);
            PhysicsCast();
        }
    }

    public class ProjectileHitEvent : UnityEvent<Collider> { }
}
