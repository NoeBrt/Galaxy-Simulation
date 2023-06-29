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

    // Start is called before the first frame update
    public void Spawn()
    {
        Delete();
        UiValues = new Dictionary<string, float>();
        sliders.ForEach(slider => UiValues.Add(slider.name, slider.value));

        float galaxyRadius = UiValues["GalaxyRadius"];
        float galaxyThickness = UiValues["GalaxyThickness"];
        float starInitialVelocity = UiValues["StarInitialVelocity"];
        int starCount = (int)UiValues["StarCount"];

        root = new TreeNode(50, Vector3.zero, galaxyRadius*2f);
        List<Star> stars = new List<Star>();

        for (int i = 0; i < starCount; i++)
        {
            Vector3 randomPosition = DiscPos(Random.Range(0f, 360f), Random.Range(-galaxyThickness, galaxyThickness), Random.Range(-galaxyRadius, galaxyRadius));
            Quaternion rotation = starPrefab.transform.rotation;

            Star star = Instantiate(starPrefab, randomPosition, rotation, transform);
            star.velocity = new Vector3((star.transform.position.x * Mathf.Cos(90f)) - (star.transform.position.z * Mathf.Sin(90f)), Random.Range(-1f, 1f), (star.transform.position.z * Mathf.Cos(90f)) + (star.transform.position.x * Mathf.Sin(90f))).normalized * starInitialVelocity;

            stars.Add(star);
            root.InsertToNode(star);
        }

        galaxy = new Galaxy(stars, galaxyRadius, galaxyRadius, starInitialVelocity);
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
                star.velocity += root.CalculateTreeForce(star, UiValues["Teta"]);
                star.transform.position += star.velocity * Time.deltaTime;
                star.CameraView();
                setStarColor(star);
            }
            root.ComputeMassDistribution(UiValues["BlackHoleMass"]);
             center_galaxy();
           // displayInfoCount();

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
