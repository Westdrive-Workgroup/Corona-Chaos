using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerPhysics : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody _body;
    [SerializeField] private float _force = 5f;
    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _body.AddForce(Vector3.up * _force, ForceMode.Impulse);
            
        }
        
    }
}
