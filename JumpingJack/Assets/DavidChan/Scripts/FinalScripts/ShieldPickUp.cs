using UnityEngine;

public class ShieldPickUp : MonoBehaviour
{
    [SerializeField] private float _shieldDuration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) { return; }

        PlayerShield shield = other.GetComponent<PlayerShield>();
        if (shield != null)
        {
            shield.Activate(_shieldDuration);
            Destroy(gameObject);
        }
    }
}
