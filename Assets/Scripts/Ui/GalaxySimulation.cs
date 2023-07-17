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
            Vector3 pos = DiscPos(Random.Range(0f, 360f), Random.Range(-UiValues["GalaxyThickness"], UiValues["GalaxyThickness"]), Random.Range(-UiValues["GalaxyRadius"], UiValues["GalaxyRadius"]));
            stars.Add(Instantiate(starPrefab, pos, starPrefab.transform.rotation, transform));
            stars[i].velocity = new Vector3((stars[i].transform.position.x * Mathf.Cos(90f)) - (stars[i].transform.position.z * Mathf.Sin(90f)), 0f, (stars[i].transform.position.z * Mathf.Cos(90f)) + (stars[i].transform.position.x * Mathf.Sin(90f))).normalized * UiValues["StarInitialVelocity"];
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
        {
            galaxy.Stars.ForEach(star => star.UpdatePosition(galaxy, UiValues["SmoothingLenght"]));
           // center_galaxy();

        }

    }

    void center_galaxy()
    {
        Vector3 mass_center = Vector3.zero;
        foreach (Star star in galaxy.Stars)
            mass_center += star.transform.position;

        mass_center /= galaxy.Stars.Count;
        foreach (Star star in galaxy.Stars)
        {
            star.transform.position -= mass_center;
        }
    }


    Vector3 DiscPos(float a, float thickness, float radius)
    {
        return new Vector3(radius * Mathf.Cos(a), thickness, radius * Mathf.Sin(a));
    }
}
