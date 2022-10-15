using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBear : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private byte damage;
    private float cooldown = Mathf.Infinity;
    private BoxCollider2D collider;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    private Health playerHealth;

    private EnemyRoute route;
    private Animator anim;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        route = GetComponentInParent<EnemyRoute>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldown += Time.deltaTime;
        if (PlayerVisibility() && playerHealth.currentHealth != 0)
        {
            if (cooldown >= attackCooldown)
            {
                cooldown = 0;
                anim.SetBool("moving", false);
                anim.SetTrigger("melee");
            }
        }

        if (route != null)
        {
            route.enabled = !PlayerVisibility();
        }
    }

    private bool PlayerVisibility()
    {
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(collider.bounds.size.x * range, collider.bounds.size.y, collider.bounds.size.z), 0, Vector2.left, 0, layer);
        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (collider != null)
            Gizmos.DrawWireCube(collider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(collider.bounds.size.x * range, collider.bounds.size.y, collider.bounds.size.z));
    }

    private void HurtPlayer()
    {
        if (PlayerVisibility() && playerHealth.currentHealth != 0)
        {
            playerHealth.Damage(damage);
        }
    }
}
