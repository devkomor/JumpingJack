using UnityEngine;

public class PlatfomMovement : Platform
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private bool _isMovingHorizontal = false;
    [SerializeField] private bool _isMovingVertical = true;

    [Header("Limites de pantalla")]
    [SerializeField] private float _lowerLimit = -6f;

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
            //Agregar logica de movimiento Izquierda y Derecha
        }
    }

    private void CheckBounds()
    {
        if(transform.position.y < _lowerLimit)
        {
            RecycleToPool();
        }
    }
}
