using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classes : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
    
        Tracker tracker = new EyeTracker();
        tracker.StartRecording();
        Tracker.a = 100;
        Tracker.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



