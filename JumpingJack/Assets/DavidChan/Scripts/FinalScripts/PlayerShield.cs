using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] private GameObject _shieldVisual;
    private float _remainingTime;

    private void Update()
    {
        if (!IsActive()) { return; }

        _remainingTime -= Time.deltaTime;

        if (!IsActive()) { _shieldVisual.SetActive(false); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsActive()) { return; }

        EnemyBehaviour enemy = collision.gameObject.GetComponent<EnemyBehaviour>();

        if (enemy != null) { EnemyPool.Instance.RecycleEnemy(enemy); }

        StaticEnemy staticEnemy = collision.gameObject.GetComponent<StaticEnemy>();

        if (staticEnemy != null) { staticEnemy.DestroyEnemy(); }
    }

    //Metodos 
    public void Activate(float duration)
    {
        _remainingTime = duration;
        _shieldVisual.SetActive(true);
    }

    public bool IsActive() { return _remainingTime > 0f; }
}
