using System.Collections;
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
    public IEnumerable<IRock> LeftRocks => AllEntities.Where(e => e.Side == EArenaSide.Left).OfType<IRock>();
    public IEnumerable<IPaper> LeftPapers => AllEntities.Where(e => e.Side == EArenaSide.Left).OfType<IPaper>();
    public IEnumerable<IScissor> LeftScissors => AllEntities.Where(e => e.Side == EArenaSide.Left).OfType<IScissor>();
    public IEnumerable<IRock> RightRocks => AllEntities.Where(e => e.Side == EArenaSide.Right).OfType<IRock>();
    public IEnumerable<IPaper> RightPapers => AllEntities.Where(e => e.Side == EArenaSide.Right).OfType<IPaper>();
    public IEnumerable<IScissor> RightScissors => AllEntities.Where(e => e.Side == EArenaSide.Right).OfType<IScissor>();
    public IEnumerable<IEntity> LeftEntities => AllEntities.Where(e => e.Side == EArenaSide.Left);
    public IEnumerable<IEntity> RightEntities => AllEntities.Where(e => e.Side == EArenaSide.Right);


    [Header("Area")]
    [SerializeField] private Forcefield forcefield;
    [SerializeField] private Spawner spawner;
    [SerializeField] private BoxCollider2D mainArena;
    [SerializeField] private BoxCollider2D leftArena;
    [SerializeField] private BoxCollider2D rightArena;
    public Bounds MainArenaBounds => mainArena.bounds;
    public Bounds LeftArenaBounds => leftArena.bounds;
    public Bounds RightArenaBounds => rightArena.bounds;
    public bool IsLeftArenaActive => leftArena.enabled;
    public bool IsRightArenaActive => rightArena.enabled;
    public Vector2[] LeftSpawnPoints { get; private set; }
    public Vector2[] RightSpawnPoints { get; private set; }
    public bool LeftResolved { get; private set; } = false;
    public bool RightResolved { get; private set; } = false;

    private readonly Queue<(IEntity victim, EType newType, EArenaSide newSide)> convertQueue = new();
    private bool isConverting = false;


    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        CreateViewportBasedColliders();
        CalculateSpawnPoints();
    }

    void Update()
    {
        if (!isConverting && convertQueue.Count > 0)
            StartCoroutine(ProcessConvertQueue());
    }

    void CalculateSpawnPoints()
    {
        LeftSpawnPoints = GenerateEquilateralTrianglePoints(LeftArenaBounds, 3);
        RightSpawnPoints = GenerateEquilateralTrianglePoints(RightArenaBounds, 3);
    }

    public void EnqueueConvert(IEntity victim, EType newType, EArenaSide newSide)
    {
        convertQueue.Enqueue((victim, newType, newSide));
    }

    private IEnumerator ProcessConvertQueue()
    {
        isConverting = true;

        while (convertQueue.Count > 0)
        {
            var (victim, newType, newSide) = convertQueue.Dequeue();

            if (victim is Entity victimEntity && victimEntity != null && victimEntity.gameObject != null)
            {
                DoConvert(victim, newType, newSide);
            }

            yield return null;
        }

        isConverting = false;
    }

    private void DoConvert(IEntity victim, EType newType, EArenaSide newSide)
    {
        if (victim is not Entity victimEntity || victimEntity == null || victimEntity.gameObject == null)
            return;

        var victimSide = victim.Side;
        victimEntity.isPendingConvert = false;

        AllEntities.Remove(victim);

        GameObject prefab = newType switch
        {
            EType.Rock => rockPrefab,
            EType.Paper => paperPrefab,
            EType.Scissors => scissorsPrefab,
            _ => null
        };

        if (prefab == null) return;

        var go = Instantiate(prefab, victimEntity.transform.position, Quaternion.identity);
        if (go.TryGetComponent<IEntity>(out var newIe))
        {
            newIe.Side = (victimSide != newSide) ? newSide : victimSide;
            ((Entity)newIe).Init();
            AllEntities.Add(newIe);
        }

        Destroy(victimEntity.gameObject);

        CheckAndDisableSideColliders();
        HUDManager.Instance.UpdateEntityCounters();
    }

    public void CheckAndDisableSideColliders()
    {
        LeftResolved = false;
        RightResolved = false;

        if (IsLeftArenaActive)
        {
            int leftTypeCount = 0;
            if (LeftRocks.Any()) leftTypeCount++;
            if (LeftPapers.Any()) leftTypeCount++;
            if (LeftScissors.Any()) leftTypeCount++;

            if (leftTypeCount == 2)
            {
                SwitchLeftArena(false);
                forcefield.DisableLeftField();
            }
        }

        if (IsRightArenaActive)
        {
            int rightTypeCount = 0;
            if (RightRocks.Any()) rightTypeCount++;
            if (RightPapers.Any()) rightTypeCount++;
            if (RightScissors.Any()) rightTypeCount++;

            if (rightTypeCount == 2)
            {
                SwitchRightArena(false);
                forcefield.DisableRightField();
            }
        }

    }


    void CreateViewportBasedColliders()
    {
        Camera cam = Camera.main;
        if (!cam) return;

        float cameraDistanceFromPlane = Mathf.Abs(cam.transform.position.z);

        Vector3 mainBottomLeft = cam.ViewportToWorldPoint(new Vector3(0.04f, 0.04f, cameraDistanceFromPlane));
        Vector3 mainTopRight = cam.ViewportToWorldPoint(new Vector3(0.96f, 0.813333f, cameraDistanceFromPlane));
        CreateColliderZone(ref mainArena, "MainArenaZone", mainBottomLeft, mainTopRight);

        Vector3 leftBottomLeft = cam.ViewportToWorldPoint(new Vector3(0.04f, 0.04f, cameraDistanceFromPlane));
        Vector3 leftTopRight = cam.ViewportToWorldPoint(new Vector3(0.46f, 0.813333f, cameraDistanceFromPlane));
        CreateColliderZone(ref leftArena, "LeftArenaZone", leftBottomLeft, leftTopRight);

        Vector3 rightBottomLeft = cam.ViewportToWorldPoint(new Vector3(0.54f, 0.04f, cameraDistanceFromPlane));
        Vector3 rightTopRight = cam.ViewportToWorldPoint(new Vector3(0.96f, 0.813333f, cameraDistanceFromPlane));
        CreateColliderZone(ref rightArena, "RightArenaZone", rightBottomLeft, rightTopRight);
    }

    void CreateColliderZone(ref BoxCollider2D colRef, string name, Vector3 worldBL, Vector3 worldTR)
    {
        Vector2 center = (worldBL + worldTR) / 2f;
        Vector2 size = new Vector2(
            Mathf.Abs(worldTR.x - worldBL.x),
            Mathf.Abs(worldTR.y - worldBL.y)
        );

        GameObject go = new GameObject(name);
        go.transform.parent = this.transform;
        go.transform.position = center;

        colRef = go.AddComponent<BoxCollider2D>();
        colRef.size = size;
        colRef.isTrigger = true;
    }

    Vector2[] GenerateEquilateralTrianglePoints(Bounds bounds, int count)
    {
        Vector2 center = bounds.center;
        float radius = Mathf.Min(bounds.extents.x, bounds.extents.y) * 0.6f;

        Vector2[] points = new Vector2[count];

        for (int i = 0; i < count; i++)
        {
            float angleDeg = 120f * i - 90f;
            float angleRad = angleDeg * Mathf.Deg2Rad;

            points[i] = center + new Vector2(
                Mathf.Cos(angleRad) * radius,
                Mathf.Sin(angleRad) * radius
            );
        }

        return points;
    }

    public Bounds GetEffectiveBounds(EArenaSide side)
    {
        if (side == EArenaSide.Left)
            return IsLeftArenaActive ? LeftArenaBounds : MainArenaBounds;
        if (side == EArenaSide.Right)
            return IsRightArenaActive ? RightArenaBounds : MainArenaBounds;
        return MainArenaBounds;
    }

    public void SwitchLeftArena(bool toggle)
    {
        if (leftArena != null)
        {
            leftArena.enabled = toggle;
        }
    }

    public void SwitchRightArena(bool toggle)
    {
        if (rightArena != null)
        {
            rightArena.enabled = toggle;
        }
    }

    public void RestartArena()
    {
        GUIManager.Instance.playerPanel.ClosePanel();

        foreach (var entity in AllEntities.ToList())
        {
            if (entity is Entity e && e != null)
            {
                Destroy(e.gameObject);
            }
        }

        AllEntities.Clear();
        SwitchLeftArena(true);
        SwitchRightArena(true);

        LeftResolved = false;
        RightResolved = false;

        forcefield.ActivateFields();
        spawner.InitiateSpawningEntities();
        HUDManager.Instance.UpdateEntityCounters();
    }
}