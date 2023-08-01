using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class floatingUI : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    public static bool isDragging;
    private Vector3 offset;
    private Vector3 initialMousePos;
    private bool selected;
    [SerializeField] Color selectedColor;
    [SerializeField] Image image;
    Color originalColor;
    private void Awake() {
        originalColor = image.color;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition))
        {
            GameObject clickedObject = GetObjectUnderMouse();

            // Check for the specific tag, and return if the click is not on the parent
            if (clickedObject && !clickedObject.GetComponent<floatingUI>())
                return;

            initialMousePos = Input.mousePosition;
            offset = transform.position - Input.mousePosition;
            selected = true;
        }

        if (Input.GetMouseButton(0) && selected)
        {
            isDragging = true;
            image.color = selectedColor;
            Vector3 currentMousePos = Input.mousePosition;
            Vector3 newPosition = currentMousePos + offset;
            transform.position = newPosition;
        }

        if (Input.GetMouseButtonUp(0) && selected)
        {
            isDragging = false;
            selected = false;
            image.color = originalColor;
        }
    }

    // Method to detect if the pointer is over a UI element
    private GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        if (raycastResults.Count > 0)
        {
            // Return the first hit object (if any)
            return raycastResults[0].gameObject;
        }

        return null; // Nothing under the mouse
    }
}
