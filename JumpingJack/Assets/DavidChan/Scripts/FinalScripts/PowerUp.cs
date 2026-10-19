using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] protected float _duration = 5f; 

    //funcion pendiente hasata tener power up
    public virtual void ApplyEffect(PlayerMovementPhone player)
    {

    }

    protected void DestroyAfterCollect()
    {
        Destroy(gameObject);
    }
}
