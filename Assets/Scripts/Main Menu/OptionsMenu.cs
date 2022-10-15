using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    private Resolution[] resolutions;
    private TMP_Dropdown resDropdown;
    private Slider volumeSlider;
    private Toggle fullscreenToggle;
    private TMP_Dropdown graphicsIndex;

    private void Awake()
    {
        volumeSlider = GetComponentInChildren<Slider>();
        volumeSlider.value = GamePreferencesSaver.Instance.volume; 
        SetVolume(GamePreferencesSaver.Instance.volume);
        fullscreenToggle = GetComponentInChildren<Toggle>();
        fullscreenToggle.isOn = GamePreferencesSaver.Instance.fullScreenToggle == 1;
        SetFullscreen(GamePreferencesSaver.Instance.fullScreenToggle == 1);
        graphicsIndex = GameObject.Find("Graphics").GetComponent<TMP_Dropdown>();
        graphicsIndex.value = GamePreferencesSaver.Instance.graphicsIndex;
        SetQuality(GamePreferencesSaver.Instance.graphicsIndex);
        resDropdown = GameObject.Find("ResolutionDropdown").GetComponent<TMP_Dropdown>();
        
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string res = resolutions[i].width + " x " + resolutions[i].height + " @" + resolutions[i].refreshRate + "hz";
            options.Add(res);

            if (resolutions[i].height == Screen.height && resolutions[i].width == Screen.width)
            {
                currentResIndex = i;
            }
        }
        resDropdown.AddOptions(options);
        if (GamePreferencesSaver.Instance.resolutionIndex == -1)
        {
            resDropdown.value = currentResIndex;
        }
        else
        {
            resDropdown.value = GamePreferencesSaver.Instance.resolutionIndex;
        }
        resDropdown.RefreshShownValue();
        
    }

    public void UpdateResolution(int resIndex)
    {
        GamePreferencesSaver.Instance.resolutionIndex = resIndex;
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);    
    }

    public void SetVolume(float volume)
    {
        GamePreferencesSaver.Instance.volume = volume;
        mixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int index)
    {
        GamePreferencesSaver.Instance.graphicsIndex = index;
        QualitySettings.SetQualityLevel(index);
    }
    
    public void SetFullscreen(bool fullscreened)
    {
        GamePreferencesSaver.Instance.fullScreenToggle = fullscreened ? 1 : 0;
        Screen.fullScreen = fullscreened;
    }
}
