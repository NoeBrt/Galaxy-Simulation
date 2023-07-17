using UnityEngine.UI;
using UnityEngine;
using TMPro;
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
        [SerializeField] private Slider interactionRateSlider;
        [SerializeField] private Slider timeStepSlider;
        [SerializeField] private TMP_Dropdown typeDropdown;

        public Slider BodiesCountSlider => bodiesCountSlider;
        public Slider RadiusSlider => radiusSlider;
        public Slider ThicknessSlider => thicknessSlider;
        public Slider InitialVelocitySlider => initialVelocitySlider;
        public Slider SmoothingLengthSlider => smoothingLengthSlider;
        public Slider BlackHoleMassSlider => blackHoleMassSlider;
        public Slider InteractionRateSlider => interactionRateSlider;
        public Slider TimeStepSlider => timeStepSlider;
        public TMP_Dropdown TypeDropdown => typeDropdown;




        public void setSliderValue(SimulationDefaults defaults)
        {
            bodiesCountSlider.value = defaults.bodiesCount;
            radiusSlider.value = defaults.radius;
            thicknessSlider.value = defaults.thickness;
            initialVelocitySlider.value = defaults.initialVelocity;
            smoothingLengthSlider.value = defaults.smoothingLength;
            blackHoleMassSlider.value = defaults.blackHoleMass;
            interactionRateSlider.value = defaults.interactionRate;
            // timeStepSlider.value = defaults.timeStep;
        }


        public void init(int value)
        {
            SimulationDefaults defaults = GlobalManager.Instance.DefaultsList[value];
            setSliderValue(defaults);
            GlobalManager.Instance.SimulationParameter.Init(value);
            switch ((SimulationType)value)
            {
                case SimulationType.Galaxy:
                    BlackHoleMassSlider.GetComponent<SliderUi>().SetSliderInteractable(true);
                    ThicknessSlider.GetComponent<SliderUi>().SetSliderInteractable(true);
                    BodiesCountSlider.GetComponent<SliderUi>().Label.text = "Number of Stars";
                    RadiusSlider.GetComponent<SliderUi>().Label.text = "Galaxy Radius";
                    ThicknessSlider.GetComponent<SliderUi>().Label.text = "Galaxy Thickness";
                    initialVelocitySlider.GetComponent<SliderUi>().Label.text = "Stars Initial Velocity";
                    break;
                case SimulationType.Collision:
                    BlackHoleMassSlider.GetComponent<SliderUi>().SetSliderInteractable(false);
                    BodiesCountSlider.GetComponent<SliderUi>().Label.text = "Number of Stars";
                    RadiusSlider.GetComponent<SliderUi>().Label.text = "Galaxies Radius";
                    ThicknessSlider.GetComponent<SliderUi>().Label.text = "Galaxies Thickness";
                    initialVelocitySlider.GetComponent<SliderUi>().Label.text = "Stars Initial Velocity";
                    break;
                case SimulationType.Universe:
                    BlackHoleMassSlider.GetComponent<SliderUi>().SetSliderInteractable(false);
                    ThicknessSlider.GetComponent<SliderUi>().SetSliderInteractable(false);
                    BodiesCountSlider.GetComponent<SliderUi>().Label.text = "Number of Galaxies";
                    RadiusSlider.GetComponent<SliderUi>().Label.text = "Universe Radius";
                    initialVelocitySlider.GetComponent<SliderUi>().Label.text = "Galaxies Initial Velocity";
                    break;

            }
        }
    }

}