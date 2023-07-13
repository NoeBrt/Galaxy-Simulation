using System.Threading.Tasks;
using UnityEngine;

namespace Simulation
{
    public class GalaxyInitializer : IBodiesInitializer
    {
        SimulationController simulationController;


        public GalaxyInitializer(SimulationController simulationController)
        {
            this.simulationController = simulationController;
        }

        
        public Star[] InitStars()
        {
            throw new System.NotImplementedException();
        }
        public Vector3 InitPosition(float diameter, float ){
            throw new System.NotImplementedException();


        }
        public Vector3 InitVelocity(){

            throw new System.NotImplementedException();

        }

    }


}