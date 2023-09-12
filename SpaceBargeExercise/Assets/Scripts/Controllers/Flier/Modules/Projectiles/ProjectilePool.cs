using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flier.Weapons.Projectiles {
    public class ProjectilePool : MonoBehaviour
    {
        public static ProjectilePool instance 
        {
            get 
            {
                if (!m_instance)
                    m_instance = new GameObject("Projectile Pool").AddComponent<ProjectilePool>();
                return m_instance;
            }
        }
        private static ProjectilePool m_instance;
        public Dictionary<BasicProjectile, Queue<BasicProjectile>> projectilePrefabPool = new Dictionary<BasicProjectile, Queue<BasicProjectile>>();
        public static BasicProjectile GetInstance(BasicProjectile prefab) => instance.GetProjectileInstance(prefab);
        public BasicProjectile GetProjectileInstance(BasicProjectile prefab)
        {
            if (projectilePrefabPool.ContainsKey(prefab) && projectilePrefabPool[prefab].Count > 0)
                return DequeueInstance(prefab);
            else return CreateInstance(prefab);
        }
        private BasicProjectile CreateInstance(BasicProjectile projectilePrefab) 
        {
            BasicProjectile new_projectile = Instantiate(projectilePrefab.gameObject, transform).GetComponent<BasicProjectile>();
            new_projectile.gameObject.SetActive(true);
            new_projectile.onDestroy.AddListener(() => Enpool(projectilePrefab, new_projectile));
            return new_projectile;
        }
        private BasicProjectile DequeueInstance(BasicProjectile projectilePrefab) 
        {
            BasicProjectile new_projectile = projectilePrefabPool[projectilePrefab].Dequeue();
            new_projectile.gameObject.SetActive(true);
            return new_projectile;
        }
        private void Enpool(BasicProjectile prefab, BasicProjectile instance) 
        {
            instance.gameObject.SetActive(false);
            instance.onColliderHit.RemoveAllListeners();
            if (!projectilePrefabPool.ContainsKey(prefab))
                projectilePrefabPool.Add(prefab, new Queue<BasicProjectile>());
            projectilePrefabPool[prefab].Enqueue(instance);
        }
    }
}
