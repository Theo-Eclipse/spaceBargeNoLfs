using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flier.Weapons;
using Flier.Weapons.Projectiles;

namespace Flier.Modules {
    public class WeaponModule : MonoBehaviour
    {
        public bool toggleFire = false;
        [SerializeField] private ModuleInfo info;
        [Header("Weapon Stats")]
        public WeaponStats sDefault = new WeaponStats
        {
            damagePerHit = 20,
            speed = 10,
            shootInterval = 0.5f,
            spread = 10,
            projectileCount = 1,
            profectileLifeTime = 3
        };
        public WeaponStats sFinnal = new WeaponStats
        {
            damagePerHit = 20,
            speed = 10,
            shootInterval = 0.5f,
            spread = 10,
            projectileCount = 1,
            profectileLifeTime = 3
        };
        public float reloadProgress => Mathf.InverseLerp(shootReadyTime - sFinnal.shootInterval, shootReadyTime, Time.time);
        public BasicProjectile projectilePrefab;
        [SerializeField] private Transform projectileSpawnPoint;

        [Header("Sounds")]
        [SerializeField] private string shootSoundName;


        private IDamagable hitTarget;
        private BasicFlier moduleHolder;
        private int projIndex = 0;
        private float shootReadyTime = 0;
        private float startAngle => -(sFinnal.spread * (sFinnal.projectileCount-1)) * 0.5f;
        private void Start()
        {
            moduleHolder = GetComponentInParent<BasicFlier>();
        }
        // Update is called once per frame
        public void Update()
        {
            if (!toggleFire || !moduleHolder.isAlive)
                return;
            Shoot();
        }

        public void Shoot() 
        {
            if (shootReadyTime > 0 && Time.time < shootReadyTime)
                return;
            shootReadyTime = Time.time + sFinnal.shootInterval;
            if (sFinnal.projectileCount == 1)
                LaunchProjectile(transform.forward);
            else
                for (projIndex = 0; projIndex < sFinnal.projectileCount; projIndex++)
                    LaunchProjectile(AngleToDirection(startAngle + sFinnal.spread * projIndex));
            if(!string.IsNullOrEmpty(shootSoundName))
                AudioManager.PlaySfx(shootSoundName, projectileSpawnPoint.position);
        }

        private void LaunchProjectile(Vector3 direction) 
        {
            BasicProjectile new_projectile = ProjectilePool.GetInstance(projectilePrefab);
            new_projectile.transform.position = projectileSpawnPoint.position;
            new_projectile.transform.forward = direction;
            new_projectile.speed = sFinnal.speed;
            new_projectile.lifeTime = sFinnal.profectileLifeTime;
            new_projectile.onColliderHit.AddListener(OnShotHit);
        }

        private void OnShotHit(Collider collider) 
        {
            hitTarget = collider.GetComponentInParent<IDamagable>();
            if (hitTarget != null)
                hitTarget.DamageOrHeal(-sFinnal.damagePerHit);
        }
        private Vector3 AngleToDirection(float angle)
        {
            angle += Vector3.SignedAngle(Vector3.forward, transform.forward, Vector3.up);
            return new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
        }
    }
}
