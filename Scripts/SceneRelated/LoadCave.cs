/*
* Author: Rylan Neo
* Date of creation: 20th June 2024
* Description: Code for loading into the next scene and changing BGM
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCave : Collectible
{
    // Connects to both audio manager and menu because of changing scene index and playing sound
    public AudioManager audioManager;
    public Menu menu;
    public bool enteredCave = false;

    // i forgot if this goes unused after 2 days of trying to get everything to function smoothly
    public void MenuSwap()
    {
        enteredCave = false;
    }

    /// <summary>
    /// the scene swapper is udner collectible script because it is more convenient to do taht to allow raytace text to appear
    /// </summary>
    /// <param name="player"></param>
    public override void Collected(Player player)
    {
        Debug.Log("Scene swapping... ");
        Debug.Log(enteredCave);
        base.Collected(player);

        // From start scene to cave scene
        if (enteredCave == false)
        {
            SceneManager.LoadScene(2);
            enteredCave = true;
            menu.SceneBGM();
        }

        // vise versa
        else
        {
            SceneManager.LoadScene(1);
            enteredCave = false;
            menu.SceneBGM();
        }
        audioManager.PlaySFX(audioManager.enterCave);
    }

    // On start, find menu and audio manager
    private void Start()
    {
        Debug.Log(enteredCave);
        menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
}
