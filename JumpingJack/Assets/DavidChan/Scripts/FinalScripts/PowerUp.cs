using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] protected float duration = 5f;

    public virtual void ApplyEffect(PlayerMovementPhone player)
    {
        // Logica del objeto
    }

    protected void DestroyAfterCollect()
    {
        Destroy(gameObject);
    }
}
