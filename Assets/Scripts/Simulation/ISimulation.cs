using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simulation
{
    public interface ISimulation
    {
        public void Start();
        public void Stop();
        public void Update();
    }
}