using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity;
public class Star : MonoBehaviour
{
    public Vector3 velocity { get; set; }
    public Vector3 acceleration { get; set; }


    // Start is called before the first frame update
    // Update is called once per frame
    public void UpdatePosition(Galaxy galaxy)
    {
        acceleration = Vector3.zero;
        foreach (Star star in galaxy.Stars)
        {
            if (star != this)
            {
                acceleration += (star.transform.position - transform.position).normalized / MathF.Pow(Vector3.Distance(transform.position, star.transform.position), 2);
            }
        }
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity;
        transform.rotation = Camera.main.transform.rotation;
        CameraMovement cameraMovement = Camera.main.GetComponent<CameraMovement>();
        transform.localScale = new Vector3(cameraMovement.DistanceToTarget / 500f, cameraMovement.DistanceToTarget / 500f);
        Debug.DrawRay(transform.position, velocity, Color.magenta);
    }
    public void CameraView()
    {
        transform.rotation = Camera.main.transform.rotation;
        CameraMovement cameraMovement = Camera.main.GetComponent<CameraMovement>();
        transform.localScale = new Vector3(cameraMovement.DistanceToTarget / 450f, cameraMovement.DistanceToTarget / 450f);
    }




}
