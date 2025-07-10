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
        SpawnEntity(EType.Rock, ArenaSide.Left, Arena.Instance.LeftSpawnPoints[0]);
        SpawnEntity(EType.Paper, ArenaSide.Left, Arena.Instance.LeftSpawnPoints[1]);
        SpawnEntity(EType.Scissors, ArenaSide.Left, Arena.Instance.LeftSpawnPoints[2]);
    }

    public void SpawnRightEntities()
    {
        SpawnEntity(EType.Rock, ArenaSide.Right, Arena.Instance.RightSpawnPoints[0]);
        SpawnEntity(EType.Paper, ArenaSide.Right, Arena.Instance.RightSpawnPoints[1]);
        SpawnEntity(EType.Scissors, ArenaSide.Right, Arena.Instance.RightSpawnPoints[2]);
    }

    private void SpawnEntity(EType type, ArenaSide side, Vector2 spawnPoint)
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
        }
    }
}