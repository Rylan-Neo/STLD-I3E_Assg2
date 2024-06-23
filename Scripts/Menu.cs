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

public class Menu : MonoBehaviour
{
    public GameObject settings;
    public GameObject credits; 
    public AudioSource button;
    public Slider volumeSlider;
    public static bool gamePaused = false;
    public GameObject pauseMenu;
    public GameObject quitMenu;
    public GameObject controlMenu;
    public GameObject deathScreen;
    public LoadCave inCave;
    public void PlaySound()
    {
        button.Play();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(0);
    }
    public void SettingMenu()
    {
        settings.SetActive(!settings.activeSelf);
    }
    public void ControlsMenu()
    {
        controlMenu.SetActive(!controlMenu.activeSelf);
    }
    public void CreditsMenu()
    {
        credits.SetActive(!credits.activeSelf);
    }
    public void QuitMenu()
    {
        quitMenu.SetActive(!quitMenu.activeSelf);
    }
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }    
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        gamePaused = true;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        gamePaused = false;
    }
    public void MenuLoad()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
        gamePaused = false;
        inCave.GetComponent<LoadCave>().MenuSwap();
    }
    private void Awake()
    {
        settings.SetActive(false);
        credits.SetActive(false);
        pauseMenu.SetActive(false);
        quitMenu.SetActive(false);
        deathScreen.SetActive(false);
        if (!PlayerPrefs.HasKey("Mastervolume"))
        {
            PlayerPrefs.SetFloat("Mastervolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }
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
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Mastervolume");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("Mastervolume", volumeSlider.value);
    }
}
