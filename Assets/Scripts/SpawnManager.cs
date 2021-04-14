using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    [Range(0f,1f)]
    [SerializeField] private float _normalCoronaSpawnChance = 0f;
    [Range(0f,1f)]
    [SerializeField] private float _chanceModifier = 0.01f;
    [SerializeField] private List<GameObject> _virusPrefabs;
    [SerializeField] private GameObject _UVLightPrefab;
    
    [SerializeField] 
    private float _delay = 2f;

    [SerializeField]
    private float _powerUPSpawnRate = 30f;
    private bool _spawningON = true;
    void Start()
    {
        StartCoroutine(SpawnSystem());
        StartCoroutine(SpawnPowerup());

    }

    public void onPlayerDeath()
    {
        _spawningON = false;
    }
    // spawn a new virus every 2 seconds
    private int SelectVirusIndex()
    {
        int virusIndex = 0;
        if (_normalCoronaSpawnChance < Random.Range(0f, 1f))
        {
            _normalCoronaSpawnChance += _chanceModifier;    
            virusIndex = 0;
        }
        else
        {
            virusIndex = Random.Range(1, _virusPrefabs.Count);
        }
        return virusIndex;
    }

    
    IEnumerator SpawnSystem()
    {
        while (_spawningON)
        {
            
            Instantiate(_virusPrefabs[SelectVirusIndex()], new Vector3(Random.Range(-8f,8f), 7f, 0f), Quaternion.identity,this.transform);        
            yield return new WaitForSeconds(_delay);
        }
        Destroy(this.gameObject);
    }

    IEnumerator SpawnPowerup()
    {
        Debug.Log("powerups spawn enabled");
        while (true)
        {
            Instantiate(_UVLightPrefab,new Vector3(Random.Range(-8f, 8f), 7f, 0f), Quaternion.identity, this.transform);
            Debug.Log("spawning");
            yield return new WaitForSeconds(_powerUPSpawnRate);
        }
       
    }
}
