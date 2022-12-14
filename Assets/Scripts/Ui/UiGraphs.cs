using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class UiGraphs : MonoBehaviour
{
    UiLineRenderer graphVelTime;
    [SerializeField] List<GameObject> Graphs;
    List<UiLineRenderer> GraphLines;
    private WriteCSV csv;

    public bool GraphStart { get; set; }
    public static float timer;
    int index = 0;

    private void Start()
    {
        csv = new WriteCSV();
        GraphLines = new List<UiLineRenderer>();
        foreach (GameObject graph in Graphs)
        {
            GraphLines.Add(graph.GetComponentInChildren<UiLineRenderer>());
        }
    }
    private void Update()
    {
        if (GraphStart)
        {

            setFpsTimeGraph();
            setCpuUsageGraph();
            UpdateGridSize();
            csv.data.Add(new CsvModel(timer, 1f / Time.unscaledDeltaTime, UiPerformance.CpuUsage));
            selectGraph();
            timer += Time.deltaTime;
        }
    }

    // Start is called before the first frame update
    public void initGraph()
    {
        csv.data.Clear();
        foreach (UiLineRenderer line in GraphLines)
        {
            line.points.Clear();
            line.grid.gridSize = new Vector2Int(1, 5);
        }
        timer = 0;
    }

    void setFpsTimeGraph()
    {

        GraphLines[0].points.Add(new Vector2(timer / 4f, 1f / Time.unscaledDeltaTime / 10f));
    }
    void setCpuUsageGraph()
    {
        GraphLines[1].points.Add(new Vector2(timer, UiPerformance.CpuUsage / 10f));
    }
    private void UpdateGridSize()
    {
        foreach (UiLineRenderer line in GraphLines)
        {
            while (line.points.Last().x > line.grid.gridSize.x)
            {
                line.grid.gridSize.x++;

            }
            while (line.points.Last().y > line.grid.gridSize.y - line.grid.originY - 1)
            {
                line.grid.gridSize.y++;
            }
            while (line.points.Last().y <= -line.grid.originY)
            {
                Debug.Log("origin : " + line.grid.originY);
                line.grid.originY = line.grid.gridSize.y / 2;
                line.grid.gridSize.y++;
            }
            line.SetAllDirty();
            line.grid.SetAllDirty();
        }
    }

    void selectGraph()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {

            if (index < Graphs.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            Graphs.ForEach(graph => graph.SetActive(false));
            Graphs[index].SetActive(true);

        }
    }
    public void SaveData()
    {
        csv.Write();
    }
    public void pause()
    {
        Time.timeScale = 0;
    }
    public void resume()
    {
        Time.timeScale = 1;
    }

}
