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

    [Header("Area")]
    [SerializeField] private BoxCollider2D mainArena;
    [SerializeField] private BoxCollider2D leftArena;
    [SerializeField] private BoxCollider2D rightArena;
    public Bounds MainArenaBounds => mainArena.bounds;
    public Bounds LeftArenaBounds => leftArena.bounds;
    public Bounds RightArenaBounds => rightArena.bounds;


    public Vector2[] LeftSpawnPoints { get; private set; }
    public Vector2[] RightSpawnPoints { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        CreateViewportBasedColliders();
        CalculateSpawnPoints();
    }

    void CalculateSpawnPoints()
    {
        LeftSpawnPoints = GenerateEquilateralTrianglePoints(LeftArenaBounds, 3);
        RightSpawnPoints = GenerateEquilateralTrianglePoints(RightArenaBounds, 3);
    }

    void CreateViewportBasedColliders()
    {
        Camera cam = Camera.main;
        if (!cam) return;

        float cameraDistanceFromPlane = Mathf.Abs(cam.transform.position.z);

        Vector3 mainBottomLeft = cam.ViewportToWorldPoint(new Vector3(0.04f, 0.04f, cameraDistanceFromPlane));
        Vector3 mainTopRight   = cam.ViewportToWorldPoint(new Vector3(0.96f, 0.813333f, cameraDistanceFromPlane));
        CreateColliderZone(ref mainArena, "MainArenaZone", mainBottomLeft, mainTopRight);

        Vector3 leftBottomLeft = cam.ViewportToWorldPoint(new Vector3(0.04f, 0.04f, cameraDistanceFromPlane));
        Vector3 leftTopRight   = cam.ViewportToWorldPoint(new Vector3(0.44f, 0.813333f, cameraDistanceFromPlane));
        CreateColliderZone(ref leftArena, "LeftArenaZone", leftBottomLeft, leftTopRight);

        Vector3 rightBottomLeft = cam.ViewportToWorldPoint(new Vector3(0.52f, 0.04f, cameraDistanceFromPlane));
        Vector3 rightTopRight   = cam.ViewportToWorldPoint(new Vector3(0.96f, 0.813333f, cameraDistanceFromPlane));
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
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if (LeftSpawnPoints != null)
            foreach (var p in LeftSpawnPoints)
                Gizmos.DrawSphere(p, 0.2f);

        Gizmos.color = Color.yellow;
        if (RightSpawnPoints != null)
            foreach (var p in RightSpawnPoints)
                Gizmos.DrawSphere(p, 0.2f);
    }
}