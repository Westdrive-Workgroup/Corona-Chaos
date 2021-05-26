using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(MeshRenderer)),RequireComponent(typeof(MeshFilter))]
public class Corona : MonoBehaviour
{
    
    [SerializeField] private Virus _virusData;
    private float _canInfect = -1f;
    private Animator _animator;
    [Range(0,1)]
    public float time = 1f;
    void Awake()
    {
        if(_virusData.virusMesh != null)
            GetComponent<MeshFilter>().mesh = _virusData.virusMesh;
        if(_virusData.virusMaterial != null)
            GetComponent<MeshRenderer>().material = _virusData.virusMaterial;
        if (name.Contains("B117"))
        {
            _virusData.blend = 1f;
        }
        else if (name.Contains("501V2"))
        {
            _virusData.blend = 0.5f;
            if(_virusData.projectilePrefab == null)
                Debug.Log("Evil Vaccine Prefab is missing!");
        }
        if (GetComponent<Animator>() != null)
        {
            _animator = GetComponent<Animator>();
            _animator.SetBool("ShouldAnimate", true);
            _animator.SetFloat("Blend", _virusData.blend);
        }
        
    }

    void Update()
    {
        Time.timeScale = time;
        transform.Translate(-1f * Vector3.forward * _virusData.speed * Time.deltaTime);
        if (transform.position.z < _virusData.endDepth)
        {
            transform.position =
                new Vector3(Random.Range(-1 * _virusData.boundaries.width / 2, _virusData.boundaries.width / 2),
                    Random.Range(-1 * _virusData.boundaries.height / 2, _virusData.boundaries.height / 2),
                    _virusData.initalDepth);
            
        }

        if (_virusData.canInfect)
        {
            Infect();
        }
    }
    public void Infect()
    {
        if (Time.time > _canInfect)
        {
            _canInfect = Time.time + _virusData.incidentRate;
            Instantiate(_virusData.projectilePrefab,transform.position + new Vector3(0f, 0f, -0.7f),Quaternion.identity);

        }

    }
    void OnTriggerEnter(Collider other)
    {
        //if the object we collide with is the player 
        if (other.CompareTag("Player"))
        {
            //damage player or destroy it and the virus    
            //other.GetComponent<Player>().Damage();
            GameManager.Instance.onDamageTaken?.Invoke();
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

                GameManager.Instance.AddScore(3);
            }
            else
            {
                GameManager.Instance.AddScore(1);
            }

            Destroy(this.gameObject);
        }
    }
}