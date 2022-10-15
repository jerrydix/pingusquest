using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GamePreferences : MonoBehaviour
{
    // Start is called before the first frame update
    public static GamePreferences Instance { get; set; }
    [SerializeField] private AudioMixer mixer;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadPrefs();
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    private void OnApplicationQuit()
    {
        SavePrefs();
    }

    private void SavePrefs()
    {
        PlayerPrefs.SetFloat("Volume", GamePreferencesSaver.Instance.volume);
        PlayerPrefs.SetInt("GraphicsIndex", GamePreferencesSaver.Instance.graphicsIndex);
        PlayerPrefs.SetInt("FullScreenToggle", GamePreferencesSaver.Instance.fullScreenToggle);
        PlayerPrefs.SetInt("ResolutionIndex", GamePreferencesSaver.Instance.resolutionIndex);
        PlayerPrefs.SetInt("HighScore", GamePreferencesSaver.Instance.highScore);
        PlayerPrefs.SetString("CompletionTime", GamePreferencesSaver.Instance.completionTime);
        PlayerPrefs.SetFloat("CompletionTimeFloat", GamePreferencesSaver.Instance.completionTimeFloat);
        PlayerPrefs.Save();
    }

    private void LoadPrefs()
    {
        var volume = PlayerPrefs.GetFloat("Volume", 0.0f);
        var graphicsIndex = PlayerPrefs.GetInt("GraphicsIndex", 6);
        var fullScreenToggle = PlayerPrefs.GetInt("FullScreenToggle", 1);

        Resolution[] resolutions;
        resolutions = Screen.resolutions;

        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].height == Screen.height && resolutions[i].width == Screen.width)
            {
                currentResIndex = i;
            }
        }
        
        var resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", currentResIndex);
        var highScore = PlayerPrefs.GetInt("HighScore", 0);
        var completionTime = PlayerPrefs.GetString("CompletionTime", "-");
        var completionTimeFloat =
            PlayerPrefs.GetFloat("CompletionTimeFloat", float.MaxValue);
        
        GamePreferencesSaver.Instance.volume = volume;
        GamePreferencesSaver.Instance.graphicsIndex = graphicsIndex;
        GamePreferencesSaver.Instance.fullScreenToggle = fullScreenToggle;
        GamePreferencesSaver.Instance.resolutionIndex = resolutionIndex;
        GamePreferencesSaver.Instance.highScore = highScore;
        GamePreferencesSaver.Instance.completionTime = completionTime;
        GamePreferencesSaver.Instance.completionTimeFloat = completionTimeFloat;

        mixer.SetFloat("Volume", volume);
        QualitySettings.SetQualityLevel(graphicsIndex);
        Screen.fullScreen = fullScreenToggle == 1;
        resolutions = Screen.resolutions;
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
