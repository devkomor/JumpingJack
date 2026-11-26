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

    //Metodos 
    public void Activate(float duration)
    {
        _remainingTime = duration;
        _shieldVisual.SetActive(true);
    }

    public bool IsActive() { return _remainingTime > 0f; }
}
