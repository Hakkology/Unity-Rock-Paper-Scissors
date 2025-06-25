using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public abstract class Entity : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;

    // Alt sınıflar override edecek:
    protected abstract EType Type     { get; }
    protected abstract EType Prey     { get; }
    protected abstract EType Predator { get; }

    Rigidbody2D   rb;
    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    void Update()
    {
        ChasePrey();
    }

    void ChasePrey()
    {
        var targets = GameObject.FindGameObjectsWithTag(Prey.ToString());
        if (targets.Length == 0) return;

        var nearest = targets
            .OrderBy(t => (t.transform.position - transform.position).sqrMagnitude)
            .First();

        Vector2 dir = (nearest.transform.position - transform.position).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Prey.ToString()))
        {
            // Factory’ye dönüşüm talebi
            EntityFactory.Instance.Convert(Type, other.gameObject);
        }
    }

    void UpdateColor()
    {
        switch (Type)
        {
            case EType.Rock:     sr.color = Color.gray;    break;
            case EType.Paper:    sr.color = Color.white;   break;
            case EType.Scissors: sr.color = Color.magenta; break;
        }
    }
}