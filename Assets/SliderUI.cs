using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUi : MonoBehaviour
{
    [SerializeField] private Text label;
    [SerializeField] private Text value;
    public Text Value { get; set; }
    public Text Label { get ; set; }

    public void SetSliderInteractable(bool state)
    {
        GetComponent<Slider>().interactable = state;
        SetLabelOpacity(state);
    }

    private void SetLabelOpacity(bool isInteractable)
    {
        Color labelColor = label.color;
        labelColor.a = isInteractable ? 1f : 0.5f;
        label.color = labelColor;
    }
}