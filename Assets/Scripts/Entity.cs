// Entity.cs
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public abstract class Entity : MonoBehaviour, IEntity
{
    [Header("Movement Settings")]
    [SerializeField]private float speed = 2f;
    [SerializeField]private float rotateSpeed = 2f;

    public abstract EType Type { get; }
    public abstract EType Prey { get; }

    Rigidbody2D rb;
    public void Init() => rb = GetComponent<Rigidbody2D>();
    void Update()
    {
        RotateEntity();
        ChasePrey();
    }

    void RotateEntity()
    {
        
        float randomFactor = Random.Range(0.75f, 1.25f);
        float zAngle = 10 * speed * randomFactor * Time.deltaTime;
        gameObject.transform.Rotate(0f, 0f, zAngle);
    }

    void ChasePrey()
    {
        var targets = Arena.Instance.AllEntities
            .Where(e => e.Type == Prey)
            .Cast<Entity>()
            .ToArray();

        if (targets.Length == 0)
            return;

        var nearest = targets
            .OrderBy(t => (t.transform.position - transform.position).sqrMagnitude)
            .First();

        Vector2 dir = (nearest.transform.position - transform.position).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IEntity>(out var victim) &&
            victim.Type == Prey)
        {
            Convert(victim, Type);
        }
    }
    
    public void Convert(IEntity victim, EType newType)
    {
        Arena.Instance.AllEntities.Remove(victim);
        Destroy(((Entity)victim).gameObject);

        GameObject prefab = newType switch
        {
            EType.Rock     => Arena.Instance.rockPrefab,
            EType.Paper    => Arena.Instance.paperPrefab,
            EType.Scissors => Arena.Instance.scissorsPrefab,
            _ => null
        };

        if (prefab == null) return;

        var go = Instantiate(prefab, ((Entity)victim).transform.position, Quaternion.identity);
        if (go.TryGetComponent<IEntity>(out var newIe))
        {
            Arena.Instance.AllEntities.Add(newIe);
            ((Entity)newIe).Init();
        }
    }
}
