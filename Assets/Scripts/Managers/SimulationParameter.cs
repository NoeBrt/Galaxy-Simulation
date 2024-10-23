using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulation.Struct;

namespace Simulation
{
    public enum SimulationType
    {
        Galaxy,
        Collision,
        Universe
    }
    public class SimulationParameter : MonoBehaviour
    {

        public float BodiesCount{get; set;}

        public float Radius{get; set;}
        public float Thickness{get; set;}//utile seulement pour le type galaxies

        public float InitialVelocity{get; set;}

        public float SmoothingLength{get; set;}

        public float BlackHoleMass{get; set;}
        public float Distance{get; set;}

        public float InteractionRate{get; set;}
        public float TimeStep{get; set;}
        public DynamicColor Color{get; set;}

        public SimulationType simulationType { get; set; }

        public void Init(int simulationType)
        {
            this.simulationType = (SimulationType)simulationType;
            SimulationDefaults defaults = GlobalManager.Instance.DefaultsList[simulationType];
            Color = defaults.color;

        }

    }

}
