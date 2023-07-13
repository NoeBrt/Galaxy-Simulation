using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public static SimulationManager Instance { get; private set; }

    public float Radius { get; set; }
    public float Thickness { get; set; }
    public float InitialVelocity { get; set; }
    public float SmoothingLength { get; set; }
    public float BlackHoleMass { get; set; }
    public float InteractionRate { get; set; }
    public float BodiesCount { get; set; } //lmaao
    public float TimeStep { get; set; }
    public float Teta { get; set; }







    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }





}
