﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Text Loaders")]
    [SerializeField]
    private TextLoader aboutTextLoader;
    [SerializeField]
    private TextLoader howToPlayTextLoader;
    [Header("Panel Controller")]

    [SerializeField]
    public GeneralPanelController aboutController;
    [SerializeField]
    public GeneralPanelController howToPlayController;
    [SerializeField]
    public SettingsController settingsController;
    [SerializeField]
    [Header("Buttons")]
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
        StartCoroutine(LoadSceneASync("MainGame"));
    } 
    public void onButtonAboutClick()
    {
        aboutController.enableCanvas();
        aboutTextLoader.LoadText();
    } 
    public void onButtonHowToPlayClick()
    {
        howToPlayController.enableCanvas();
        howToPlayTextLoader.LoadText();
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

    IEnumerator LoadSceneASync(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
