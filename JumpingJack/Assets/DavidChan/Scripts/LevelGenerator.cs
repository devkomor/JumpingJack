using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab;

    public int NumberOfPlatforms = 2;
    public float LevelWidth = 3f;
    public float minY = 0.2f;
    public float maxY = 1.5f;  

    void Start()
    {
        Vector3 spawnPosition = new Vector3();
        
        for (int i = 0; i < NumberOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-LevelWidth, LevelWidth);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void Update()
    {
        
    }
}
