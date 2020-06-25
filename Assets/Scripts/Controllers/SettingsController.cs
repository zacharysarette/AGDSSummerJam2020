using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [SerializeField]
    private Canvas settingsMenuCanvas;
    [SerializeField]
    private TextMeshProUGUI sfxVolumeValueText; 
    [SerializeField]
    private TextMeshProUGUI musicVolumeValueText; 
    [SerializeField]
    private  Slider sfxSlider; 
    [SerializeField]
    private  Slider musicSlider; 

    private void Start()
    {
       sfxSlider.value = AudioManager.Instance.GetDefaultSfxVolume(); 
       musicSlider.value = AudioManager.Instance.GetDefaultMusicVolume();
    }

    public void UpdateSfx()
    {
        float sfxSliderValue = sfxSlider.value;
        AudioManager.Instance.SetSfxVolume(sfxSliderValue);
        sfxVolumeValueText.text = sfxSliderValue.ToString("0.00");
    }

    public void UpdateMusic()
    {
        float musicSliderValue = musicSlider.value;
        AudioManager.Instance.SetMusicVolume(musicSliderValue);
        musicVolumeValueText.text = musicSliderValue.ToString("0.00");
    }

    public void onBackButtonClicked()
    {
        disableCanvas();
    }

    public void disableCanvas()
    {
        settingsMenuCanvas.enabled = !enabled;
    }

    public void enableCanvas()
    {
        settingsMenuCanvas.enabled = enabled;
    }
}
