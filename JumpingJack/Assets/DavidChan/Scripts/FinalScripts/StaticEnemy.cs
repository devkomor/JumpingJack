using Unity.VisualScripting;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    public bool _isAlive;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void DestroyEnemy()
    {
        _isAlive = false;
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ContactPoint contact = collision.contacts[GameObject.GetComponent<PlayerMovementPhone.Die()>];
        //El signo de ? sirve para verificar que la collision contra la que choque el objeto no sea nula
        //collision.gameObject.GetComponent<PlayerMovementPhone>()?.Die();
        PlayerMovementPhone player = collision.gameObject.GetComponent<PlayerMovementPhone>();

        if (player == null) { return; }

        player.TakeEnemyHit();
    }
}
