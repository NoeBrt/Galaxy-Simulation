using UnityEngine.UI;
using UnityEngine;
using TMPro;

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
    public TMP_Dropdown TypeDropdown=> typeDropdown;



    // Ajouter d'autres sliders en fonction de vos besoins



    // Autres méthodes liées à la gestion de l'interface utilisateur
}

