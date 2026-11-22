// Sugerencia general de Clean Code: usar if de una sola línea con llaves cuando sea legible.

using System;
using UnityEngine;

public class EnemyMovement : EnemyBehaviour
{
    [SerializeField] private float _movementSpeed = 3f;
    [SerializeField] private bool _scrollWithLevel = true;
    [SerializeField] private bool _isMovingHorizontal = true;
    [SerializeField] private float _horizontalSpeed = 2f;
    [SerializeField] private float _horizontalRange = 2.5f;
    [SerializeField] private float _stompBounceForce = 14f;

    [Header("Limites de pantalla")]
    [SerializeField] private float _lowerLimit = -6f;

    private float _startX;
    private float _timeOffset;

    private void OnEnable()
    {
        _startX = transform.position.x;
        _timeOffset = UnityEngine.Random.Range(0f, 10f);
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        CheckBounds();
    }
    private void Move()
    {
        float verticalMovement = _scrollWithLevel ? -_movementSpeed * Time.fixedDeltaTime : 0f;
        float newY = transform.position.y + verticalMovement;
        float newX = transform.position.x;
        if (_isMovingHorizontal)
        {
            newX = _startX + Mathf.Sin((Time.time + _timeOffset) * _horizontalSpeed) * _horizontalRange;
        }
        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    private void CheckBounds()
    {
        if (transform.position.y < _lowerLimit)
        {
            EnemyPool.Instance.RecycleEnemy(this);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) { return; }

        PlayerMovementPhone player = collision.gameObject.GetComponent<PlayerMovementPhone>();
        if (player == null) { return; }
        
        PlayerShield shield = collision.gameObject.GetComponent<PlayerShield>();
        if (shield != null && shield.IsActive())
        {
            player.Jump(_stompBounceForce);
            RecycleToEnemyPool();
            return;
        }

        if (player.IsKnockedOut)
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
            return;
        }

        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
        //Verificamos que el jugador este cayendo y si sus pies estan por encima del centro del enemigo
        bool isFalling = playerRb != null && playerRb.linearVelocity.y <= 0.1f;
        bool isAboveEnemy = collision.transform.position.y > transform.position.y + 0.2f;

        if (isFalling && isAboveEnemy)
        {
            //El jugador rebota y el enemigo se recicla
            player.Jump(_stompBounceForce);
            RecycleToEnemyPool();
        }
        else
        {
            //Choque frontal/inferior: Noquea al jugador
            player.TakeEnemyHit();
            //Si el golpe lo dejó noqueado, ignoramos la colisión física con este enemigo
            if (player.IsKnockedOut)
            {
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
            }
        }
    }
}
