using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    private bool hit;
    private BoxCollider2D collider;
    private float direction;
    
    void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (hit)
        {
            return;
        }

        float speed = projectileSpeed * Time.deltaTime * direction;
        transform.Translate(speed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Pickup") || col.CompareTag("Checkpoint") || col.CompareTag("FireTrap")) return;
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Health>().Damage(1);
        }
        hit = true;
        collider.enabled = false;
        Destroy();
    }

    public void SetDirection(float dir)
    {
        direction = dir;
        gameObject.SetActive(true);
        hit = false;
        collider.enabled = true;

        float scaleX = transform.localScale.x;
        if (Mathf.Sign(scaleX) != dir)
        {
            scaleX *= -1;
        }

        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }
}
