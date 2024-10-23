using System.Threading.Tasks;
using UnityEngine;
using Simulation.Struct;
using System.Linq;
namespace Simulation
{
    public class CollisionInitializer : BodiesInitializer
    {
        GalaxyInitializer galaxInit;
        float DistanceBetweenStars;

        public CollisionInitializer(SimulationParameter simulationParameter) : base(simulationParameter)
        {
            galaxInit = new GalaxyInitializer(simulationParameter);
            DistanceBetweenStars = simulationParameter.Distance;
        }


        public override Particule[] InitStars()
        {

            Particule[] galaxies1 = galaxInit.InitStars(Vector3.left * DistanceBetweenStars, base.bodiesCount / 2);
            Particule[] galaxies2 = galaxInit.InitStars(Vector3.right * DistanceBetweenStars, base.bodiesCount % 2 == 0 ? base.bodiesCount / 2 : base.bodiesCount / 2 + 1);
            Particule[] allGalaxies = new Particule[base.bodiesCount];
            //rotate the galaxie2 at 90° on the x axis, the veloty has to be rotated too
            for (int i = 0; i < galaxies2.Length; i++)
            {
                var star = galaxies2[i];
                var x = star.position.x;
                var y = star.position.y * Mathf.Cos(90f) - star.position.z * Mathf.Sin(90f);
                var z = star.position.z * Mathf.Cos(90f) + star.position.y * Mathf.Sin(90f);
                var velocityX = star.velocity.x;
                var velocityY = star.velocity.y * Mathf.Cos(90f) - star.velocity.z * Mathf.Sin(90f);
                var velocityZ = star.velocity.z * Mathf.Cos(90f) + star.velocity.y * Mathf.Sin(90f);
                galaxies2[i] = new Particule() { position = new Vector3(x, y, z), velocity = new Vector3(velocityX, velocityY, velocityZ) };
                //return the two galaxies array
            }




            for (int i = 0; i < galaxies1.Length; i++)
            {
                Particule p1 = galaxies1[i];
                if (i % 2 == 0)
                    allGalaxies[i] = p1;
            }

            for (int i = 0; i < galaxies2.Length; i++)
            {
                Particule p2 = galaxies2[i];
                if (i % 2 != 0)
                    allGalaxies[i] = p2;
            }


            return allGalaxies;
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
