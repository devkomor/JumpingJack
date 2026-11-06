using UnityEngine;

public class PlatfomMovement : Platform 
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private bool _isMovingHorizontal = false;
    [SerializeField] private bool _isMovingVertical = true;

    [Header("Limites de pantalla")]
    [SerializeField] private float _lowerLimit = -6f;

    private PlatformPool _pool;

    public void SetPool(PlatformPool pool)
    {
        _pool = pool;
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {
        CheckBounds();
    }

    // METODOS

    private void Move()
    {
        if (_isMovingVertical)
        {
            transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);
        }
        if (_isMovingHorizontal)
        {
            transform.Translate(Vector3.Lerp(Vector3.left, Vector3.right, 0.5f) * _moveSpeed * Time.deltaTime);
        }
    }

    private void CheckBounds()
    {
        if (transform.position.y < _lowerLimit)
        {
            RecycleToPool();
        }
    }
}
