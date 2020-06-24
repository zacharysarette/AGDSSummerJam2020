using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
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

    public void onButtonStartClick()
    {
        Debug.Log("Opening MainGame");
        SceneManager.LoadScene("MainGame");
         
    } 
    public void onButtonAboutClick()
    {
        Debug.Log("Opening About");
         
    } 
    public void onButtonHowToPlayClick()
    {
        Debug.Log("Opening HowToPlay");
         
    } 
    public void onButtonSettingsClick()
    {
        Debug.Log("Opening Settings");
    } 
    public void onButtonQuitClick()
    {
        Debug.Log("Quitting");
        Application.Quit();
    } 
}
