using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUi : MonoBehaviour
{
    [SerializeField] private Text label;
    [SerializeField] private Text value;
    public Text Value { get => value; set => this.value = value; }
    public Text Label { get => label; set => label = value; }

    public void SetSliderInteractable(bool state)
    {
        if (state == GetComponent<Slider>().interactable) return;
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