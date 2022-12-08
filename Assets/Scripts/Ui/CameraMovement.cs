using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToTarget = 10;
    [SerializeField] private float mouseScroolSpeed = 100f;

    private Vector3 previousPosition;

    public float DistanceToTarget { get => distanceToTarget; set => distanceToTarget = value; }

    private void LateUpdate()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            {
                previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            }
        }
        cam.transform.position = target.position;

        Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        Vector3 direction = previousPosition - newPosition;
        float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
        float rotationAroundXAxis = direction.y * 180; // camera moves vertically
        if (Input.GetMouseButton(0))
        {
            cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <— This is what makes it work!
        }
        DistanceToTarget += -Input.mouseScrollDelta.y * mouseScroolSpeed * Time.deltaTime;
        cam.transform.Translate(new Vector3(0, 0, -DistanceToTarget));

        previousPosition = newPosition;



    }
}