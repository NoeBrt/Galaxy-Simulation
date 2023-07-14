using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Simulation.Struct;
namespace Simulation
{
    public abstract class BodiesInitializer
    {
      protected float initialVelocity;
      protected int bodiesCount;

        public BodiesInitializer(SimulationParameter simulationParameter)
        {
            this.initialVelocity =simulationParameter.InitialVelocity;
            this.bodiesCount =(int)simulationParameter.BodiesCount;
        }

        public virtual Particule[] InitStars(){
            return new Particule[bodiesCount];
        }
    }
}