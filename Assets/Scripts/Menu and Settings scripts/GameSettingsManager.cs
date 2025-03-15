using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsManager : MonoBehaviour
{

    public Slider _sounds;

    [SerializeField] private string soundsKey;

    public Dropdown _screenResolution;
    public Dropdown _graphicsQuality;

    [SerializeField] private string resolutionKey;
	[SerializeField] private string graphicsKey;

	public bool _isFullscreen = true;
	public Toggle _fullscreenModeActivator;
	[SerializeField] private string fullscreenKey;

	void Start()
    {
        if (!PlayerPrefs.HasKey(soundsKey))
        {
			PlayerPrefs.SetFloat(soundsKey, 0.5f);
            _sounds.value = 0.5f;
		}
        else
        {
            _sounds.value = PlayerPrefs.GetFloat(soundsKey);
        }

		if (!PlayerPrefs.HasKey(resolutionKey))
		{
			PlayerPrefs.SetInt(resolutionKey, 2);
			_screenResolution.value = 2;
			//SaveScreenResolutionSet();
		}
		else
		{
			_screenResolution.value = PlayerPrefs.GetInt(resolutionKey);
		}

		if (!PlayerPrefs.HasKey(graphicsKey))
		{
			PlayerPrefs.SetInt(graphicsKey, 1);
			_graphicsQuality.value = 1;
			//SaveGraphicsQuakitySet();
		}
		else
		{
			_graphicsQuality.value = PlayerPrefs.GetInt(graphicsKey);
		}

		if (!PlayerPrefs.HasKey(fullscreenKey))
		{
			PlayerPrefs.SetString(fullscreenKey, "true");
			_fullscreenModeActivator.isOn = true;
			_isFullscreen = true;
		}
		else
		{
			_fullscreenModeActivator.isOn = bool.Parse(PlayerPrefs.GetString(fullscreenKey)); 
		}
	}


    public void _saveSoundsSet()
    {
        PlayerPrefs.SetFloat(soundsKey, _sounds.value);
    }

	public void SaveScreenResolutionSet()
	{
		switch (_screenResolution.value)
		{
			case 0:
				Screen.SetResolution(1024, 768, _isFullscreen);
				PlayerPrefs.SetInt(resolutionKey, _screenResolution.value);
				break;
			case 1:
				Screen.SetResolution(1280, 720, _isFullscreen);
				PlayerPrefs.SetInt(resolutionKey, _screenResolution.value);
				break;
			case 2:
				Screen.SetResolution(1366, 768, _isFullscreen);
				PlayerPrefs.SetInt(resolutionKey, _screenResolution.value);
				break;
			case 3:
				Screen.SetResolution(2560, 1440, _isFullscreen);
				PlayerPrefs.SetInt(resolutionKey, _screenResolution.value);
				break;
			case 4:
				Screen.SetResolution(3840, 2160, _isFullscreen);
				PlayerPrefs.SetInt(resolutionKey, _screenResolution.value);
				break;
		}
		Debug.Log("Saved");
	}

	public void SaveGraphicsQuakitySet()
	{
		switch (_graphicsQuality.value)
		{
			case 0:
				QualitySettings.SetQualityLevel(0);
				PlayerPrefs.SetInt(graphicsKey, _graphicsQuality.value);
				break;
			case 1:
				QualitySettings.SetQualityLevel(1);
				PlayerPrefs.SetInt(graphicsKey, _graphicsQuality.value);
				break;
			case 2:
				QualitySettings.SetQualityLevel(2);
				PlayerPrefs.SetInt(graphicsKey, _graphicsQuality.value);
				break;
		}
		Debug.Log("Saved");
	}

	public void FullscreenSet()
	{
		PlayerPrefs.SetString(fullscreenKey, Convert.ToString(_fullscreenModeActivator.isOn));
		_isFullscreen = bool.Parse(PlayerPrefs.GetString(fullscreenKey));
		SaveScreenResolutionSet();
	}

	public void _ResetAllSettings()
    {
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat(soundsKey, 0.5f);
		PlayerPrefs.SetInt(resolutionKey, 2);
		PlayerPrefs.SetInt(graphicsKey, 1);
		PlayerPrefs.SetString(fullscreenKey, "true");

		_sounds.value = 0.5f;
		_screenResolution.value = 2;
		_graphicsQuality.value = 1;
		_fullscreenModeActivator.isOn = true;

		Screen.SetResolution(1366, 768, _isFullscreen);
    }

	private void Update()
	{
		Debug.Log("Sounds:" + PlayerPrefs.GetFloat(soundsKey));
		Debug.Log("Resolution:" + PlayerPrefs.GetInt(resolutionKey));
		Debug.Log("Graphics:" + PlayerPrefs.GetInt(graphicsKey));
		Debug.Log("Fullscreen:" + PlayerPrefs.GetString(fullscreenKey));
		Debug.Log(QualitySettings.GetQualityLevel());
	}
}
