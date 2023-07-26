using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Simulation.Struct
{
    [System.Serializable]
   public struct DynamicColor
    {
        public Color colorStart;
        public Color colorEnd;
        public float divider;
    }
}