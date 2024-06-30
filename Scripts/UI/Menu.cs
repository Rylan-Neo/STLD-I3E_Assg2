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
    // Reference to player because some things require player data and input
    public Player player;

    // Audio manager refernce
    public AudioManager audioManager;

    // Setting and credits for the starting menu
    public GameObject settings;
    public GameObject credits; 

    // Volume sliders
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sFXSlider;

    // Boolean to check if game paused
    public static bool gamePaused = false;

    // Different game menus
    public GameObject pauseMenu;
    public GameObject quitMenu;
    public GameObject controlMenu;
    public GameObject deathScreen;
    public GameObject endCard;
    public GameManager gameManager;

    // Uses the scene value to change the BGM
    public int sceneValue;

    // For the boss scene
    bool startMusic = false;
    bool bossStart = true;

    // The sliders audio sources use Audio mixer
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

    // the game ends when this opens
    public void EndCard()
    {
        endCard.SetActive(!endCard.activeSelf);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        gamePaused = true;
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

    // Closes the pause menu and unfreezes the game
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
        endCard.SetActive(false);

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
            if (startMusic) 
            {
                audioManager.PlayBGM(audioManager.bgm3);
            }
            else
            {
                audioManager.PlayBGM(audioManager.bgm2);
                startMusic = true;
            }
        }
        else if (sceneValue == 2)
        {
            audioManager.PlayBGM(audioManager.bgm2);
        }
    }

    // Starts the boss BGM, has a reverse functionf or when after the boss dies and the core is collected
    public void BossFightBGM()
    {
        if (bossStart)
        { 
            audioManager.PlayBGM(audioManager.bgm4);
            bossStart = false;
        }
        else
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

    // Allows teh slider to adjust the actual BGM volume
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("musicVol", MathF.Log10(volume)*20);
    }

    // Allows the slider to adjust the actual SFX volume
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

    // Saves player preferences
    private void Save()
    {
        PlayerPrefs.SetFloat("Mastervolume", masterSlider.value);
        PlayerPrefs.SetFloat("BGMvolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXvolume", sFXSlider.value);
    }

    // Supposed to restart the scene upon player death. It also heals the player
    public void RestartScene()
    {
        deathScreen.SetActive(false);
        Destroy(gameManager);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        gamePaused = false;
        player.UpdatePause();
        player.Damage(-500);
    }
}
