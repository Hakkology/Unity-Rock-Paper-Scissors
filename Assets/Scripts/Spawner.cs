// Spawner.cs
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [Header("Spawn Settings")]
    public int countPerType = 20;
    public float areaSize = 8f;

    void Start()
    {
        SpawnAll();

        foreach (var ie in Arena.Instance.AllEntities)
            ((Entity)ie).Init();
    }

    void SpawnAll()
    {
        SpawnMany(Arena.Instance.rockPrefab,     countPerType);
        SpawnMany(Arena.Instance.paperPrefab,    countPerType);
        SpawnMany(Arena.Instance.scissorsPrefab, countPerType);
    }

    void SpawnMany(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = new Vector2(
                Random.Range(-areaSize, areaSize),
                Random.Range(-areaSize, areaSize)
            );
            var go = Instantiate(prefab, pos, Quaternion.identity);
            if (go.TryGetComponent<IEntity>(out var ie))
            {
                Arena.Instance.AllEntities.Add(ie);
            }
        }
    }
}
