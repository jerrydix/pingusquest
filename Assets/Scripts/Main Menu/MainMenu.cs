using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highscore;
    [SerializeField] private TextMeshProUGUI completionTime;
    private bool setData;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        setData = false;
        completionTime.text = "Fastest Completion Time:\n" + GamePreferencesSaver.Instance.completionTime;
        highscore.text = "Highest Amount\nof Collected\nFlakecoins:\n" + GamePreferencesSaver.Instance.highScore + " / 12";
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (!setData)
        {
            setData = true;
            completionTime.text = "Fastest Completion Time:\n" + GamePreferencesSaver.Instance.completionTime;
            highscore.text = "Highest Amount\nof Collected\nFlakecoins:\n" + GamePreferencesSaver.Instance.highScore + " / 12";
        }
    }
}
