using UnityEngine;

public class MouseControl : MonoBehaviour
{
  private Camera mainCamera;

    [SerializeField]
    private float maxSpeed = 10f;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        FollowMousePositionDelayed(maxSpeed);
    }
     
    private void FollowMousePosition()
    {
        transform.position = GetWorldPositionFromMouse();

    }

    private void FollowMousePositionDelayed(float maxSpeed)
    {
        transform.position = Vector2.MoveTowards(transform.position, GetWorldPositionFromMouse(), maxSpeed * Time.deltaTime); 
    }

    private Vector2 GetWorldPositionFromMouse()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}
