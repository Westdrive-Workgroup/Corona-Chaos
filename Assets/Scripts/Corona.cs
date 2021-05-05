using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Corona : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _horizontalSpeed = 10f;
    [SerializeField] private Rect _boundaries;
    [Range(50f, 0)] [SerializeField] private float _initalDepth = 20f;
    [Range(0f, -10f)] [SerializeField] private float _endDepth = -2f;
 
    void Update()
    {
        
        transform.Translate(-1f * Vector3.forward * _speed * Time.deltaTime);
        if (transform.position.z < _endDepth)
        {
            transform.position = new Vector3(Random.Range(-1 * _boundaries.width/2, _boundaries.width/2), Random.Range(-1 * _boundaries.height/2, _boundaries.height/2), _initalDepth);
            
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
            if (!other.name.Contains("UVLight") && !other.name.Contains("ScreenBarrier") )
            {
                Destroy(other.gameObject);
            }

            if (name.Contains("B117"))
            {
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(3);
            }
            else
            {
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
            }

            Destroy(this.gameObject);
        }
    }
}