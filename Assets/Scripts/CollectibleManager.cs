using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleManager : MonoBehaviour
{
    public int collectibleAmount;
    [SerializeField] private int maxCollectiblesInThisLevel;
    private float recordTime;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI timer;
    public TimeSpan timeSpan;
    private bool timerActive;
    public float elapsedTime;
    private Health playerHealth;
    private PlayerMovement playerMovement;
    public bool isEnded;

    public void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        isEnded = false;
        timer.text = "Time: 00:00.0";
        StartTimer();
    }
    
    private void Update()
    {
        if (playerHealth.currentHealth <= 0 || playerMovement.inPause || isEnded)
        {
            timerActive = false;
            
        }
        else if (!timerActive)
        {
            timerActive = true;
            StartCoroutine(UpdateTimer());
        }
    }

    public void StartTimer()
    {
        timerActive = true;
        elapsedTime = 0f;
        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerActive = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerActive)
        {
            elapsedTime += Time.deltaTime;
            timeSpan = TimeSpan.FromSeconds(elapsedTime);
            timer.text = "Time: " + timeSpan.ToString("mm':'ss'.'ff");
            yield return null;
        }
    }

    public void AddCollectible()
    {
        collectibleAmount++;
        score.SetText("Flakecoins: " + collectibleAmount);
    }
}
