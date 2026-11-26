using UnityEngine;


public class Platform : MonoBehaviour
{
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    [SerializeField] protected float _bounceForce = 12f; 
    public virtual void ApplyBounce(PlayerMovementPhone player)
    {
        _animator.SetTrigger("Bounce");
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