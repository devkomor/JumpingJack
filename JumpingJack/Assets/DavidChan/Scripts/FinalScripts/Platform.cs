using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] protected float bounceForce = 12f;
    public virtual void ApplyBounce(PlayerMovementPhone player)
    {
        player.Jump(bounceForce);
    }
    public virtual void RecycleToPool()
    {
        PlatformPool.Instance.RecyclePlatform(this);
    }
}
