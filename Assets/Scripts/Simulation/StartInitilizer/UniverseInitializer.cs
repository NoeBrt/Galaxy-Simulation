using System.Threading.Tasks;
using UnityEngine;
using Simulation.Struct;
namespace Simulation
{
    public class UniverseInitializer : BodiesInitializer
    {
        float diameter;


        public UniverseInitializer(SimulationParameter simulationParameter) : base(simulationParameter)
        {
            this.diameter = simulationParameter.Radius;

        }


        public override Particule[] InitStars()
        {
            var data = new Particule[base.bodiesCount];
            for (int i = 0; i < base.bodiesCount; i++)
            {
                var star = new Particule();
                star.position = insideSphere(diameter);
                star.velocity = SphereVelocity(base.initialVelocity, star, diameter);
                data[i] = star;
            }
            return data;
        }


        private Vector3 insideSphere(float diameter)
        {
            Vector3 discPoint = Random.insideUnitSphere * (diameter / 2f);

            return discPoint;
        }

        private Vector3 SphereVelocity(float starInitialVelocity, Particule star, float diameter)
        {
            var speed = star.position;

            return speed.normalized * (speed.magnitude / (diameter / 2f)) * starInitialVelocity;
        }
    }
}