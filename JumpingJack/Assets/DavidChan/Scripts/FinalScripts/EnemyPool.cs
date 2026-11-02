using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _poolSize = 12;
    [SerializeField] private List<EnemyBehaviour> _enemyList = new List<EnemyBehaviour>();
    [SerializeField] private float _levelWidth = 3.5f;
    [SerializeField] private float _minY = 0.2f;
    [SerializeField] private float _maxY = 3f;

    public static EnemyPool Instance { get { return instance; } }
    private static EnemyPool instance;

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
        Vector3 spawnPosition = new Vector3(1f, 1f, 1f);
        for(int i = 0; i < _poolSize; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.SetActive(true);

            EnemyBehaviour enemyComponent = enemy.GetComponent<EnemyBehaviour>();
            if(enemyComponent != null)
            {
                _enemyList.Add(enemyComponent);
            }

            spawnPosition.y += Random.Range(_minY, _maxY);
            spawnPosition.x = Random.Range(-_levelWidth, _levelWidth);
        }   
    }

    //Metodos

    public void RecycleEnemy(EnemyBehaviour enemyToRecycle)
    {
        float highestY = GetHighestEnemyY();
        Vector3 newPosition = new Vector3(Random.Range(-_levelWidth, _levelWidth), highestY + Random.Range(_minY, _maxY), 0f);
        enemyToRecycle.transform.position = newPosition;
    }
    private float GetHighestEnemyY()
    {
        float highestY = 0f;
        foreach(EnemyBehaviour enemy in _enemyList)
        {
            if(enemy.transform.position.y > highestY)
            {
                highestY = enemy.transform.position.y;
            }
        }
        return highestY;
    }
}
