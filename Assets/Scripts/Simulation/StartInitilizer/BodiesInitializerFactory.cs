using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Simulation
{
    public class BodiesInitializerFactory
    {
        public static BodiesInitializer Create(SimulationParameter simulationParameter)
        {
            switch (simulationParameter.simulationType)
            {
                case SimulationParameter.SimulationType.Universe:
                    return new UniverseInitializer(simulationParameter);
                case SimulationParameter.SimulationType.Galaxy:
                    return new GalaxyInitializer(simulationParameter);
                case SimulationParameter.SimulationType.Collision:
                    return new CollisionInitializer(simulationParameter);
                default:
                    throw new Exception("Initializer type not found");
            }

        }
    }

}