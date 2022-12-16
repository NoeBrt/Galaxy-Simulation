using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GalaxySimulationBH : MonoBehaviour
{
    TreeNode root;
    // Start is called before the first frame update
    [SerializeField] private Star starPrefab;
    [SerializeField] private List<Slider> sliders;
    public Dictionary<string, float> UiValues { get; set; }
    //[SerializeField] public List<Star> Galaxy { get; set; }
    public Galaxy galaxy { get; set; }
    bool simulationStarted = false;
    [SerializeField] Color right;
    [SerializeField] Color left;



    // Start is called before the first frame update
    public void Spawn()
    {
        Delete();

        UiValues = new Dictionary<string, float>();
        sliders.ForEach(slider => UiValues.Add(slider.name, slider.value));

        root = new TreeNode(50, new Rect(-UiValues["GalaxyRadius"] * 3 / 2f, -UiValues["GalaxyRadius"] * 3 / 2f, UiValues["GalaxyRadius"] * 3, UiValues["GalaxyRadius"] * 3));

        List<Star> stars = new List<Star>();
        for (int i = 0; i < UiValues["StarCount"]; i++)
        {
            float angle = Random.Range(0f, 360f);
            Vector3 pos = DiscPos(angle, Random.Range(-UiValues["GalaxyThickness"], UiValues["GalaxyThickness"]), Random.Range(-UiValues["GalaxyRadius"], UiValues["GalaxyRadius"]));
            stars.Add(Instantiate(starPrefab, pos, starPrefab.transform.rotation, transform));
            if (pos.x > UiValues["GalaxyThickness"] / 2f)
                stars[i].GetComponent<SpriteRenderer>().color = right;
            else
                stars[i].GetComponent<SpriteRenderer>().color = left;

            stars[i].velocity = new Vector3((stars[i].transform.position.x * Mathf.Cos(90f)) - (stars[i].transform.position.z * Mathf.Sin(90f)), 0f, (stars[i].transform.position.z * Mathf.Cos(90f)) + (stars[i].transform.position.x * Mathf.Sin(90f))).normalized * UiValues["StarInitialVelocity"];

            root.insertToNode(stars[i]);

        }
        galaxy = new Galaxy(stars, UiValues["GalaxyRadius"], UiValues["GalaxyRadius"], UiValues["StarInitialVelocity"]);
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
        root = null;
        simulationStarted = false;

    }

    public void Update()
    {
        if (simulationStarted)
        {
            foreach (Star star in galaxy.Stars)
            {
                star.velocity += root.CalculateTreeForce(star, UiValues["Teta"], UiValues["SmoothingLenght"]);
                star.transform.position += star.velocity * Time.deltaTime;
                star.CameraView();
                // setStarColor(star);
            }
            root.ComputeMassDistribution(UiValues["BlackHoleMass"]);
            //  center_galaxy();
            displayInfoCount();
        }
    }

    void setStarColor(Star star)
    {
        Color color = star.GetComponent<SpriteRenderer>().material.color;
        color.g = Mathf.Clamp(1 / star.acceleration.magnitude, 0.35f, 1f);
        star.GetComponent<SpriteRenderer>().material.color = color;
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

    private void OnDrawGizmos()
    {
        if (root != null)
            root.DrawDebug();
    }

    public void displayInfoCount()
    {
        for (int i = 0; i < 4; i++)
        {
            if (root.childs[i] != null)
                Debug.Log("node n°: " + i + " " + root.childs[i].bodiesStored.Count);
            else
                Debug.Log("node n°:" + i + "Empty");

        }
    }
}
