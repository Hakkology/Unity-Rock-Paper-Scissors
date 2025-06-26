// Entity.cs
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public abstract class Entity : MonoBehaviour, IEntity
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public abstract EType Type { get; }
    public abstract EType Prey { get; }

    protected Rigidbody2D rb;
    SpriteRenderer sr;
    public void Init()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ChasePrey();
    }

    void ChasePrey()
    {
        // Arena’daki tüm IEntity’ler içinden enum’a göre filtrele
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
