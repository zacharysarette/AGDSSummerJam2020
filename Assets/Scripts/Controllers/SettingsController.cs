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
        AudioManager.Instance.SetSfxVolume(musicSlider.value);
        AudioManager.Instance.SetDefaultSfxVolume(musicSlider.value);
        sfxVolumeValueText.text = sfxSlider.value.ToString("0.00");
    }

    public void UpdateMusic()
    {

        AudioManager.Instance.SetMusicVolume(musicSlider.value);
        AudioManager.Instance.SetDefaultMusicVolume(musicSlider.value);
        musicVolumeValueText.text = musicSlider.value.ToString("0.00");
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
