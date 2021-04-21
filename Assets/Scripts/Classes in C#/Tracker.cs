using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker
{
    public static int a = 1000;
    private int b;
    public int c;
    public void StartRecording()
    {
        Debug.Log("Tracker recording!");
    }

    public void StopRecording()
    {
        a = 50;
    }

    public static void Save()
    {
        //some code
    }
}