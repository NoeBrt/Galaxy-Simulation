using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class WriteCSV
{
    public List<CsvModel> data { get; set; }
    string filename = "";
    // Update is called once per frame
    public WriteCSV()
    {
        data = new List<CsvModel>();
    }

    public void Write()
    {
        getFilePath();
        if (data.Count > 0 && filename != "")
        {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("time ;  FPS ; CPU usage(%)");
            tw.Close();
            tw = new StreamWriter(filename, true);
            for (int i = 0; i < data.Count; i++)
            {
                tw.WriteLine(data[i].time + " ; " + data[i].fps + " ; " + data[i].CpuUsage);
            }
            tw.Close();
        }

    }
    void getFilePath()
    {
        filename = EditorUtility.SaveFilePanel("CurrentBallMovement", "", "dataBallInBound", ".csv");

        if (File.Exists(filename))
        {
            File.Delete(filename);
        }
    }
}
