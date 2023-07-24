using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private int bodiesCount;

        private float radius;
        private float thickness; //utile seulement pour le type galaxies

        private float initialVelocity;

        private float smoothingLength;

        private float blackHoleMass;

        private float interactionRate;
        private float timeStep;


        public float Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                GlobalManager.Instance.UIManager.RadiusSlider.value = value;
            }
        }

        public float Thickness
        {
            get { return thickness; }
            set
            {
                thickness = value;
                GlobalManager.Instance.UIManager.ThicknessSlider.value = value;
            }
        }


        public float InitialVelocity
        {
            get { return initialVelocity; }
            set
            {
                initialVelocity = value;
                GlobalManager.Instance.UIManager.InitialVelocitySlider.value = value;
            }
        }

        public float SmoothingLength
        {
            get { return smoothingLength; }
            set
            {
                smoothingLength = value;
                GlobalManager.Instance.UIManager.SmoothingLengthSlider.value = value;
            }
        }

        public float BlackHoleMass
        {
            get { return blackHoleMass; }
            set
            {
                blackHoleMass = value;
                GlobalManager.Instance.UIManager.BlackHoleMassSlider.value = value;
            }
        }

        public float InteractionRate
        {
            get { return interactionRate; }
            set
            {
                interactionRate = value;
                GlobalManager.Instance.UIManager.InteractionRateSlider.value = value;
            }
        }

        public float BodiesCount
        {
            get { return (int)bodiesCount; }
            set
            {
                bodiesCount = (int)value;
                GlobalManager.Instance.UIManager.BodiesCountSlider.value = bodiesCount;
            }
        }
        public float TimeStep
        {
            get { return timeStep; }
            set
            {
                timeStep = value;
                GlobalManager.Instance.UIManager.TimeStepSlider.value = value;
            }
        }


        public SimulationType simulationType { get; set; }


        public void Init(int simulationType)
        {
            this.simulationType = (SimulationType)simulationType;
        }

    }

}
