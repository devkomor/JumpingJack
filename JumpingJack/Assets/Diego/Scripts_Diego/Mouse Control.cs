using UnityEngine;

public class MouseControl : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, transform.position.y, transform.position.z);
    }
}
