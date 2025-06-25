// EntityFactory.cs
using UnityEngine;
public enum EType { Rock, Paper, Scissors }
public class EntityFactory : MonoBehaviour
{
    public static EntityFactory Instance { get; private set; }

    [Header("Prefabs")]
    public GameObject rockPrefab;
    public GameObject paperPrefab;
    public GameObject scissorsPrefab;

    [Header("Spawn Settings")]
    public int countPerType = 20;
    public float areaSize = 8f;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        // Başlangıç spawn
        SpawnMany(rockPrefab, countPerType);
        SpawnMany(paperPrefab, countPerType);
        SpawnMany(scissorsPrefab, countPerType);
    }

    void SpawnMany(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = new Vector2(
                Random.Range(-areaSize, areaSize),
                Random.Range(-areaSize, areaSize)
            );
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }

    public void Convert(EType newType, GameObject victim)
    {
        GameObject prefab = newType switch
        {
            EType.Rock     => rockPrefab,
            EType.Paper    => paperPrefab,
            EType.Scissors => scissorsPrefab,
            _ => null
        };

        if (prefab != null)
            Instantiate(prefab, victim.transform.position, Quaternion.identity);

        Destroy(victim);
    }
}
