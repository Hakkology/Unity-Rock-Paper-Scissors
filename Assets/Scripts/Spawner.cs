using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private int spawnCount;

    void Start()
    {
        if (Arena.Instance.LeftSpawnPoints.Length < 3 || Arena.Instance.RightSpawnPoints.Length < 3)
        {
            Debug.LogError("Yetersiz spawn noktası!");
            return;
        }

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnLeftEntities();
            SpawnRightEntities();
        }
    }
    
    public void SpawnLeftEntities()
    {
        SpawnEntity(EType.Rock, EArenaSide.Left, Arena.Instance.LeftSpawnPoints[0]);
        SpawnEntity(EType.Paper, EArenaSide.Left, Arena.Instance.LeftSpawnPoints[1]);
        SpawnEntity(EType.Scissors, EArenaSide.Left, Arena.Instance.LeftSpawnPoints[2]);
    }

    public void SpawnRightEntities()
    {
        SpawnEntity(EType.Rock, EArenaSide.Right, Arena.Instance.RightSpawnPoints[0]);
        SpawnEntity(EType.Paper, EArenaSide.Right, Arena.Instance.RightSpawnPoints[1]);
        SpawnEntity(EType.Scissors, EArenaSide.Right, Arena.Instance.RightSpawnPoints[2]);
    }

    private void SpawnEntity(EType type, EArenaSide side, Vector2 spawnPoint)
    {
        GameObject prefab = type switch
        {
            EType.Rock => Arena.Instance.rockPrefab,
            EType.Paper => Arena.Instance.paperPrefab,
            EType.Scissors => Arena.Instance.scissorsPrefab,
            _ => null
        };

        if (prefab == null) return;

        GameObject go = Instantiate(prefab, spawnPoint, Quaternion.identity);
        if (go.TryGetComponent<IEntity>(out var entity))
        {
            entity.Side = side;
            Arena.Instance.AllEntities.Add(entity);
            entity.Init();
            Hud.Instance.UpdateEntityCounters();                         
        }
    }
}