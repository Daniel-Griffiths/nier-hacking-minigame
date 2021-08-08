using Kino;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;
    public AudioClip hitSound;    
    public AudioClip shootSound;    
    public float moveSpeed = 5f;
    public GameObject[] leftShipParts;
    public GameObject[] rightShipParts;

    private float _shootTimer;
    private float _bulletSpeed = 30f;
    private float _shootDelay = .12f;

    private int _health = 3;    
    private Vector3 _moveDirection;
    private Vector3 _mousePosition;
    private AudioSource _audioSource;
    private bool _isInvincible = false;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        Key.MouseDown(0, () => ShootRapid());
        Key.Up(KeyCode.E, () => _shootTimer = 0f);

        _mousePosition = Input.mousePosition;

        float x = Input.GetAxis("Horizontal");
        float y = 0f;
        float z =  Input.GetAxis("Vertical");

        _moveDirection = Move(x, y, z);
    }

    void FixedUpdate()
    {
        // Move the player
        transform.position += _moveDirection * moveSpeed;

        // Face mouse pointer
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        _mousePosition.x = _mousePosition.x - objectPos.x;
        _mousePosition.y = _mousePosition.y - objectPos.y;
 
        float angle = Mathf.Atan2(_mousePosition.y, _mousePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, -angle + 90f, 0));

        // Using the controller
        // Vector3 lookDirection = new Vector3(Input.GetAxisRaw ("RightHorizontal"), 0, Input.GetAxisRaw ("RightVertical"));
        // transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    Vector3 Move(float x, float y, float z) 
    {
        Vector3 upPosition = transform.position;
        upPosition.z += 1f;

        Vector3 downPosition = transform.position;
        downPosition.z -= 1f;

        Vector3 leftPosition = transform.position;
        leftPosition.x -= 1f;

        Vector3 rightPosition = transform.position;
        rightPosition.x += 1f;

        if(!Physics.Raycast(upPosition, Vector3.down)){
            z = MathHelper.clampNegative(z);
        }  
        
        if(!Physics.Raycast(downPosition, Vector3.down)) {
            z = MathHelper.clampPositive(z);
        } 
        
        if(!Physics.Raycast(leftPosition, Vector3.down)) {
           x = MathHelper.clampPositive(x);
        } 
        
        if(!Physics.Raycast(rightPosition, Vector3.down)) {
           x = MathHelper.clampNegative(x);            
        } 

        return new Vector3(x, y, z);
    }

    public void TakeDamage()
    {
        StartCoroutine(_TakeDamage());
    }

    public IEnumerator _TakeDamage()
    {
        if(_isInvincible) {
            yield break;
        } 

        _isInvincible = true;
        _audioSource.PlayOneShot(hitSound);

        _health--;

        switch(_health) {
            case 2: 
                foreach (GameObject leftShipPart in leftShipParts){
                   Destroy(leftShipPart);
                }
                break; 
            case 1: 
                foreach (GameObject rightShipPart in rightShipParts){
                   Destroy(rightShipPart);
                }
                break; 
            case 0: 
                FindObjectOfType<GameManager>().GameOver();
                Destroy(gameObject);
                break;                                 
        }

        mainCamera.GetComponent<AnalogGlitch>().enabled = true;
        mainCamera.GetComponent<DigitalGlitch>().enabled = true;
        Invoke("ResetCamera", 0.5f);

        yield return  new WaitForSeconds(.75f);

        _isInvincible = false;
    }

    void ResetCamera()
    {
        mainCamera.GetComponent<AnalogGlitch>().enabled = false;
        mainCamera.GetComponent<DigitalGlitch>().enabled = false;
    }

    void Shoot()
    {
        GameObject bullet = Resources.Load("Prefabs/PlayerBullet") as GameObject;

        _audioSource.PlayOneShot(shootSound, 0.75f);

        Instantiate(bullet, transform.position, transform.rotation).GetComponent<Rigidbody>()
              .AddForce(transform.forward * _bulletSpeed, ForceMode.Impulse);
    }

    void ShootRapid() 
    {
        _shootTimer -= Time.deltaTime;
        
        if( _shootTimer < 0 ) {
            Shoot();
            _shootTimer += _shootDelay;
        }
    }
}
