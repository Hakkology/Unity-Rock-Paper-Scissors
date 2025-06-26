using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Arena : MonoBehaviour
{
    public static Arena Instance;
    
    [Header("Prefabs")]
    public GameObject rockPrefab;
    public GameObject paperPrefab;
    public GameObject scissorsPrefab;

    public List<IEntity> AllEntities { get; } = new List<IEntity>();
    public IEnumerable<IRock> Rocks => AllEntities.OfType<IRock>();
    public IEnumerable<IPaper> Papers => AllEntities.OfType<IPaper>();
    public IEnumerable<IScissor> Scissors => AllEntities.OfType<IScissor>();

    void Awake()
    {
        if (Instance == null)Instance = this;
        else Destroy(gameObject);
    }
}