using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Corona : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _horizontalSpeed = 10f;
    [SerializeField]
    private float _rotationSpeed = 750f;

 
    void Update()
    {
        
        // transform.GetChild(0).Rotate(new Vector3(Random.Range(-_rotationSpeed, _rotationSpeed) * Time.deltaTime,
        //         Random.Range(-_rotationSpeed, _rotationSpeed) * Time.deltaTime,
        //         Random.Range(-_rotationSpeed, _rotationSpeed) * Time.deltaTime),
        //     Space.Self);
        // transform.rotation = Quaternion.Slerp(transform.rotation,target, _rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(name.Contains("B117"))
            transform.Translate(Vector3.right * Random.Range(-1f,1f) * _horizontalSpeed * Time.deltaTime);
        if (transform.position.y < -5.5f)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            
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