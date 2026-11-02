using Unity.VisualScripting;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ContactPoint contact = collision.contacts[GameObject.GetComponent<PlayerMovementPhone.Die()>];
        //El signo de ? sirve para verificar que la collision contra la que choque el objeto no sea nula
        collision.gameObject.GetComponent<PlayerMovementPhone>()?.Die();
    }
}
