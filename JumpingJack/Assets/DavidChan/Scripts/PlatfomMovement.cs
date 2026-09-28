using UnityEngine;

public class PlatfomMovement : MonoBehaviour
{
    public float velocidad = 3f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * velocidad * Time.deltaTime);
    }
}
