using System.Collections;
using System.Collections.Generic;
using System.Resources;
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

    [Header("Player bounds")] [SerializeField]
    [Tooltip("this is a tooltip")]
    private Rect _playerBounds;
    
    [Header("Vaccination parameter")]
    [SerializeField] 
    [Range(0,2f)]
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
        
        if(_vaccineParent == null)
            Debug.LogError("_vaccine parenting transform is missing!");
        
        
    }
    void Start()
    {
        GameManager.Instance.UpdateHealth(_lives);
        GameManager.Instance.onDamageTaken += Damage;
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
        GameManager.Instance.UpdateHealth(_lives);
        //if health is 0 destroy the player
        if (_lives == 0)
        {
            
            GameManager.Instance.GameOver();
            Destroy(this.gameObject);
        }
    }

    public void RelayScore(int score)
    {
        GameManager.Instance.AddScore(score);
        
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
                    GameManager.Instance.UpdateHealth(_lives);
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
        
        transform.Rotate(new Vector3(0f,horizontalInput * _speed * 2f * Time.deltaTime,0f),Space.World);
        Vector3 playerTranslate = new Vector3(
            1f * horizontalInput * _speed * Time.deltaTime,
            1f * verticalInput * _speed * Time.deltaTime,
            0f); 
        transform.Translate(playerTranslate);
        
        float playerX = Mathf.Clamp(transform.position.x, -1 * (_playerBounds.width/2), (_playerBounds.width/2));
        float playerY = Mathf.Clamp(transform.position.y, -1 * (_playerBounds.height/2), (_playerBounds.height/2));
        
        transform.position = new Vector3(playerX,playerY,0f);
        
        
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
