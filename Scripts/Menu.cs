/*
* Author: Rylan Neo
* Date of creation: 12th June 2024
* Description: All code controlling menu functions.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class Menu : MonoBehaviour
{
    public Player player;
    public GameObject settings;
    public GameObject credits; 
    public AudioManager audioManager;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sFXSlider;
    public static bool gamePaused = false;
    public GameObject pauseMenu;
    public GameObject quitMenu;
    public GameObject controlMenu;
    public GameObject deathScreen;
    public int sceneValue;

    [SerializeField] private AudioMixer audioMixer;

    // Plays a sound when a button is pressed
    public void PlaySound()
    {
        audioManager.PlaySFX(audioManager.click);
    }

    // Loads into the main game
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        player.SceneChange(1);
        SceneBGM();
    }

    // Opens up the setting menu
    public void SettingMenu()
    {
        settings.SetActive(!settings.activeSelf);
    }

    // Opens up the controls menu
    public void ControlsMenu()
    {
        controlMenu.SetActive(!controlMenu.activeSelf);
    }

    // Opens up the credits menu
    public void CreditsMenu()
    {
        credits.SetActive(!credits.activeSelf);
    }

    // Opens up the quit confirmation menu
    public void QuitMenu()
    {
        quitMenu.SetActive(!quitMenu.activeSelf);
    }

    // Changes the audio slider value and runs the save function to ensure that player preferences are saved
    public void ChangeVolume()
    {
        AudioListener.volume = masterSlider.value;
        Save();
    }    

    // Quits the build
    public void QuitGame()
    {
        Application.Quit();
    }

    // Opens up the pause menu and freezes the game
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        gamePaused = true;
    }

    // Closes the pause meny and unfreezes the game
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        gamePaused = false;
        player.UpdatePause();
    }

    // Returns the player back to the main menu
    public void MenuLoad()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
        gamePaused = false;
        player.UpdatePause();

        // SceneChanger revert
        player.SceneChange(0);
    }

    // Player has been crabbed
    public void DeathScreen()
    {
        deathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        gamePaused = true;
        Debug.Log("Player has died ahhhhh");
    }
    private void Start()
    {
        SceneBGM();
    }
    private void Awake()
    {
        // Set all the different menus inactive
        settings.SetActive(false);
        credits.SetActive(false);
        pauseMenu.SetActive(false);
        quitMenu.SetActive(false);
        controlMenu.SetActive(false);
        deathScreen.SetActive(false);

        //AudioManager retrieval
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        // Checks if player has data on their volume settings
        if (!PlayerPrefs.HasKey("Mastervolume"))
        {
            PlayerPrefs.SetFloat("Mastervolume", 1);
            Load();
        }
        else
        {
            Load();
        }
        if (!PlayerPrefs.HasKey("BGMvolume"))
        {
            PlayerPrefs.SetFloat("BGMvolume", 1);
            Load();
        }
        else
        {
            Load();
        }
        if (!PlayerPrefs.HasKey("SFXvolume"))
        {
            PlayerPrefs.SetFloat("SFXvolume", 1);
            Load();
        }
        else
        {
            Load();
        }

        // Make sure the sliders are compatible with the audios
        SetMusicVolume();
        SetSFXVolume();
    }

    // BGM selection using scene index sort of
    public void SceneBGM()
    {
        sceneValue = SceneManager.GetActiveScene().buildIndex;
        if (sceneValue == 0)
        {
            audioManager.PlayBGM(audioManager.bgm1);
        }
        else if (sceneValue == 1)
        {
            audioManager.PlayBGM(audioManager.bgm2);
        }
        else if (sceneValue == 2)
        {
            audioManager.PlayBGM(audioManager.bgm3);
        }
    }

    // Opens up the menu
    public void MenuUI()
    {
        if(gamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
        Debug.Log("Pause working");
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("musicVol", MathF.Log10(volume)*20);
    }

    public void SetSFXVolume()
    {
        float volume = sFXSlider.value;
        audioMixer.SetFloat("sFXVol", MathF.Log10(volume) * 20);
    }

    // Loads player data on their preferences for volume sliders
    private void Load()
    {
        masterSlider.value = PlayerPrefs.GetFloat("Mastervolume");
        musicSlider.value = PlayerPrefs.GetFloat("BGMvolume");
        sFXSlider.value = PlayerPrefs.GetFloat("SFXvolume");

    }
    private void Save()
    {
        PlayerPrefs.SetFloat("Mastervolume", masterSlider.value);
        PlayerPrefs.SetFloat("BGMvolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXvolume", sFXSlider.value);
    }
    public void RestartScene()
    {
        deathScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        gamePaused = false;
        player.UpdatePause();
    }
}
