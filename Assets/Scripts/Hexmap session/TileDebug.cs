using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileDebug : MonoBehaviour
{
    public bool showDebugCoord = false;
    // Start is called before the first frame update
    
    void Start()
    {
        if (showDebugCoord == false) 
            GetComponentInChildren<TextMesh>().gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
