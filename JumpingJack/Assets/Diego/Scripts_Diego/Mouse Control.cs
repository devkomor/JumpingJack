using UnityEngine;

public class MouseControl : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        transform.position = new Vector3(mousePosition.x, transform.position.y, transform.position.z);
    }
}
