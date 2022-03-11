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
    [Header(" ")]

    [Header("Code Check")]
    [SerializeField] bool MenuActive;
    [SerializeField] bool SettingsActive;
    [SerializeField] bool AudioActive;
    [SerializeField] bool VideoActive;
    [SerializeField] int MenuLayer = 0;


    void Awake()
    {
        MenuActive = true;
        SettingsActive = false;
        AudioActive = false;
        VideoActive = false;

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

        if (MenuActive && !SettingsActive && !AudioActive && !VideoActive)
        {
            Canvas_MainMenu.SetActive(true);
            Canvas_AudioMenu.SetActive(false);
            Canvas_VideoMenu.SetActive(false);
            Canvas_SettingsMenu.SetActive(false);
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

        if (SettingsActive && !MenuActive && !AudioActive && !VideoActive)
        {
            Canvas_SettingsMenu.SetActive(true);
            Canvas_AudioMenu.SetActive(false);
            Canvas_VideoMenu.SetActive(false);
            Canvas_MainMenu.SetActive(false);
        }
    }

    public void Video()
    {
        MenuLayer = 2;
        VideoActive = true;
        SettingsActive = false;
        AudioActive = false;
        MenuActive = false;

        if (VideoActive && !MenuActive && !AudioActive && !SettingsActive)
        {
            Canvas_VideoMenu.SetActive(true);
            Canvas_AudioMenu.SetActive(false);
            Canvas_SettingsMenu.SetActive(false);
            Canvas_MainMenu.SetActive(false);
        }
    }

    public void Audio()
    {
        MenuLayer = 2;
        AudioActive = true;
        SettingsActive = false;
        VideoActive = false;
        MenuActive = false;

        if (AudioActive && !MenuActive && !VideoActive && !SettingsActive)
        {
            Canvas_AudioMenu.SetActive(true);
            Canvas_VideoMenu.SetActive(false);
            Canvas_SettingsMenu.SetActive(false);
            Canvas_MainMenu.SetActive(false);
        }
    }

    public void Controls()
    {
        Debug.Log("Opening Controls menu!");
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
