using System.Collections.Generic;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private int _poolSize = 4;
    [SerializeField] private List<GameObject> _platformList;

    private float LevelWidth = 3.5f;
    private float _minY = 0.2f;
    private float _maxY = 3f;

    private static PlatformPool instance;
    public static PlatformPool Instance { get { return instance; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 spawnPosition = new Vector3();
        for(int i = 0; i < _poolSize; i++)
        {
            spawnPosition.y += Random.Range(_minY, _maxY);
            spawnPosition.x = Random.Range(-LevelWidth, LevelWidth);
            GameObject platform = Instantiate(_platformPrefab, spawnPosition, Quaternion.identity);
            platform.SetActive(true);
            _platformList.Add(platform);
        }
    }
}