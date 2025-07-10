// Entity.cs
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public abstract class Entity : MonoBehaviour, IEntity
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private float rotateSpeed = 4f;

    public ArenaSide Side { get; set; }
    public abstract EType Type { get; }
    public abstract EType Prey { get; }

    Rigidbody2D rb;
    Vector2 moveDirection;
    private bool canConvert = true;
    private float convertCooldown = 0.3f;
    private float cooldownTimer = 0f;

    public void Init()
    {
        rb = GetComponent<Rigidbody2D>();
        SetRandomDirection();
        // Debug.Log($"{Side} side is");
        rb.velocity = moveDirection * speed;
    }

    void Update()
    {
        RotateEntity();
        MoveEntity();
        //CheckBoundsAndBounce();


        if (!canConvert)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
                canConvert = true;
        }
    }

    void RotateEntity()
    {

        float randomFactor = Random.Range(0.75f, 1.25f);
        float zAngle = 10 * rotateSpeed * randomFactor * Time.deltaTime;
        gameObject.transform.Rotate(0f, 0f, zAngle);
    }

    void MoveEntity()
    {
        Vector2 proposedNextPos = rb.position + moveDirection * speed * Time.deltaTime;

        Vector2 bouncedDir = CheckBoundsAndBounce(proposedNextPos, moveDirection);
        if (bouncedDir != moveDirection)
        {
            moveDirection = bouncedDir;
            rb.velocity = moveDirection * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!canConvert) return;

        if (other.TryGetComponent<IEntity>(out var victim) && victim.Type == Prey)
        {
            Convert(victim, Type);
            canConvert = false;
            cooldownTimer = convertCooldown;
        }
    }

    Vector2 CheckBoundsAndBounce(Vector2 nextPos, Vector2 dir)
    {
        Bounds bounds = Arena.Instance.GetEffectiveBounds(Side);

        Vector2 center = bounds.center;
        Vector2 extents = bounds.extents;
        Vector2 newDir = dir;

        if (nextPos.x > center.x + extents.x || nextPos.x < center.x - extents.x)
            newDir.x *= -1;

        if (nextPos.y > center.y + extents.y || nextPos.y < center.y - extents.y)
            newDir.y *= -1;

        return newDir.normalized;
    }


    void SetRandomDirection()
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }

    public void Convert(IEntity victim, EType newType)
    {
        var victimSide = victim.Side;

        GameObject prefab = newType switch
        {
            EType.Rock => Arena.Instance.rockPrefab,
            EType.Paper => Arena.Instance.paperPrefab,
            EType.Scissors => Arena.Instance.scissorsPrefab,
            _ => null
        };

        if (prefab == null) return;

        var go = Instantiate(prefab, ((Entity)victim).transform.position, Quaternion.identity);
        if (go.TryGetComponent<IEntity>(out var newIe))
        {
            newIe.Side = victimSide;
            ((Entity)newIe).Init();
            Arena.Instance.AllEntities.Add(newIe);
        }

        Destroy(((Entity)victim).gameObject); 

        Arena.Instance.CheckAndDisableSideColliders();
        Hud.Instance.UpdateEntityCounters();
    }

    
    protected virtual void OnDestroy()
    {
        Arena.Instance.AllEntities.Remove(this);
    }
}
