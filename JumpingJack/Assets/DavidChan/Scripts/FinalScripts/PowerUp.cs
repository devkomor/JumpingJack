using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] protected float duration = 5f; // Usar _camelCase; 

    public virtual void ApplyEffect(PlayerMovementPhone player)
    {
        // Logica del objeto 
    }

    protected void DestroyAfterCollect()
    {
        Destroy(gameObject);
    }
}
