using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GamePreferencesSaver : MonoBehaviour
{
    public static GamePreferencesSaver Instance { get; private set; }
    // Start is called before the first frame update
    public float volume;
    public int graphicsIndex;
    public int fullScreenToggle;
    public int resolutionIndex;
    public int highScore;
    public string completionTime;
    public float completionTimeFloat;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //print(volume.ToString());
    }
}
