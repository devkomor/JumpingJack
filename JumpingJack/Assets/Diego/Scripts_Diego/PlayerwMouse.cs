using UnityEngine;

public class PlayerwMouse : MonoBehaviour
{
    [SerializeField] private float MovementSpeed = 5f;

    private float _movementValue = 0f;
    private Rigidbody2D _playerRigidbody;

    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float InputArrows = Input.GetAxis("Horizontal") * MovementSpeed;
    }

    void FixedUpdate()
    {
        Vector2 currentVelocity = _playerRigidbody.linearVelocity;
        currentVelocity.x = _movementValue;
        _playerRigidbody.linearVelocity = currentVelocity;
    }
}
