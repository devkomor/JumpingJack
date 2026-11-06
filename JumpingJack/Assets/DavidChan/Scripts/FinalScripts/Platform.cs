using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] protected float _bounceForce = 12f; // Usar _camelCase
    public virtual void ApplyBounce(PlayerMovementPhone player)
    {
        player.Jump(_bounceForce);
    }
    public virtual void RecycleToPool()
    {
        PlatformPool.Instance.RecyclePlatform(this);
    } 
    public virtual void ResetPlatform()
    {

    }
}
