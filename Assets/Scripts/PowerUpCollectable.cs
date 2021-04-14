using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    UVLight = 0,Barrier = 1,
    Shield = 2
}
public class PowerUpCollectable : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 2f;

    [SerializeField] private PowerUpType _type = PowerUpType.Barrier;
    [SerializeField] private bool _spawnRandomType = true;

    void Start()
    {
        if (_spawnRandomType == true)
        {
            _type = (PowerUpType) Random.Range(0, System.Enum.GetValues(typeof(PowerUpType)).Length);
        }
    }

    public PowerUpType GetType()
    {
        return _type;
    }
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().ActivatePowerUp(_type);
            Destroy(this.gameObject);
        }
    }
    
}
