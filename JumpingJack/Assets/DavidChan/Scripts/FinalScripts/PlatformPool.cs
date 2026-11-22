using System.Collections.Generic;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private int _poolSize = 15;
    [SerializeField] private List<Platform> _platformList = new List<Platform>();
    [SerializeField] private float _levelWidth = 3.5f;

    [Header("Dificultad Inicial (Fácil)")]
    [Tooltip("Separación vertical mínima al empezar la partida")]
    [SerializeField] private float _easyMinY = 0.5f;
    [Tooltip("Separación vertical máxima al empezar la partida")]
    [SerializeField] private float _easyMaxY = 1.2f;

    [Header("Dificultad Máxima (Difícil)")]
    [Tooltip("Separación vertical mínima en dificultad máxima")]
    [SerializeField] private float _hardMinY = 1.8f;
    [Tooltip("Separación vertical máxima en dificultad máxima (No superar altura máxima de salto)")]
    [SerializeField] private float _hardMaxY = 3.2f;
    [Header("Curva de Progresión")]
    [Tooltip("Segundos de partida necesarios para alcanzar la dificultad máxima")]
    [SerializeField] private float _timeToMaxDifficulty = 90f;
    private static PlatformPool _instance;
    private float _initialSpawnY = -4.2f;
    private float _elapsedTime = 0f;
    public static PlatformPool Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null){_instance = this;} else { Destroy(gameObject); }
    }
    void Start()
    {
        Vector3 spawnPosition = new Vector3(0f, _initialSpawnY, 0f);

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject platform = Instantiate(_platformPrefab, spawnPosition, Quaternion.identity);
            platform.SetActive(true);

            Platform platformComponent = platform.GetComponent<Platform>();
            if (platformComponent != null)
            {
                _platformList.Add(platformComponent);
            }
            spawnPosition.y += Random.Range(_easyMinY, _easyMaxY);
            spawnPosition.x = Random.Range(-_levelWidth, _levelWidth);
        }
    }
    private void Update()
    {
        if (_elapsedTime < _timeToMaxDifficulty)
        {
            _elapsedTime += Time.deltaTime;
        }
    }

    //Metodos

    public void RecyclePlatform(Platform platformToRecycle)
    {
        platformToRecycle.gameObject.SetActive(false);
        float difficultyFactor = Mathf.Clamp01(_elapsedTime / _timeToMaxDifficulty);
        float currentMinY = Mathf.Lerp(_easyMinY, _hardMinY, difficultyFactor);
        float currentMaxY = Mathf.Lerp(_easyMaxY, _hardMaxY, difficultyFactor);
        float highestY = GetHighestPlatformY();
        Vector3 newPosition = new Vector3(Random.Range(-_levelWidth, _levelWidth), highestY + Random.Range(currentMinY, currentMaxY), 0f);
        platformToRecycle.transform.position = newPosition;
        platformToRecycle.ResetPlatform();
        platformToRecycle.gameObject.SetActive(true);
    }

    private float GetHighestPlatformY()
    {
        float highestY = float.MinValue;
        foreach (Platform platform in _platformList)
        {
            if (platform.transform.position.y > highestY)
            {
                highestY = platform.transform.position.y;
            }
        }
        return highestY;
    }
}