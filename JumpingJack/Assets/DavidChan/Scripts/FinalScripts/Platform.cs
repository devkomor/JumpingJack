using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] protected float _bounceForce = 12f; 

    public virtual void ApplyBounce(PlayerMovementPhone player)
    {
        player.Jump(_bounceForce);
    }
    public virtual void RecycleToPool(PlatformPool pool)
    {
        if (pool != null)
        {
            pool.RecyclePlatform(this);
        }
        ; // Revisar la inyección de dependencias
    }
    
}
