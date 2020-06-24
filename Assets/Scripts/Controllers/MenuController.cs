using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    public GeneralPanelController aboutController;
    [SerializeField]
    public GeneralPanelController howToPlayController;
    [SerializeField]
    public SettingsController settingsController;
    [SerializeField]
    private Button buttonStart;
    [SerializeField]
    private Button buttonAbout;
    [SerializeField]
    private Button buttonHowToPlay;
    [SerializeField]
    private Button buttonSettings;
    [SerializeField]
    private Button buttonQuit;

    private void Awake()
    {
        aboutController.disableCanvas();
        howToPlayController.disableCanvas();
        settingsController.disableCanvas();
    }

    public void onButtonStartClick()
    {
        Debug.Log("Opening MainGame");
        SceneManager.LoadScene("MainGame");
         
    } 
    public void onButtonAboutClick()
    {
        aboutController.enableCanvas();
    } 
    public void onButtonHowToPlayClick()
    {
        howToPlayController.enableCanvas();
    } 
    public void onButtonSettingsClick()
    {
        settingsController.enableCanvas();
    } 
    public void onButtonQuitClick()
    {
        Debug.Log("Quitting");
        Application.Quit();
    } 
}
