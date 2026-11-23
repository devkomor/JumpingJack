// Sugerencia general de Clean Code: usar if de una sola línea con llaves cuando sea legible.

using UnityEngine;

public class PlatfomMovement : Platform 
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private bool _isMovingHorizontal = false;
    [SerializeField] private bool _isMovingVertical = true;

    [Header("Limites de pantalla")]
    [SerializeField] private float _lowerLimit = -6f;

    protected PlatformPool _pool; // Respetar el orden de acceso y static

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
        float dynamicLimit = Camera.main.transform.position.y + _lowerLimit;

        if (transform.position.y < dynamicLimit)
        {
            RecycleToPool();
        }
    }
    public override void RecycleToPool() // Respetar el orden de acceso y static
    {
        // Verifica si inyectaste un pool específico en esta plataforma
        if (_pool != null)
        {
            _pool.RecyclePlatform(this);
        }
        else
        {
            // Si no hay pool local, usa el comportamiento por defecto de la clase base
            base.RecycleToPool();
        }
    }
}
