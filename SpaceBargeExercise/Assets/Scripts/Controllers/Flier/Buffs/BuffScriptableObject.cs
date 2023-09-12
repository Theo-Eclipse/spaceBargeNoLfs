using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flier.BuffSystem
{
    public abstract class BuffScriptableObject : ScriptableObject 
    {
        public BuffInfo buffInfo;
        public abstract void GetEffect(out BuffEffect<BasicFlier> effect);
    }
}
