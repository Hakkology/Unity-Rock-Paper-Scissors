// Entity.cs
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public abstract class Entity : MonoBehaviour, IEntity
{
    [Header("Movement Settings")]
    [SerializeField] private float rotateSpeed = 4f;

    public EArenaSide Side { get; set; }
    public abstract EType Type { get; }
    public abstract EType Prey { get; }
    public bool isPendingConvert = false;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Vector2 moveDirection;

    private float speed;
    private bool canConvert = true;
    private float convertCooldown = 0.25f;
    private float cooldownTimer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void Init()
    {
        SetRandomDirection();
        speed = Random.Range(12, 20);
        rb.velocity = moveDirection * speed;
        if (Side == EArenaSide.Right)
            sr.color = new Color32(0x8A, 0x9A, 0xFF, 0xFF);
        else if (Side == EArenaSide.Left)
            sr.color = new Color32(0xFF, 0xB9, 0xB9, 0xFF); 
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

        if (!other.TryGetComponent<IEntity>(out var victim)) return;
        if (victim.Type != Prey) return;

        //if (victim.Side == this.Side) return;

        Convert(victim, Type, Side);
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

    public void Convert(IEntity victim, EType newType, EArenaSide newSide)
    {
        if (victim is Entity e && !e.isPendingConvert)
        {
            e.isPendingConvert = true;
            Arena.Instance.EnqueueConvert(victim, newType, newSide);
            canConvert = false;
            cooldownTimer = convertCooldown;
        }
    }

    protected virtual void OnDestroy()
    {
        Arena.Instance.AllEntities.Remove(this);
    }
}
