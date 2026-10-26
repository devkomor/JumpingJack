using UnityEngine;

public class MouseControl : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Sin abreviaciones
        transform.position = new Vector3(mousePos.x, transform.position.y, transform.position.z);
    }
}
