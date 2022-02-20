using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    [Header("Canvas Setup")]
    [SerializeField] GameObject Canvas_MainMenu;
    [SerializeField] GameObject Canvas_SettingsMenu;
    [SerializeField] GameObject Canvas_VideoMenu;
    [SerializeField] GameObject Canvas_SoundsMenu;
    //[SerializeField] GameObject Canvas_ControlsMenu;
    [Header(" ")]

    [Header("Code Check")]
    [SerializeField] bool MenuActive;
    [SerializeField] bool SettingsActive;
    [SerializeField] bool SoundsActive;
    [SerializeField] bool VideoActive;
    [SerializeField] bool MainMenu;


    void Awake()
    {
        if (MainMenu)
        {
            MenuActive = true;
            SettingsActive = false;
            VideoActive = false;
            SoundsActive = false;
        }

        if (!MainMenu)
        {
            MenuActive = false;
            SettingsActive = false;
            VideoActive = false;
            SoundsActive = false;
        }
    }

    void Update()
    {
        KeyCheck();
        MenuChecker();
    }

    void KeyCheck()
    {
        // Main menu key check

        if (Input.GetKeyDown(KeyCode.Escape) && !MenuActive && MainMenu)
        {
            Options();
            Debug.Log("ESC Key pressed!");
        }

        // Game scene key check

        if (Input.GetKeyDown(KeyCode.Escape) && !MenuActive && !MainMenu)
        {
            Options();
            Debug.Log("ESC Key pressed!");
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && MenuActive && !MainMenu)
        {
            Exit();
            Debug.Log("ESC Key pressed twice!");
        }
    }

    void MenuChecker()
    {
        // Menu checker
        if (MenuActive)
        {
            Canvas_MainMenu.SetActive(true);
        }
        if (!MenuActive)
        {
            Canvas_MainMenu.SetActive(false);
        }

        // Settings checker
        if (SettingsActive)
        {
            Canvas_SettingsMenu.SetActive(true);
        }
        if (!SettingsActive)
        {
            Canvas_SettingsMenu.SetActive(false);
        }

        // Video checker
        if (VideoActive)
        {
            Canvas_VideoMenu.SetActive(true);
        }
        if (!VideoActive)
        {
            Canvas_VideoMenu.SetActive(false);
        }

        // Sound checker
        if (SoundsActive)
        {
            Canvas_SoundsMenu.SetActive(true);
        }
        if (!SoundsActive)
        {
            Canvas_SoundsMenu.SetActive(false);
        }
    }

    public void Resume()
    {
        MenuActive = false;
        SettingsActive = false;
        SoundsActive = false;
        VideoActive = false;
        Debug.Log("Resuming game!");
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Starting game!");
    }

    public void Options()
    {
        Debug.Log("Opening options menu!");
        MenuActive = true;
        SettingsActive = false;
        SoundsActive = false;
        VideoActive = false;
    }

    public void Settings()
    {
        Debug.Log("Opening settings menu!");
        SettingsActive = true;
        MenuActive = false;
        SoundsActive = false;
        VideoActive = false;
    }

    public void Video()
    {
        VideoActive = true;
        SettingsActive = false;
        MenuActive = false;
        SoundsActive = false;
        Debug.Log("Opening Video Settings menu!");
    }

    public void Sound()
    {
        SoundsActive = true;
        SettingsActive = false;
        MenuActive = false;
        VideoActive = false;
        Debug.Log("Opening Sound Effects menu!");
    }

    public void Controls()
    {
        Debug.Log("Opening Controls menu!");
    }

    public void Mute()
    {
        Debug.Log("Muting game!");
    }

    public void Exit()
    {
        Debug.Log("Exiting menu!");
        MenuActive = false;       
    }

    public void QuitToTitle()
    {
        SceneManager.LoadScene(0);       
        Debug.Log("Quitting to title!");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game!");
        Application.Quit();
    }
}
