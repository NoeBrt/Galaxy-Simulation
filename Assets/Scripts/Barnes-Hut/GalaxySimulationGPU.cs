using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GalaxySimulationGPU : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<Slider> sliders;
    public Dictionary<string, float> UiValues { get; set; }
    //[SerializeField] public List<Star> Galaxy { get; set; }
    bool simulationStarted = false;
    [SerializeField] ComputeShader computeShader;
    private int kernel;
    [SerializeField]
    private ComputeBuffer starsBuffer;
    private List<Particule> galaxy;
    [SerializeField] private GameObject starPrefab;
    private List<GameObject> starsGm;

    int starCount;

    [SerializeField]
    private Material renderMaterial;
    [SerializeField]
    private bool render = true;
    [SerializeField]
    private float particleSize = 0.01f;
    // Start is called before the first frame update
    public void Spawn()
    {
        Delete();
        UiValues = new Dictionary<string, float>();
        sliders.ForEach(slider => UiValues.Add(slider.name, slider.value));
        float galaxyRadius = UiValues["GalaxyRadius"];
        float galaxyThickness = UiValues["GalaxyThickness"];
        float starInitialVelocity = UiValues["StarInitialVelocity"];
        starCount = (int)UiValues["StarCount"];
        float smoothingLenght = UiValues["SmoothingLenght"];
        float interactionRate = UiValues["InteractionRate"];
        SetupShader(starCount);
        InitStarsAttribute(starCount, Time.deltaTime, smoothingLenght, interactionRate);
        InitStarsPos(starCount, galaxyRadius, galaxyThickness, starInitialVelocity);
        simulationStarted = true;
        Debug.Log(galaxy.Count);


    }
    public struct Particule
    {
        public Vector3 position;
        public Vector3 velocity;
    };
    void Update()
    {
        if (simulationStarted)
        {

            RunComputeShader();
             var data = new Particule[starCount];
                starsBuffer.GetData(data);
            for (int i = 0; i < starCount; i++)
            {
               
                starsGm[i].transform.position = data[i].position;
                starsGm[i].GetComponent<Star>().CameraView();
            }
        }
    }
    void RunComputeShader()
    {
        computeShader.Dispatch(kernel, starCount, 1, 1);
    }

    public void SetupShader(int n)
    {
        try
        {
            kernel = computeShader.FindKernel("UpdateStars");
        }
        catch
        {
            Debug.Log("kernel not found");
        }
        int bufferStride = sizeof(float) * 6;
        starsBuffer = new ComputeBuffer(starCount, bufferStride);
        computeShader.SetBuffer(kernel, "star", starsBuffer);

    }

    public void InitStarsAttribute(int n, float deltaTime, float smoothingLenght, float interactionRate)
    {
        computeShader.SetInt("starCount", n);
        computeShader.SetFloat("deltaTime", deltaTime);
        computeShader.SetFloat("smoothingLenght", smoothingLenght);
        computeShader.SetFloat("interactionRate", interactionRate);


    }

    public void InitStarsPos(int n, float diameter, float thickness, float starInitialVelocity)
    {
        galaxy = new List<Particule>();
        starsGm = new List<GameObject>();
        for (int i = 0; i < n; i++)
        {
            Particule p = new Particule();
            p.position = insideCylinder(diameter, thickness);
            Debug.Log(p.position);
            p.velocity += DiscVelocity(starInitialVelocity, p);
            galaxy.Add(p);
            starsGm.Add(Instantiate(starPrefab, p.position, Quaternion.identity));

        }
        starsBuffer.SetData(galaxy);
    }


    private Vector3 insideCylinder(float diameter, float thickness)
    {
        Vector2 discPoint = Random.insideUnitCircle * (diameter / 2f);
        float yPosition = Random.Range(-thickness / 2f, thickness / 2f);

        return new Vector3(discPoint.x, yPosition, discPoint.y);
    }

    private Vector3 DiscVelocity(float starInitialVelocity, Particule star)
    {
        var x = (star.position.x * Mathf.Cos(90f)) - (star.position.z * Mathf.Sin(90f));
        var z = (star.position.z * Mathf.Cos(90f)) + (star.position.x * Mathf.Sin(90f));
        var y = Random.Range(-1f, 1f);
        return new Vector3(x, y, z).normalized * starInitialVelocity;

    }


    void OnRenderObject()
    {
        if (render)
        {
            renderMaterial.SetFloat("_Size", particleSize);
            renderMaterial.SetPass(0);
            renderMaterial.SetBuffer("bodies", starsBuffer);
            Graphics.DrawProceduralNow(MeshTopology.Points, starCount);
        }
    }

    public void Delete()
    {

        if (starsBuffer!=null)
        {
            starsGm.ForEach(star => Destroy(star));
            starsGm.Clear();
        }
        if (starsBuffer != null)
            starsBuffer.Dispose();
        simulationStarted = false;
    }

}
