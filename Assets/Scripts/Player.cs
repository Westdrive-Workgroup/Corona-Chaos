using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5f;

    [Header("External Components")]
    [SerializeField] 
    private GameObject _vaccinePrefab;
    
    [SerializeField] 
    private GameObject _uvLightPrefab;

    [SerializeField] 
    private GameObject _barrierPrefab;
    
    [SerializeField] 
    private GameObject _ShieldPrefab;
    
    [SerializeField] 
    private SpawnManager _spawnManager;

    //we keep this for Max
    [SerializeField] 
    private UIManager _uiManager;
    
    [Header("Vaccination parameter")]
    [SerializeField] 
    private float _vaccinationRate = 0.4f;
    [SerializeField]
    private Transform _vaccineParent;
    [SerializeField] 
    private float _powerupTimeout = 5f;
    
    [Header("Player Settings")]
    [SerializeField]
    private int _lives = 3;

    private float _canVaccinate = -1f;
    private float _colorChannel = 1f;
    private bool _isPowerUpOn = false;
    private PowerUpType _powerUpType;
    
    private bool _bdEasterEgg = false; 
   

    void OnEnable()
    {
        if (_vaccinePrefab == null)
            Debug.LogError("Vaccince Prefab is missing!");
        if(_uvLightPrefab == null)
            Debug.LogError("UV light Prefab is missing!");
        if (_barrierPrefab == null)
            Debug.LogError("Barrier Prefab is missing!");
        if(_ShieldPrefab == null)
            Debug.LogError("Shield Prefab is missing!");
        if (_spawnManager == null)
            Debug.LogError("Spawn Manager is missing!");
        if(_uiManager == null)
            Debug.LogError("UI manager is missing!");
        if(_vaccineParent == null)
            Debug.LogError("_vaccine parenting transform is missing!");
        
    }
    void Start()
    {
        _uiManager.UpdateHealth(_lives);
        _isPowerUpOn = false;
        transform.position = new Vector3(0f, 0f, 0f);
    }
   
    void Update()
    {

        
        PlayerMovement();
        Vaccinate();
        DefensiveOn();
    }
    

    // Damaging the plyar
    public void Damage()
    {
        if(_ShieldPrefab.activeSelf)
            _ShieldPrefab.SetActive(false);
        _lives -= 1;  
        _uiManager.UpdateHealth(_lives);
        //if health is 0 destroy the player
        if (_lives == 0)
        {
            _spawnManager.onPlayerDeath();
            _uiManager.ShowGameOver();
            Destroy(this.gameObject);
        }
    }

    public void RelayScore(int score)
    {
        _uiManager.AddScore(score);
    }
    void DefensiveOn()
    {
        if (_isPowerUpOn)
        {
            switch (_powerUpType)
            {
                case PowerUpType.Barrier:
                    Instantiate(_barrierPrefab,  new Vector3(0f, -10f, 0), Quaternion.identity,
                        _vaccineParent);
                    _isPowerUpOn = false;
                    break;
                case PowerUpType.Shield:
                    if(!_ShieldPrefab.activeSelf)
                        _ShieldPrefab.SetActive(true);
                    _lives++;
                    _uiManager.UpdateHealth(_lives);
                    _isPowerUpOn = false;
                    break;
                default:
                    break;
            }
        }
    }
    // instantiaing new vaccines
    void Vaccinate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canVaccinate)
        {
            // add the rate to the current time to set the future possible vaccination time
            _canVaccinate = Time.time + _vaccinationRate;
            // Instantiating the new vaccine drop
            
            if (!_isPowerUpOn)
                Instantiate(_vaccinePrefab, transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity,
                    _vaccineParent);
            else
            {
                switch (_powerUpType)
                {
                    case PowerUpType.UVLight:
                        Instantiate(_uvLightPrefab, transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity,
                            _vaccineParent);
                        break;
                    default:
                        break;
                }
                
            }
        }
        
    }
    
    
    //player movement function
    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        transform.GetChild(0).Rotate(new Vector3(0f,horizontalInput * _speed * 2f * Time.deltaTime,0f),Space.World);
        Vector3 playerTranslate = new Vector3(
            1f * horizontalInput * _speed * Time.deltaTime,
            1f * verticalInput * _speed * Time.deltaTime,
            0f);
        transform.Translate(playerTranslate);

        
        if (transform.position.y >0f)   
        {
            // keep player y position at 0
            transform.position = new Vector3(transform.position.x,
                0f,
                0f);
        }
        else if (transform.position.y < -4.5f)
        {
            transform.position = new Vector3(transform.position.x,
                -4.5f,
                0f);
        }
        
        if (transform.position.x >11.2f)   
        {
            transform.position = new Vector3(-11.2f,
                transform.position.y,
                0f);
        }
        else if (transform.position.x < -11.2f)
        {
            transform.position = new Vector3(11.2f,
                transform.position.y,
                0f);
        }
    }

    public void ActivatePowerUp(PowerUpType type)
    {
        _powerUpType = type;
        _isPowerUpOn = true;
        StartCoroutine(DeactivatePowerUp());
    }

    IEnumerator DeactivatePowerUp()
    {
        yield return new WaitForSeconds(_powerupTimeout);
        _isPowerUpOn = false;
    }
}
