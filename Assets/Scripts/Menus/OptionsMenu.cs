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


    [SerializeField] GameObject BuildMenuHolder;
    [SerializeField] GameObject CameraHolder;

    BuildMenu BuildMenu;
    CameraLook look;
    CameraMove move;

    void Awake()
    {
        MenuActive = false;
        SettingsActive = false;
        AudioActive = false;
        VideoActive = false;
        ControlsActive = false;

        MenuLayer = 0;

        BuildMenu = BuildMenuHolder.GetComponent<BuildMenu>();
        look = CameraHolder.GetComponent<CameraLook>();
        move = CameraHolder.GetComponent<CameraMove>();
    }

    void Update()
    {
        KeyCheck();
        CursorUnlocker();
    }

    void KeyCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && MenuLayer == 0) Pause();
        else if (Input.GetKeyDown(KeyCode.Escape) && MenuLayer == 1) Resume();
        else if (Input.GetKeyDown(KeyCode.Escape) && MenuLayer == 2) Pause();
        else if (Input.GetKeyDown(KeyCode.Escape) && MenuLayer == 3) Settings();
    }

    void CursorUnlocker()
    {
        if (MenuActive || SettingsActive || VideoActive || AudioActive || ControlsActive || BuildMenu.BuildingMenu)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            look.enabled = false;
            move.enabled = false;
        }

        if (!MenuActive && !SettingsActive && !VideoActive && !AudioActive && !ControlsActive && !BuildMenu.BuildingMenu)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;

            look.enabled = true;
            move.enabled = true;
        }
    }

    void Pauser()
    {
        if (MenuActive || SettingsActive || VideoActive || AudioActive || ControlsActive)
        {
            Time.timeScale = 0;
        }
        else if (!MenuActive && !SettingsActive && !VideoActive && !AudioActive && !ControlsActive)
        {
            Time.timeScale = 1;
        }
    }

    public void Pause()
    {
        Debug.Log("Layer 0");
        MenuLayer = 1;
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
            Canvas_ControlsMenu.SetActive(false);
            Canvas_SettingsMenu.SetActive(false);
        }

        Pauser();
    }

    public void Resume()
    {
        Debug.Log("Layer 0");
        MenuLayer = 0;
        MenuActive = false;
        SettingsActive = false;
        AudioActive = false;
        VideoActive = false;
        ControlsActive = false;
        
        Canvas_MainMenu.SetActive(false);
        Canvas_AudioMenu.SetActive(false);
        Canvas_VideoMenu.SetActive(false);
        Canvas_ControlsMenu.SetActive(false);
        Canvas_SettingsMenu.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        Debug.Log("Layer 1");
        MenuLayer = 2;
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
            Canvas_ControlsMenu.SetActive(false);
            Canvas_MainMenu.SetActive(false);
        }
    }

    public void Video()
    {
        Debug.Log("Layer 2");
        MenuLayer = 3;
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
            Canvas_ControlsMenu.SetActive(false);
            Canvas_MainMenu.SetActive(false);
        }
    }

    public void Audio()
    {
        Debug.Log("Layer 2");
        MenuLayer = 3;
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
            Canvas_ControlsMenu.SetActive(false);
            Canvas_MainMenu.SetActive(false);
        }
    }

    public void Controls()
    {
        Debug.Log("Layer 2");
        MenuLayer = 3;
        ControlsActive = true;
        AudioActive = false;
        SettingsActive = false;
        VideoActive = false;
        MenuActive = false;

        if (ControlsActive && !AudioActive && !MenuActive && !VideoActive && !SettingsActive)
        {
            Canvas_ControlsMenu.SetActive(true);
            Canvas_AudioMenu.SetActive(true);
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
        SceneManager.LoadScene(0);
    }
}