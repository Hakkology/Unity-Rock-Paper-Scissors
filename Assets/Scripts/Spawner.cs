// Spawner.cs
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [Header("Spawn Settings")]
    public int countPerType = 20;

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
        Bounds bounds = Arena.Instance.Bounds;
        for (int i = 0; i < count; i++)
        {
            float x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
            float y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));
            Vector3 pos = new Vector3(x, y, 0);
            var go = Instantiate(prefab, pos, Quaternion.identity);
            if (go.TryGetComponent<IEntity>(out var ie))
            {
                Arena.Instance.AllEntities.Add(ie);
            }
        }
    }
}
