using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Descenso con el nivel")]
    [SerializeField] private float _scrollSpeed = 3f;
    [SerializeField] private float _lowerLimit = -6f;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * _scrollSpeed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        if (transform.position.y < _lowerLimit)
        {
            RecycleToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovementPhone player = other.GetComponent<PlayerMovementPhone>();
        if (player == null) { return; }

        player.ApplyPowerUp(this);
        RecycleToPool();
    }

    // Metodos
    public virtual void ApplyEffect(PlayerMovementPhone player)
    {

    }
    protected void RecycleToPool()
    {
        PowerUpPool.Instance.RecyclePowerUp(this);
    }
}
