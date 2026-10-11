// Sugerencia general de Clean Code: usar if de una sola línea con llaves cuando sea legible.

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
    [SerializeField] private float _smoothing = 10f; // Aclarar el significado o las unidades con Tooltip
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

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) Shoot(); // Agregar llaves al if
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame) TrySpawnSavePlatform(); // Agregar llaves al if

        WrapAroundScreen();

        CheckVerticalBounds();
    }

    private float ReadKeyboardInput()
    {
        if (Keyboard.current != null) // Revisar si se puede usar retorno temprano
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

    void FixedUpdate() // Respetar el orden de los miembros
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

    public void Jump(float customForce = 0f) // Respetar el orden de acceso y static
    {
        float forceToApply = customForce > 0f ? customForce : _jumpVelocity;
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, forceToApply);
    }

    private void Shoot() // Revisar si este código es necesario
    {
        //TODO Instancia de la clase Bullet segun el fireRate
        Debug.Log("Mueran malditos comunistas!!!!");
    }

    private void TrySpawnSavePlatform() // El método debe hacer lo que indica su nombre
    {
        if (_savePlatformsLeft > 0)
        {
            //TODO Spawnear plataforma 
            _savePlatformsLeft--;

            Debug.Log("Plataforma creada");
        }
    }

    public void ApplyPowerUp(PowerUp powerUp) // Respetar el orden de acceso y static
    {
        powerUp.ApplyEffect(this);
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter2D(Collision2D collision) // Respetar el orden de los miembros
    {
        if (_rigidbody.linearVelocity.y <= 0.01f) // Revisar si se puede usar retorno temprano
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
        var cam = Camera.main; // Usar el tipo explícito en lugar de var; Sin abreviaciones
        float camHalfWidth = cam.orthographicSize * cam.aspect;
        float x = transform.position.x; // Usar un nombre descriptivo

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
    private void CheckVerticalBounds() // Revisar si se puede usar retorno temprano
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
