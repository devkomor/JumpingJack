using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovementPhone player = other.GetComponent<PlayerMovementPhone>();
        if (player == null) { return; }

        player.ApplyPowerUp(this);
        DestroyAfterCollect();
    }

    //Metodos
    public virtual void ApplyEffect(PlayerMovementPhone player)
    {

    }

    protected void DestroyAfterCollect()
    {
        Destroy(gameObject);
    }
}
