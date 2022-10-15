using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagement : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject ending;
    [SerializeField] private AudioClip gameOverSound;
    private GameObject collectibleManager;
    [SerializeField] private TextMeshProUGUI scores;
    [SerializeField] private TextMeshProUGUI times;
    [SerializeField] private TextMeshProUGUI deadScore;
    [SerializeField] private TextMeshProUGUI pauseScore;

    private void Awake()
    {
        collectibleManager = GameObject.Find("CollectibleManager");
    }

    public void GameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("BackgroundMusic").SetActive(false);
        SoundManagement.instance.PlaySound(gameOverSound);
        int collectibleAmount = collectibleManager.GetComponent<CollectibleManager>().collectibleAmount;
        if (collectibleAmount > GamePreferencesSaver.Instance.highScore)
        {
            GamePreferencesSaver.Instance.highScore = collectibleAmount;
        }
        deadScore.text = "Collected Flakecoins:\n" +
                      collectibleManager.GetComponent<CollectibleManager>().collectibleAmount + " / 30\nHighscore:\n" +
                      GamePreferencesSaver.Instance.highScore + " / 30";
        gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0); // 0 is main menu
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Ending()
    {
        float elapsedTime = collectibleManager.GetComponent<CollectibleManager>().elapsedTime;
        TimeSpan elapsedSpan = collectibleManager.GetComponent<CollectibleManager>().timeSpan;
        int collectibleAmount = collectibleManager.GetComponent<CollectibleManager>().collectibleAmount;
        if (collectibleAmount > GamePreferencesSaver.Instance.highScore)
        {
            GamePreferencesSaver.Instance.highScore = collectibleAmount;
        }
        if (elapsedTime < GamePreferencesSaver.Instance.completionTimeFloat)
        {
            GamePreferencesSaver.Instance.completionTimeFloat = elapsedTime;
            GamePreferencesSaver.Instance.completionTime = elapsedSpan.ToString("mm':'ss'.'ff");
        }
        collectibleManager.GetComponent<CollectibleManager>().isEnded = true;
        ending.SetActive(true);
        scores.text = "Collected Flakecoins:\n" +
                      collectibleManager.GetComponent<CollectibleManager>().collectibleAmount + " / 12\nHighscore:\n" +
                      GamePreferencesSaver.Instance.highScore + " / 12";
        times.text = "Final Time:\n" +
                     collectibleManager.GetComponent<CollectibleManager>().timeSpan.ToString("mm':'ss'.'ff") +
                     "\nRecord Time:\n" + GamePreferencesSaver.Instance.completionTime;
    }

    public void PauseMenu()
    {
        int collectibleAmount = collectibleManager.GetComponent<CollectibleManager>().collectibleAmount;
        if (collectibleAmount > GamePreferencesSaver.Instance.highScore)
        {
            GamePreferencesSaver.Instance.highScore = collectibleAmount;
        }
        pauseScore.text = "Collected Flakecoins:\n" +
                         collectibleManager.GetComponent<CollectibleManager>().collectibleAmount + " / 12\nHighscore:\n" +
                         GamePreferencesSaver.Instance.highScore + " / 12";
    }
}
