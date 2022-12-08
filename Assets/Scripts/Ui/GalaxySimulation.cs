using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GalaxySimulation : MonoBehaviour
{
    [SerializeField] private Star starPrefab;
    [SerializeField] private List<Slider> sliders;
    public Dictionary<string, float> UiValues { get; set; }
    //[SerializeField] public List<Star> Galaxy { get; set; }
    public Galaxy galaxy { get; set; }
    bool simulationStarted = false;


    // Start is called before the first frame update
    public void Spawn()
    {
        Delete();
        UiValues = new Dictionary<string, float>();
        sliders.ForEach(slider => UiValues.Add(slider.name, slider.value));

        List<Star> stars = new List<Star>();
        for (int i = 0; i < UiValues["StarCount"]; i++)
        {
            stars.Add(Instantiate(starPrefab, new Vector3(Random.Range(-10f, 10f), Random.Range(-UiValues["GalaxyThickness"], UiValues["GalaxyThickness"]), Random.Range(-10f, 10f)), starPrefab.transform.rotation, transform));
        }
        galaxy = new Galaxy(stars, UiValues["GalaxyRadius"], UiValues["GalaxyThickness"], UiValues["StarInitialVelocity"]);
        simulationStarted = true;

    }
    public void Delete()
    {
        if (galaxy == null)
            return;
        foreach (Star star in galaxy.Stars)
        {
            Destroy(star.gameObject);
        }
        galaxy.Stars.Clear();
        simulationStarted = false;

    }

    public void Update()
    {
        if (simulationStarted)
            galaxy.Stars.ForEach(star => star.UpdatePosition(galaxy));
    }
}
