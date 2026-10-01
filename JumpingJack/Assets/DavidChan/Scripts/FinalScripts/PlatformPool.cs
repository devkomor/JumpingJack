using System.Collections.Generic;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private int _poolSize = 12;
    [SerializeField] private List<Platform> _platformList = new List<Platform>();

    [SerializeField] private float LevelWidth = 3.5f;
    [SerializeField] private float _minY = 0.2f;
    [SerializeField] private float _maxY = 3f;

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
    void Start()
    {
        Vector3 spawnPosition = new Vector3(0f, -4.2f, 0f);
        for(int i = 0; i < _poolSize; i++)
        {
            GameObject platform = Instantiate(_platformPrefab, spawnPosition, Quaternion.identity);
            platform.SetActive(true);
            
            Platform platformComponent = platform.GetComponent<Platform>();
            if (platformComponent != null)
            {
                _platformList.Add(platformComponent);
            }

            spawnPosition.y += Random.Range(_minY, _maxY);
            spawnPosition.x = Random.Range(-LevelWidth, LevelWidth);

        }
    }

    //Metodos

    public void RecyclePlatform(Platform platformToRecycle)
    {
        float highestY = GetHighestPlatformY();

        Vector3 newPosition = new Vector3(Random.Range(-LevelWidth, LevelWidth), highestY + Random.Range(_minY, _maxY),0f );
    
        platformToRecycle.transform.position = newPosition;
    }

    private float GetHighestPlatformY()
    {
        float highestY = 0f;
        foreach (Platform plat in _platformList)
        {
            if (plat.transform.position.y > highestY)
            {
                highestY = plat.transform.position.y;
            }
        }
        return highestY;
    }
}