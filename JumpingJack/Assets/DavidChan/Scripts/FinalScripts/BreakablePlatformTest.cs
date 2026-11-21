using UnityEngine;

public class BreakablePlatformTest : PlatfomMovement
{
    // Cambiamos a OnCollisionEnter2D para tener solidez física y evitar que el jugador se hunda
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovementPhone player = collision.gameObject.GetComponent<PlayerMovementPhone>();

        if (player != null)
        {
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            // Comprobamos que el jugador está cayendo
            if (playerRigidbody != null && playerRigidbody.linearVelocity.y <= 0.1f)
            {
                base.ApplyBounce(player);

                // Simplemente reciclamos. La clase padre ya sabe cómo gestionar el Pool.
                // NUNCA usamos Destroy() en objetos que pertenecen a un Pool.
                RecycleToPool();
            }
        }
    }
}
