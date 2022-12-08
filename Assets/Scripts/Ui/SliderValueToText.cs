using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderValueToText : MonoBehaviour
{
    [SerializeField] Slider sliderUI;
    private Text textSliderValue;
    Vector3 offset = new Vector3(0, 0, 20);

    void Start()
    {
        textSliderValue = GetComponent<Text>();

    }
    private void Update()
    {
        textSliderValue.transform.localPosition = sliderUI.handleRect.localPosition;
    }

    public void Text(float value)
    {

        textSliderValue.text = System.Math.Round(value, 2).ToString();
    }
}