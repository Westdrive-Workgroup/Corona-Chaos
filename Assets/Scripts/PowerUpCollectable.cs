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
    [Range(0f, -10f)] [SerializeField] private float _endDepth = -2f;
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
        transform.Translate(-1f * Vector3.forward * _speed * Time.deltaTime);
        if (transform.position.z < _endDepth)
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
