// Sugerencia general de Clean Code: usar if de una sola línea con llaves cuando sea legible.

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [Header("Prefabs de enemigos")]
    [SerializeField] private GameObject _staticEnemyPrefab;
    [SerializeField] private GameObject _movingEnemyPrefab;

    [Header("Probabilidad de Aparición")]
    [Tooltip("Porcentaje de probabilidad de que se genere un enemigo estático (0 a 100)")]
    [Range(0f, 100f)]
    [SerializeField] private float _staticEnemyChance = 70f;

    [Header("Configuración de nivel")]
    [SerializeField] private int _poolSize = 12;
    [SerializeField] private List<EnemyBehaviour> _enemyList = new List<EnemyBehaviour>();
    [SerializeField] private float _levelWidth = 3.5f;
    [SerializeField] private float _minimumY = 0.2f; // Sin abreviaciones
    [SerializeField] private float _maximumY = 3f; // Sin abreviaciones

    private float _initialSpawnY = 6f;
    private static EnemyPool _instance; // Usar _camelCase | Respetar el orden de los miembros | Respetar el orden de acceso y static
    public static EnemyPool Instance { get { return _instance; } }

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
    void Start()
    {
        Vector3 spawnPosition = new Vector3(0f, _initialSpawnY, 0f);

        for (int i = 0; i < _poolSize; i++)
        {
            float randomRoll = Random.Range(0f, 100f);
            GameObject selectedPrefab;

            if (randomRoll < _staticEnemyChance)
            {
                selectedPrefab = _staticEnemyPrefab;
            }
            else
            {
                selectedPrefab = _movingEnemyPrefab;
            }

            GameObject enemy = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
            enemy.SetActive(true);

            EnemyBehaviour enemyComponent = enemy.GetComponent<EnemyBehaviour>();
            if (enemyComponent != null)
            {
                _enemyList.Add(enemyComponent);
            }

            spawnPosition.y += Random.Range(_minimumY, _maximumY);
            spawnPosition.x = Random.Range(-_levelWidth, _levelWidth);
        }
    }

    //Metodos
    public void RecycleEnemy(EnemyBehaviour enemyToRecycle)
    {
        enemyToRecycle.gameObject.SetActive(false);
        float highestY = GetHighestEnemyY();
        Vector3 newPosition = new Vector3(Random.Range(-_levelWidth, _levelWidth), highestY + Random.Range(_minimumY, _maximumY), 0f);
        enemyToRecycle.transform.position = newPosition;
        enemyToRecycle.gameObject.SetActive(true);
    }
    private float GetHighestEnemyY()
    {
        float highestY = 8f;
        foreach (EnemyBehaviour enemy in _enemyList)
        {
            if (enemy.transform.position.y > highestY)
            {
                highestY = enemy.transform.position.y;
            }
        }
        return highestY;
    }
}
