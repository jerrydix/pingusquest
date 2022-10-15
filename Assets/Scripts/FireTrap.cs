using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float delayTime;
    [SerializeField] private float activeTime;
    [SerializeField] private byte damage;
    private Animator anim;
    private SpriteRenderer renderer;
    private bool active;
    private bool steppedOn;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (!steppedOn)
            {
                StartCoroutine(Activator());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (active && !other.GetComponent<Health>().dead)
            {
                other.GetComponent<Health>().Damage(damage);
            }
        }    
    }

    private IEnumerator Activator()
    {
        steppedOn = true;
        renderer.color = Color.red;
        yield return new WaitForSeconds(delayTime);
        renderer.color = Color.white;
        active = true;
        anim.SetBool("active", true);
        yield return new WaitForSeconds(activeTime);
        steppedOn = false;
        active = false;
        anim.SetBool("active", false);
    }
}
