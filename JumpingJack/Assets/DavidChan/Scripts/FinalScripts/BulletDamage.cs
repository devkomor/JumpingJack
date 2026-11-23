using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        StaticEnemy enemy = other.gameObject.GetComponent<StaticEnemy>();

        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy.DestroyEnemy();
            Destroy(gameObject);
        }
    }
}
