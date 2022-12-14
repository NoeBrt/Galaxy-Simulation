using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

public class CsvModel
{
    public float time;
    public float fps;
    public float CpuUsage;



    public CsvModel(float time,float fps, float CpuUsage)
    {
        this.time = time;
        this.fps = fps;
        this.CpuUsage = CpuUsage;
    }
}