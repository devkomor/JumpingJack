using System;
using UnityEngine;

public class EnemyMovement : EnemyBehaviour
{
    [SerializeField] private float _movementSpeed = 3f;
    [SerializeField] private bool _isMovingHorizontal = false;
    [SerializeField] private bool _isMovingVertical = true;
    [Header("Limites de pantalla")]
    [SerializeField] private float _lowerLimit = -6f;

    private EnemyPool _enemyPool;
    
    private void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
    }
    private void Move()
    {
        if (_isMovingVertical)
        {
            transform.Translate(Vector3.down * _movementSpeed * Time.deltaTime);
        }
        if (_isMovingHorizontal)
        {
            transform.Translate(Vector3.Lerp(Vector3.left, Vector3.right, 0.5f) * _movementSpeed * Time.deltaTime);
        }
    }

    private void CheckBounds()
    {
        if (transform.position.y < _lowerLimit)
        {
            EnemyPool.Instance.RecycleEnemy(this);
        }
    }
}
