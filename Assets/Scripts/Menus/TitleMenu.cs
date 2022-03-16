using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    [Header("Canvas Setup")]
    [SerializeField] GameObject Canvas_MainMenu;
    [SerializeField] GameObject Canvas_SettingsMenu;
    [SerializeField] GameObject Canvas_VideoMenu;
    [SerializeField] GameObject Canvas_AudioMenu;
    [SerializeField] GameObject Canvas_ControlsMenu;

    [Header(" ")]

    [Header("Code Check")]
    [SerializeField] bool MenuActive;
    [SerializeField] bool SettingsActive;
    [SerializeField] bool AudioActive;
    [SerializeField] bool VideoActive;
    [SerializeField] bool ControlsActive;
    [SerializeField] int MenuLayer = 0;


    void Awake()
    {
        MenuActive = true;
        SettingsActive = false;
        AudioActive = false;
        VideoActive = false;
        ControlsActive = false;


        MenuLayer = 0;
    }

    void Update()
    {
        KeyCheck();
    }

    void KeyCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && MenuLayer == 0) Settings();
        else if (Input.GetKeyDown(KeyCode.Escape) && MenuLayer == 1) Resume();
        if (Input.GetKeyDown(KeyCode.Escape) && MenuLayer == 2) Settings();
    }

    public void Resume()
    {
        MenuLayer = 0;
        MenuActive = true;
        SettingsActive = false;
        AudioActive = false;
        VideoActive = false;
        ControlsActive = false;

        if (MenuActive && !SettingsActive && !AudioActive && !VideoActive && !ControlsActive)
        {
            Canvas_MainMenu.SetActive(true);
            Canvas_AudioMenu.SetActive(false);
            Canvas_VideoMenu.SetActive(false);
            Canvas_SettingsMenu.SetActive(false);
            Canvas_ControlsMenu.SetActive(false);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        MenuLayer = 1;
        SettingsActive = true;
        AudioActive = false;
        VideoActive = false;
        MenuActive = false;
        ControlsActive = false;

        if (SettingsActive && !MenuActive && !AudioActive && !VideoActive && !ControlsActive)
        {
            Canvas_SettingsMenu.SetActive(true);
            Canvas_AudioMenu.SetActive(false);
            Canvas_VideoMenu.SetActive(false);
            Canvas_MainMenu.SetActive(false);
            Canvas_ControlsMenu.SetActive(false);
        }
    }

    public void Video()
    {
        MenuLayer = 2;
        VideoActive = true;
        SettingsActive = false;
        AudioActive = false;
        MenuActive = false;
        ControlsActive = false;

        if (VideoActive && !MenuActive && !AudioActive && !SettingsActive && !ControlsActive)
        {
            Canvas_VideoMenu.SetActive(true);
            Canvas_AudioMenu.SetActive(false);
            Canvas_SettingsMenu.SetActive(false);
            Canvas_MainMenu.SetActive(false);
            Canvas_ControlsMenu.SetActive(false);
        }
    }

    public void Audio()
    {
        MenuLayer = 2;
        AudioActive = true;
        SettingsActive = false;
        VideoActive = false;
        MenuActive = false;
        ControlsActive = false;

        if (AudioActive && !MenuActive && !VideoActive && !SettingsActive && !ControlsActive)
        {
            Canvas_AudioMenu.SetActive(true);
            Canvas_VideoMenu.SetActive(false);
            Canvas_SettingsMenu.SetActive(false);
            Canvas_MainMenu.SetActive(false);
            Canvas_ControlsMenu.SetActive(false);
        }
    }

    public void Controls()
    {
        MenuLayer = 3;
        ControlsActive = true;
        AudioActive = false;
        SettingsActive = false;
        VideoActive = false;
        MenuActive = false;

        if (ControlsActive && !AudioActive && !MenuActive && !VideoActive && !SettingsActive)
        {
            Canvas_ControlsMenu.SetActive(true);
            Canvas_AudioMenu.SetActive(false);
            Canvas_VideoMenu.SetActive(false);
            Canvas_SettingsMenu.SetActive(false);
            Canvas_MainMenu.SetActive(false);
        }
    }

    public void Mute()
    {
        Debug.Log("Muting game!");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
