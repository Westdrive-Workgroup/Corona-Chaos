using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SomeData : MonoBehaviour
{
    [SerializeField]
    private string name;
    [SerializeField]
    private List<float> timesptamp;
    void Start()
    {
        //name = "test data 1";
    }

    // Update is called once per frame
    void Update()
    {
        //timesptamp.Add(Time.realtimeSinceStartup);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }

    public void print()
    {
        Debug.Log(name);
        Debug.Log(timesptamp);
    }
}
