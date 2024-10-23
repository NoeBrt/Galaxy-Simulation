using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
namespace Simulation
{

    public class UIManager : MonoBehaviour
    {
        // Assumer que chaque curseur est assigné dans l'inspecteur Unity
        [SerializeField] private Slider bodiesCountSlider;
        [SerializeField] private Slider radiusSlider;
        [SerializeField] private Slider thicknessSlider;
        [SerializeField] private Slider initialVelocitySlider;
        [SerializeField] private Slider smoothingLengthSlider;
        [SerializeField] private Slider blackHoleMassSlider;
        [SerializeField] private Slider galaxiesDistanceSlider;
        [SerializeField] private Slider interactionRateSlider;
        [SerializeField] private Slider timeStepSlider;
        [SerializeField] private TMP_Dropdown typeDropdown;
        [SerializeField] private List<Button> ColorButtons;

        public Slider BodiesCountSlider => bodiesCountSlider;
        public Slider RadiusSlider => radiusSlider;
        public Slider ThicknessSlider => thicknessSlider;
        public Slider InitialVelocitySlider => initialVelocitySlider;
        public Slider SmoothingLengthSlider => smoothingLengthSlider;
        public Slider BlackHoleMassSlider => blackHoleMassSlider;
        public Slider GalaxiesDistance => galaxiesDistanceSlider;
        public Slider InteractionRateSlider => interactionRateSlider;
        public Slider TimeStepSlider => timeStepSlider;
        public TMP_Dropdown TypeDropdown => typeDropdown;
        public List<Button> ColorButtonsList => ColorButtons;



        public void setSliderValue(SimulationDefaults defaults)
        {
            bodiesCountSlider.value = defaults.bodiesCount;
            radiusSlider.value = defaults.radius;
            thicknessSlider.value = defaults.thickness;
            initialVelocitySlider.value = defaults.initialVelocity;
            smoothingLengthSlider.value = defaults.smoothingLength;
            blackHoleMassSlider.value = defaults.blackHoleMass;
            interactionRateSlider.value = defaults.interactionRate;
            galaxiesDistanceSlider.value = defaults.distance;
            timeStepSlider.value = defaults.timeStep;
        }


        public void init(int value)
        {
            SimulationDefaults defaults = GlobalManager.Instance.DefaultsList[value];
            setSliderValue(defaults);
            GlobalManager.Instance.SimulationParameter.Init(value);
            Debug.Log(BodiesCountSlider.GetComponent<SliderUi>());
            switch ((SimulationType)value) // not proud of this
            {
                case SimulationType.Galaxy:
                    galaxiesDistanceSlider.gameObject.SetActive(false);
                    BlackHoleMassSlider.gameObject.SetActive(true);
                    BlackHoleMassSlider.GetComponent<SliderUi>().SetSliderInteractable(true);
                    BlackHoleMassSlider.GetComponent<SliderUi>().Label.text = "Black Hole Mass";
                    ThicknessSlider.GetComponent<SliderUi>().SetSliderInteractable(true);
                    BodiesCountSlider.GetComponent<SliderUi>().Label.text = "Number of Stars";
                    RadiusSlider.GetComponent<SliderUi>().Label.text = "Galaxy Radius";
                    ThicknessSlider.GetComponent<SliderUi>().Label.text = "Galaxy Thickness";
                    initialVelocitySlider.GetComponent<SliderUi>().Label.text = "Stars Initial Velocity";
                    ColorButtonsList.ForEach(button => button.GetComponent<ChangeColorOnClick>().divider = 200);
                    ColorButtonsList[2].GetComponent<ChangeColorOnClick>().divider = 100;
                    break;
                case SimulationType.Collision:
                    BlackHoleMassSlider.gameObject.SetActive(false);
                    galaxiesDistanceSlider.gameObject.SetActive(true);
                    galaxiesDistanceSlider.GetComponent<SliderUi>().SetSliderInteractable(true);
                    galaxiesDistanceSlider.GetComponent<SliderUi>().Label.text = "Galaxies Distance";
                    ThicknessSlider.GetComponent<SliderUi>().SetSliderInteractable(true);
                    BodiesCountSlider.GetComponent<SliderUi>().Label.text = "Number of Stars";
                    RadiusSlider.GetComponent<SliderUi>().Label.text = "Galaxies Radius";
                    ThicknessSlider.GetComponent<SliderUi>().Label.text = "Galaxies Thickness";
                    initialVelocitySlider.GetComponent<SliderUi>().Label.text = "Stars Initial Velocity";
                    ColorButtonsList.ForEach(button => button.GetComponent<ChangeColorOnClick>().divider = 200);
                    ColorButtonsList[2].GetComponent<ChangeColorOnClick>().divider = 100;
                    break;
                case SimulationType.Universe:
                    galaxiesDistanceSlider.gameObject.SetActive(false);
                    BlackHoleMassSlider.gameObject.SetActive(true);
                    BlackHoleMassSlider.GetComponent<SliderUi>().SetSliderInteractable(false);
                    BlackHoleMassSlider.GetComponent<SliderUi>().Label.text = "Black Hole Mass";
                    ThicknessSlider.GetComponent<SliderUi>().SetSliderInteractable(false);
                    BodiesCountSlider.GetComponent<SliderUi>().Label.text = "Number of Galaxies";
                    RadiusSlider.GetComponent<SliderUi>().Label.text = "Universe Radius";
                    initialVelocitySlider.GetComponent<SliderUi>().Label.text = "Galaxies Initial Velocity";
                    ColorButtonsList.ForEach(button => button.GetComponent<ChangeColorOnClick>().divider = 30);
                    break;

            }
        }

        SimulationType previousType;
        public void setCameraRotation()
        {
            Debug.Log(previousType + " " + GlobalManager.Instance.SimulationParameter.simulationType);
            if (previousType == GlobalManager.Instance.SimulationParameter.simulationType)
                return;

            switch (GlobalManager.Instance.SimulationParameter.simulationType)
            {
                case SimulationType.Galaxy:
                    Camera.main.transform.rotation = Quaternion.Euler(60, -3, 0);
                    Camera.main.GetComponent<CameraMovement>().AutoRotation = false;
                    Camera.main.GetComponent<CameraMovement>().DistanceToTarget = radiusSlider.value;

                    break;
                case SimulationType.Collision:
                    Camera.main.transform.rotation = Quaternion.Euler(90, 0, 0);
                    Camera.main.GetComponent<CameraMovement>().AutoRotation = false;
                    Camera.main.GetComponent<CameraMovement>().DistanceToTarget = radiusSlider.value * 2;


                    break;
                case SimulationType.Universe:
                    Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
                    Camera.main.GetComponent<CameraMovement>().AutoRotation = true;
                      Camera.main.GetComponent<CameraMovement>().DistanceToTarget = radiusSlider.value * 2;

                    break;

            }


            previousType = GlobalManager.Instance.SimulationParameter.simulationType;

        }
    }

}