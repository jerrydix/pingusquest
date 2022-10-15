using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPickup : MonoBehaviour
{
    [SerializeField] private byte healthValue;
    [SerializeField] private AudioClip healthPickupSound;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SoundManagement.instance.PlaySound(healthPickupSound);
            col.GetComponent<Health>().Heal(healthValue);
            gameObject.SetActive(false);
        }
    }
}
