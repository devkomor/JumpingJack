using System.Collections.Generic;
using UnityEngine;

public class PowerUpPool : MonoBehaviour
{
    [Header("Prefabs de Power-Ups")]
    [SerializeField] private GameObject[] _powerUpPrefabs;
    [SerializeField] private int _poolSize = 4;

    [Header("Frecuencia y Distancia de Aparición")]
    [SerializeField] private float _levelWidth = 3f;
    [Tooltip("Distancia vertical mínima entre un Power-Up y el siguiente")]
    [SerializeField] private float _minY = 12f;
    [Tooltip("Distancia vertical máxima entre un Power-Up y el siguiente")]
    [SerializeField] private float _maxY = 25f;

    private List<PowerUp> _powerUpList = new List<PowerUp>();
    private float _initialSpawnY = 10f;

    private static PowerUpPool _instance;
    public static PowerUpPool Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (_powerUpPrefabs == null || _powerUpPrefabs.Length == 0)
        {
            Debug.LogWarning("No hay prefabs asignados en PowerUpPool.");
            return;
        }

        Vector3 spawnPosition = new Vector3(Random.Range(-_levelWidth, _levelWidth), _initialSpawnY, 0f);

        for (int i = 0; i < _poolSize; i++)
        {
            int randomIndex = Random.Range(0, _powerUpPrefabs.Length);
            GameObject obj = Instantiate(_powerUpPrefabs[randomIndex], spawnPosition, Quaternion.identity);
            obj.SetActive(true);

            PowerUp powerUpComponent = obj.GetComponent<PowerUp>();
            if (powerUpComponent != null)
            {
                _powerUpList.Add(powerUpComponent);
            }

            spawnPosition.y += Random.Range(_minY, _maxY);
            spawnPosition.x = Random.Range(-_levelWidth, _levelWidth);
        }
    }

    public void RecyclePowerUp(PowerUp powerUpToRecycle)
    {
        powerUpToRecycle.gameObject.SetActive(false);

        float highestY = GetHighestPowerUpY();
        Vector3 newPosition = new Vector3(
            Random.Range(-_levelWidth, _levelWidth),
            highestY + Random.Range(_minY, _maxY),
            0f
        );

        powerUpToRecycle.transform.position = newPosition;
        powerUpToRecycle.gameObject.SetActive(true);
    }

    private float GetHighestPowerUpY()
    {
        float highestY = 8f;
        foreach (PowerUp powerUp in _powerUpList)
        {
            if (powerUp.transform.position.y > highestY)
            {
                highestY = powerUp.transform.position.y;
            }
        }
        return highestY;
    }
}
