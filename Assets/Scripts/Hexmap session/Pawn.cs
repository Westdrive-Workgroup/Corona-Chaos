using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 destination;
    private Vector3 direction;
    void Start()
    {
        destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        direction = destination - transform.position;
        direction = Vector3.ClampMagnitude(direction, Vector3.Distance(transform.position, destination));
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void setDest(Vector3 dest)
    {
        destination = new Vector3(dest.x,transform.position.y,dest.z);
    }
}
