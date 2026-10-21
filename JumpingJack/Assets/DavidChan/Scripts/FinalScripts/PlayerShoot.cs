using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Tooltip("Aqui va el prefab de la bala que se va a usar para el personaje")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _bulletVelocity = 10f;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    void Update()
    {

    }

    private void Shoot()
    {
        GameObject tempBullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
        tempBullet.GetComponent<Rigidbody2D>().linearVelocity = _shootPoint.up * _bulletVelocity;
        Destroy(tempBullet, 1.5f);
    }

     // Logica del codigo original para disparar
     // TODO implementar este codigo a la logica de "PlayerShoot"
     //    if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) 
     //    {
     //    Shoot();
     //    }
}
