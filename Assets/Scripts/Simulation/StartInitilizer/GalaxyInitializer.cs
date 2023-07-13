using System.Threading.Tasks;
using UnityEngine;
using Simulation.Struct;
namespace Simulation
{
    public class GalaxyInitializer : BodiesInitializer
    {
        float thickness;
        float diameter;


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
                star.velocity = DiscVelocity(base.initialVelocity, star,Vector3.zero);
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
                star.velocity = DiscVelocity(base.initialVelocity, star,center) ;
                data[i] = star;
            }
            return data;
        }

        private Vector3 insideCylinder(float diameter, float thickness)
        {
            Vector2 discPoint = Random.insideUnitCircle * (diameter / 2f);
            float yPosition = Random.Range(-thickness / 2f, thickness / 2f);

            return new Vector3(discPoint.x, yPosition, discPoint.y);
        }

        private Vector3 DiscVelocity(float starInitialVelocity, Particule star, Vector3 center)
        {
            Vector3 direction = (center - star.position).normalized; // direction from p1 to P
            Vector3 up = new Vector3(0, 1, 0); // Up vector
            Vector3 velocityDirection = Vector3.Cross(up, direction); // Perpendicular direction
            float speed = starInitialVelocity; // Speed magnitude
            Vector3 velocity = velocityDirection * speed;

            return velocity;

           /*    var x = (star.position.x * Mathf.Cos(90f)) - (star.position.z * Mathf.Sin(90f));
            var z = (star.position.z * Mathf.Cos(90f)) + (star.position.x * Mathf.Sin(90f));
            var y = Random.Range(-1f, 1f);
            return new Vector3(x, y, z).normalized * starInitialVelocity;*/
         

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