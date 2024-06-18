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
    public void CreditsMenu()
    {
        credits.SetActive(!credits.activeSelf);
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
    private void Awake()
    {
        settings.SetActive(false);
        credits.SetActive(false);
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
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Mastervolume");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("Mastervolume", volumeSlider.value);
    }
}
