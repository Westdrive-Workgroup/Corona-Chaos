using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Virus",menuName = "GameAssets/Virus")]
public class Virus : ScriptableObject
{
    public string name = "";
    public Mesh virusMesh;
    public Material virusMaterial;
    
    public float speed = 5f;
    public float horizontalSpeed = 10f;
    public Rect boundaries;
    [Range(50f, 0)] public float initalDepth = 20f;
    [Range(0f, -10f)] public float endDepth = -2f;
    [Range(0f,1f)] public float blend = 0f;
    public GameObject projectilePrefab;
    public float incidentRate = 2f;
    public bool canInfect;
    
    public void Print()
    {
        Debug.LogWarning("I am " + name);
    }

}
