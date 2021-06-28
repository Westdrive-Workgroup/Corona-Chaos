using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[RequireComponent(typeof(SomeData))]
public class DataSerialization : MonoBehaviour
{
    // Start is called before the first frame update
    private int _id = 500;
    private SomeData _data;
    void Start()
    {
        _data = GetComponent<SomeData>();
        if(PlayerPrefs.HasKey("id"))
            _id = PlayerPrefs.GetInt("id");
        Debug.LogFormat("The id is {0}",_id);
        
        _data = JsonUtility.FromJson<SomeData>(File.ReadAllText("data.json"));
        //_data.print();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetInt("id",900);
            //string json = JsonUtility.ToJson(_data);
            //File.WriteAllText("data.json", json);
            //Debug.Log(json);
            
        }
    }
}
