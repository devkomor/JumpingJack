using UnityEngine;

public class MouseControl : MonoBehaviour // Revisar: el nombre del archivo debe coincidir con el de la clase
{
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        transform.position = new Vector3(mousePosition.x, transform.position.y, transform.position.z);
    }
}
