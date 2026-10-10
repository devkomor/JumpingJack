using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementPhone : MonoBehaviour
{
    [SerializeField] private int _savePlatformsLeft = 5;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private float _jumpVelocity = 12f;
    [SerializeField] private float _tiltSpeed = 12f;
    [SerializeField] private float _smoothing = 10f;
    [SerializeField] private float _topLimit = 5f;
    [SerializeField] private float _bottomLimit = -5f;
    private Rigidbody2D _rigidbody;
    private float _horizontal;
    private float _halfWidth;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _halfWidth = GetComponent<Collider2D>().bounds.extents.x;
    }

    void OnEnable()
    {
        #if UNITY_ANDROID || UNITY_IOS
        if (Accelerometer.current != null)
        {
            InputSystem.EnableDevice(Accelerometer.current);
        }
        #endif
    }

    void Update()
    {
        //https://docs.unity3d.com/520/Documentation/Manual/PlatformDependentCompilation.html
        float target = 0f;
#if UNITY_ANDROID || UNITY_IOS
        target = ReadAccelerometerInput();
#endif
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        target = ReadKeyboardInput();
#endif
        Move(target);

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) Shoot();
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame) TrySpawnSavePlatform();

        WrapAroundScreen();

        CheckVerticalBounds();
    }

    private float ReadKeyboardInput()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current.leftArrowKey.isPressed)
            {
                return -1f;
            }
            else if (Keyboard.current.rightArrowKey.isPressed)
            {
                return 1f;
            }
        }

        return 0;
    }

    void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(_horizontal * _tiltSpeed, _rigidbody.linearVelocity.y);
    }

    // METODOS

    private float ReadAccelerometerInput()
    {
        
        if (Accelerometer.current != null)
        {
            return Accelerometer.current.acceleration.ReadValue().x;
        }
        else
        {
            return 0;
        }
    }

    private void Move(float input)
    {
        _horizontal = Mathf.Lerp(_horizontal, input, Time.deltaTime * _smoothing);
    }

    public void Jump(float customForce = 0f)
    {
        float forceToApply = customForce > 0f ? customForce : _jumpVelocity;
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, forceToApply);
    }

    private void Shoot()
    {
        //TODO Instancia de la clase Bullet segun el fireRate
        Debug.Log("Mueran malditos comunistas!!!!");
    }

    private void TrySpawnSavePlatform()
    {
        if (_savePlatformsLeft > 0)
        {
            //TODO Spawnear plataforma 
            _savePlatformsLeft--;

            Debug.Log("Plataforma creada");
        }
    }

    public void ApplyPowerUp(PowerUp powerUp)
    {
        powerUp.ApplyEffect(this);
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_rigidbody.linearVelocity.y <= 0.01f)
        {
            Platform platform = collision.gameObject.GetComponent<Platform>();
            if (platform != null)
            {
                platform.ApplyBounce(this);
            }
        }
    }

    private void WrapAroundScreen()
    {
        var cam = Camera.main; 
        float camHalfWidth = cam.orthographicSize * cam.aspect;
        float x = transform.position.x;

        if (x > camHalfWidth + _halfWidth)
        {
            x = -camHalfWidth - _halfWidth;
        }
        else if (x < -camHalfWidth - _halfWidth)
        {
            x = camHalfWidth + _halfWidth;
        }
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    private void CheckVerticalBounds()
    {
        if (transform.position.y > _topLimit)
        {
            transform.position = new Vector3(transform.position.x, _topLimit, transform.position.z);

            if (_rigidbody.linearVelocity.y > 0)
            {
                _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0f);
            }
        }
        
        if (transform.position.y < _bottomLimit)
        {
            Die();
        }
    }
}
