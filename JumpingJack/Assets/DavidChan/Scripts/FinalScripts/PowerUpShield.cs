using UnityEngine;

public class PowerUpShield : PowerUp
{
    [SerializeField] private float _shieldDuration = 5f;

    //Metodos}
    //Se usa override para utilizar herencia por si se crean mas Powe ups a futuro
    public override void ApplyEffect(PlayerMovementPhone player)
    {
        PlayerShield shield = player.GetComponent<PlayerShield>();
        if (shield != null)
        {
            shield.Activate(_shieldDuration);
        }
    }
}
