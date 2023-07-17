using System.Threading.Tasks;
using UnityEngine;
using Simulation.Struct;
namespace Simulation
{
    public class GalaxyInitializer : BodiesInitializer
    {
        float thickness;
        float diameter;
        float centerConcentration;


        public GalaxyInitializer(SimulationParameter simulationParameter) : base(simulationParameter)
        {
            this.thickness = simulationParameter.Thickness;
            this.diameter = simulationParameter.Radius;

        }


        public override Particule[] InitStars()
        {
            var data = new Particule[base.bodiesCount];
            for (int i = 0; i < base.bodiesCount; i++)
            {
                var star = new Particule();
                star.position = insideCylinder(diameter, thickness);
                star.velocity = DiscVelocity(base.initialVelocity, star, Vector3.zero);
                data[i] = star;
            }
            return data;
        }

        public Particule[] InitStars(Vector3 center, int count)
        {
            var data = new Particule[count];
            for (int i = 0; i < count; i++)
            {
                var star = new Particule();

                star.position = insideCylinder(diameter, thickness) + center;
                star.velocity = DiscVelocity(base.initialVelocity, star, center);
                data[i] = star;
            }
            return data;
        }



        private Vector3 insideCylinder(float diameter, float thickness)
        {
            float fourthRootRadius = 1 - Mathf.Pow(Random.value, 0.5f);
            float angle = Random.value * 2 * Mathf.PI;

            Vector2 discPoint = new Vector2(fourthRootRadius * Mathf.Cos(angle), fourthRootRadius * Mathf.Sin(angle)) * (diameter / 2f);
            float yPosition = Random.Range(-thickness / 2f, thickness / 2f);

            return new Vector3(discPoint.x, yPosition, discPoint.y);
        }

        private Vector3 DiscVelocity(float starInitialVelocity, Particule star, Vector3 center)
        {
            Vector3 direction = (center - star.position); // direction from star to center
            float distance = direction.magnitude;
            Vector3 up = new Vector3(0, 1, 0); // Up vector
            Vector3 velocityDirection = Vector3.Cross(up, direction.normalized); // Perpendicular direction

            // Adjust the initial velocity based on distance to maintain stable circular orbits
            float adjustedInitialVelocity = starInitialVelocity*10f * Mathf.Sqrt(distance/diameter);

            // Add a small radial component to the velocity to give spiral arms
            float radialVelocityFactor = 0.01f; // Experiment with different values for this
            Vector3 radialComponent = direction.normalized * adjustedInitialVelocity * radialVelocityFactor;

            Vector3 velocity = velocityDirection * adjustedInitialVelocity + radialComponent;

            return velocity;


        }

        /*  private Vector3 DiscVelocity2(float starInitialVelocity, Particule star)
       {   
           Vector2 velocity = Vector3.Distance(star.position,Vector3.zero)/diameter * starInitialVelocity;

   Vector3 direction = (P.position - p1.position).normalized; // direction from p1 to P
   Vector3 up = new Vector3(0, 1, 0); // Up vector
   Vector3 velocityDirection = Vector3.Cross(up, direction); // Perpendicular direction
   float speed = ω * R; // Speed magnitude
   Vector3 velocity = velocityDirection * speed;

         //  var x = (star.position.x * Mathf.Cos(90f)) - (star.position.z * Mathf.Sin(90f));
          // var z = (star.position.z * Mathf.Cos(90f)) + (star.position.x * Mathf.Sin(90f));
           var y = Random.Range(-1f, 1f);
           return new Vector3(x, , z).normalized * starInitialVelocity;

       }*/

    }
}