using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flier.Modules
{
    [System.Serializable]
    public class ModuleInfo
    {
        public string name;
        [TextArea]public string description;
        public Sprite moduleIcon;
    }
}
