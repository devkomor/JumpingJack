using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] protected float _bounceForce = 12f; 
    public virtual void ApplyBounce(PlayerMovementPhone player)
    {
        player.Jump(_bounceForce);
    }
    public virtual void RecycleToPool()
    {
<<<<<<< Updated upstream
        PlatformPool.Instance.RecyclePlatform(this);
    } 
    public virtual void ResetPlatform()
    {

=======
        if (pool != null)
        {
            pool.RecyclePlatform(this);
        }
        
>>>>>>> Stashed changes
    }
}
