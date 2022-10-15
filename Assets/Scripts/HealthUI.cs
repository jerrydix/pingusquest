using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Heart[] hearts;

    public void UpdateHearts()
    {
        byte activeHearts = health.currentHealth;
        if (hearts.Length != 0)
        {
            for (int i = 0; i < activeHearts; i++)
            {
                hearts[i].SetState(true);
            }

            for (int i = activeHearts; i < hearts.Length; i++)
            {
                hearts[i].SetState(false);
            }
        }
    }
}
