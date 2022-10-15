using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private CollectibleManager manager;
    [SerializeField] private AudioClip collectibleSound;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SoundManagement.instance.PlaySound(collectibleSound);
            manager.AddCollectible();
            gameObject.SetActive(false);
        }
    }
}
