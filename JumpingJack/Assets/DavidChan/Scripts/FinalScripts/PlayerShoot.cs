// Sugerencia general de Clean Code: usar if de una sola línea con llaves cuando sea legible.

using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShoot : MonoBehaviour
{
    [Tooltip("Aqui va el prefab de la bala que se va a usar para el personaje")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _bulletVelocity = 10f;
    [SerializeField] private float _fireCooldown = 1.5f;

    private float _nextFireTime = 0f;
    private PlayerMovementPhone _playerMovement;
    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovementPhone>();
    }
    private void Update()
    {

        if (_playerMovement != null && _playerMovement.IsKnockedOut)
        {
            return;
        }

        if (Time.time >= _nextFireTime)
        {
            // Disparo con teclado
            bool spacePressed = Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;

            // Disparo táctil o clic en pantalla
            bool screenTapped = Pointer.current != null && Pointer.current.press.wasPressedThisFrame;

            if (spacePressed || screenTapped)
            {
                Shoot();
                _nextFireTime = Time.time + _fireCooldown;
            }
        }
    }

    private void Shoot()
    {
        GameObject tempBullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
        tempBullet.GetComponent<Rigidbody2D>().linearVelocity = _shootPoint.up * _bulletVelocity;
        Destroy(tempBullet, 1.5f);
    }
}
