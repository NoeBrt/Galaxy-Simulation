using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CollisionSimulation : MonoBehaviour
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
    float radius;
float borderSize=300f;
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

        starCount = (int)UiValues["StarCount"];
        radius = 20f;


        SetupShader(starCount);
        InitStarsAttribute(starCount, 0.01f);
        InitStarsPos(starCount, radius);
        simulationStarted = true;
        Debug.Log(galaxy.Count);


    }
    public struct Particule
    {
        public Vector3 position;
        public Vector3 velocity;
        public float radius;
    };
    void Update()
    {
        if (simulationStarted)
        {

            RunComputeShader();
            var data = new Particule[starCount];
            starsBuffer.GetData(data);
            Debug.Log(data[0].velocity);
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
            kernel = computeShader.FindKernel("UpdateStar");
        }
        catch
        {
            Debug.Log("kernel not found");
        }
        int bufferStride = sizeof(float) * 7;
        starsBuffer = new ComputeBuffer(starCount, bufferStride);
        computeShader.SetBuffer(kernel, "star", starsBuffer);

    }

    public void InitStarsAttribute(int n, float deltaTime)
    {
        computeShader.SetInt("starCount", n);
        computeShader.SetFloat("deltaTime", deltaTime);
        computeShader.SetFloat("borderSize", borderSize);

    }

    public void InitStarsPos(int n,float radius)
    {
        galaxy = new List<Particule>();
        starsGm = new List<GameObject>();
        for (int i = 0; i < n; i++)
        {
            Particule p = new Particule();
            p.position = new Vector3(Random.Range(-80,80f),Random.Range(-80f,80f), 0);
            Debug.Log(p.position);
            p.velocity =new Vector3(Random.Range(-1f,1),Random.Range(-1,1), 0).normalized*20f;
            p.radius = radius;	
            galaxy.Add(p);
            starsGm.Add(Instantiate(starPrefab, p.position, Quaternion.identity));

        }
        starsBuffer.SetData(galaxy);
    }


   

    public void Delete()
    {

        if (starsBuffer != null)
        {
            starsGm.ForEach(star => Destroy(star));
            starsGm.Clear();
        }
        if (starsBuffer != null)
            starsBuffer.Dispose();
        simulationStarted = false;
    }

}
