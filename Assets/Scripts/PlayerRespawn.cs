using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentPoint;
    private Health health;
    private UIManagement management;

    private void Awake()
    {
        health = GetComponent<Health>();
        management = FindObjectOfType<UIManagement>();
    }

    private void CheckRespawn()
    {
        if (currentPoint == null)
        {
            management.GameOver();
            return;
        }
        transform.position = currentPoint.position;
        health.Respawn();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Checkpoint"))
        {
            currentPoint = col.transform;
            SoundManagement.instance.PlaySound(checkpointSound);
            col.GetComponent<Collider2D>().enabled = false;
            col.GetComponent<Animator>().SetTrigger("activate");
        }
    }
}
