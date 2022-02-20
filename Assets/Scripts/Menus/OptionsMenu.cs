using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [Header("Canvas Setup")]
    [SerializeField] GameObject Canvas_MainMenu;
    [SerializeField] GameObject Canvas_SettingsMenu;
    /*[SerializeField] GameObject Canvas_GraphicsMenu;
    [SerializeField] GameObject Canvas_SoundMenu;
    [SerializeField] GameObject Canvas_ControlsMenu;*/
    [Header(" ")]

    [Header("Code Check")]
    [SerializeField] bool MenuActive;
    [SerializeField] bool SettingsActive;


    // Start is called before the first frame update
    void Start()
    {
        MenuActive = false;
        SettingsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        KeyCheck();
        MenuChecker();
    }

    void KeyCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !MenuActive)
        {
            Options();
            Debug.Log("ESC Key pressed!");
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && MenuActive)
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
    }

    public void Resume()
    {
        Debug.Log("Resuming game!");
    }

    public void Play()
    {
        Debug.Log("Starting game!");
    }

    public void Options()
    {
        Debug.Log("Opening options menu!");
        MenuActive = true;
        SettingsActive = false;
    }

    public void Settings()
    {
        Debug.Log("Opening settings menu!");
        MenuActive = false;
        SettingsActive = true;
    }

    public void Video()
    {
        Debug.Log("Opening Video Settings menu!");
    }

    public void Sound()
    {
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
        Debug.Log("Quitting to title!");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game!");
        Application.Quit();
    }
}
