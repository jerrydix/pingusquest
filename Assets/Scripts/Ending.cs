using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] private AudioClip endingSound;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SoundManagement.instance.PlaySound(endingSound);
            GameObject.Find("BackgroundMusic").SetActive(false);
            col.gameObject.GetComponent<PlayerMovement>().inEnd = true;
            col.gameObject.GetComponent<PlayerAttack>().enabled = false;
            GameObject.Find("UI").GetComponent<UIManagement>().Ending();
        }
    }
}
