using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Vaccine : MonoBehaviour
{
    [SerializeField] 
    private float _vacinneSpeed = 8f;

    [SerializeField]
    private float _rotationSpeed = 30f;

    
    // making sure vaccenes stay in the camera frustrum 
    void Update()
    {
        transform.Rotate(new Vector3(0f, _rotationSpeed * Time.deltaTime, 0f), Space.Self);
        if (CompareTag("Vaccine"))
        {
            transform.Translate((Vector3.up) * _vacinneSpeed * Time.deltaTime);
            
            if (transform.position.y > 7f)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            transform.Translate((Vector3.down) * _vacinneSpeed * Time.deltaTime);
            if (transform.position.y < -7f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    
  
    void OnTriggerEnter(Collider other)
    {
        //if the object we collide with is the player 
        if (other.CompareTag("Player"))
        {
            //damage player or destroy it and the virus    
            other.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }
        //but if the other one is vaccine
        else if (other.CompareTag("Vaccine"))
        {
            //destroy vaccine and the virus    
            if (!other.name.Contains("UVLight") )
            {
                Destroy(other.gameObject);
            }
            if (!other.name.Contains("ScreenBarrier"))
            {
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(5);
                Destroy(this.gameObject);
            }
        }
    }
}
