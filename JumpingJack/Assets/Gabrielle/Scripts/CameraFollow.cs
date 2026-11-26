using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target; 

    void LateUpdate ()
    {
        if (Target.position.y > transform.position.y)
        {
            Vector3 newPosition = new Vector3(transform.position.x, Target.position.y, transform.position.z); // Sin abreviaciones
            transform.position = newPosition;
        }    
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
