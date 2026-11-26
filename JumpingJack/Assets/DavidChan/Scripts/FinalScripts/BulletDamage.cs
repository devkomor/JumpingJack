using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyBehaviour enemy = other.gameObject.GetComponent<EnemyBehaviour>();
            if (enemy != null)
            {
                enemy.RecycleToEnemyPool();
            }
            Destroy(gameObject);
        }
    }
}
