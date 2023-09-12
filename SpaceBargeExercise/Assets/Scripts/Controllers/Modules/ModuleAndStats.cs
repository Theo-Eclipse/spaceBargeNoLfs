using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flier
{
    [System.Serializable]
    public struct FlierStats
    {
        public float health;
        public float maxVelocity;
        public float maxAngularVelocity;
        public float forwardSpeed;
        public float maneurSpeed;
        public float rotationSpeed;
        public float velocityDumping;
    }
}
