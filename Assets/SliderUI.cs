using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUi : MonoBehaviour
{
    [SerializeField] private Text label;
    [SerializeField] private Text value;
    [SerializeField] private Image fill;

    public Text Value { get => value; set => this.value = value; }
    public Text Label { get => label; set => label = value; }
    public Image Fill { get => fill; set => fill = value; }

    public void SetSliderInteractable(bool state)
    {
        if (state == GetComponent<Slider>().interactable) return;
        GetComponent<Slider>().interactable = state;
        SetLabelOpacity(state);
    }

    private void SetLabelOpacity(bool isInteractable)
    {
        Color labelColor = label.color;
        labelColor.a = isInteractable ? 1f : 0.3f;
        label.color = labelColor;
        Color valueColor = value.color;
        valueColor.a = isInteractable ? 1f : 0.5f;
        Value.color = labelColor;
        Color fillColor = fill.color;
        fillColor.a = isInteractable ? 1f : 0.3f;
        Fill.color = fillColor;
    }
}