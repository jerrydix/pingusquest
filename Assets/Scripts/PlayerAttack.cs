using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject[] projectiles;
    private float cooldownCounter;
    private PlayerMovement movement;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject gameOverScreen;
    
    [SerializeField] private AudioClip shootSound;
    void Awake()
    {
        cooldownCounter = Mathf.Infinity;
        movement = GetComponent<PlayerMovement>();
    }
    
    void Update()
    {
        if (!pauseMenu.activeInHierarchy && !optionsMenu.activeInHierarchy && !gameOverScreen.activeInHierarchy)
        {
            if (Input.GetMouseButton(0) && cooldownCounter > cooldown && movement.canAttack())
            {
                Shoot();
            }
            cooldownCounter += Time.deltaTime;
        }
    }

    void Shoot()
    {
        SoundManagement.instance.PlaySound(shootSound);
        cooldownCounter = 0;
        projectiles[selectProjectile()].transform.position = shootingPoint.position;
        projectiles[selectProjectile()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    int selectProjectile()
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
