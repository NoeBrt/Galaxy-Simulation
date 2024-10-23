using UnityEngine;
using UnityEngine.UI;
using Simulation;
using System.Collections;
using System.Collections.Generic;
using Simulation.Struct;
public class ChangeColorOnClick : MonoBehaviour
{
    public Button myButton;      // Reference to the button
    public Color startColor;    // Color to change to
    public Color endColor;
    public float divider;    // Color to change to

    void Start()
    {
        // Add a listener to the button's onClick event
        myButton.onClick.AddListener(ChangeColor);
    }

    void ChangeColor()
    {
    var dynColor= new DynamicColor();
    dynColor.colorStart = startColor;
    dynColor.colorEnd = endColor;
    dynColor.divider = divider;
        // Change the color of the target object's material (assuming it has a Renderer)
    GlobalManager.Instance.SimulationParameter.Color=dynColor;
    }
}
