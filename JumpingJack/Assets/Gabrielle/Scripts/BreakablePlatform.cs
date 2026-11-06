using UnityEngine;

public class BreakablePlatform : PlatfomMovement
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //jugador
        PlayerMovementPhone player = other.GetComponent<PlayerMovementPhone>();

        if (player != null)
        {
            // coimprobqmos que esta cayendo
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();

            if (playerRigidbody != null && playerRigidbody.linearVelocity.y <= 0.1f)
            {
                base.ApplyBounce(player);
                if (_pool != null)
                {
                    RecycleToPool();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}