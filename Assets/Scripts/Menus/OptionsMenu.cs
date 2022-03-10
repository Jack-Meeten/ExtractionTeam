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
    [Header(" ")]

    [Header("Code Check")]
    [SerializeField] bool MenuActive;
    [SerializeField] bool SettingsActive;
    [SerializeField] bool SoundsActive;
    [SerializeField] bool VideoActive;
    [SerializeField] bool MainMenu;
    [Header(" ")]

    [SerializeField] GameObject BuildMenuHolder;
    [SerializeField] GameObject CameraHolder;

    BuildMenu BuildMenu;
    CameraLook look;
    CameraMove move;

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

        BuildMenu = BuildMenuHolder.GetComponent<BuildMenu>();
        look = CameraHolder.GetComponent<CameraLook>();
        move = CameraHolder.GetComponent<CameraMove>();
    }

    void Update()
    {
        KeyCheck();
        MenuChecker();;
    }

     

    void KeyCheck()
    {
        // Main menu key check

        if (Input.GetKeyDown(KeyCode.Escape) && !MenuActive && MainMenu)
        {
            Options();
        }

        // Game scene key check

        if (Input.GetKeyDown(KeyCode.Escape) && !MenuActive && !MainMenu)
        {
            Options();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && MenuActive && !MainMenu)
        {
            Exit();
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

        // Cursor unlocker
        if (MenuActive || SettingsActive || VideoActive || SoundsActive || BuildMenu.BuildingMenu)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            look.enabled = false;
            move.enabled = false;
        }

        if (!MenuActive && !SettingsActive && !VideoActive && !SoundsActive && !BuildMenu.BuildingMenu)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;

            look.enabled = true;
            move.enabled = true;
        }
    }

    public void Resume()
    {
        MenuActive = false;
        SettingsActive = false;
        SoundsActive = false;
        VideoActive = false;
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        MenuActive = true;
        SettingsActive = false;
        SoundsActive = false;
        VideoActive = false;
    }

    public void Settings()
    {
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
    }

    public void Sound()
    {
        SoundsActive = true;
        SettingsActive = false;
        MenuActive = false;
        VideoActive = false;
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
        MenuActive = false;       
    }

    public void QuitToTitle()
    {
        SceneManager.LoadScene(0);       
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
