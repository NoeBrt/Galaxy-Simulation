using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
namespace Simulation
{
    public interface IBodiesInitializer
    {
        public Star[] InitStars();
    }
}