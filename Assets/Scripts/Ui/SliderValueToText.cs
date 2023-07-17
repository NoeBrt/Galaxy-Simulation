using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderValueToText : MonoBehaviour
{
    [SerializeField] Slider sliderUI;
    private Text textSliderValue;

    void OnEnable()
    {
        textSliderValue = GetComponent<Text>();
        //textSliderValue.text = sliderUI.value + "";

    }
    private void Update()
    {
     //   textSliderValue.transform.localPosition = sliderUI.handleRect.localPosition;
    }

    public void Text(float value)
    {
        Debug.Log(value);
        Debug.Log(textSliderValue.text);

        textSliderValue.text = System.Math.Round(value, 3).ToString();
    }
}