using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flier.Weapons
{
    [System.Serializable]
    public struct WeaponStats
    {
        public float damagePerHit;
        public float speed;
        public float shootInterval;
        public float spread;
        public int projectileCount;
        public float profectileLifeTime;
    }
}
