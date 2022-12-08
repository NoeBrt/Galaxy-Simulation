using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class Galaxy
{
    public Galaxy(List<Star> stars, float radius, float thickness, float starsInitialVelocity)
    {
        Stars = stars;
        Radius = radius;
        Thickness = thickness;
        StarsInitialVelocity = starsInitialVelocity;
    }

    public List<Star> Stars { get; set; }
    public float Radius { get; set; }
    public float Thickness { get; set; }
    public float StarsInitialVelocity { get; set; }


  




}