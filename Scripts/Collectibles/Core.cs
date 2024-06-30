/*
* Author: Rylan Neo
* Date of creation: 22nd June 2024
* Description: Script for Core collectible
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : Collectible
{
    //Needs to reference player script and menu to store info
    public Menu menu;
    public Player playerScript;
    public AudioManager audioManager;
    private void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Destroys object once collected
    void GetCollected()
    {
        Destroy(gameObject);
    }

    // Pick up the collectible
    public override void Collected(Player player)
    {
        Debug.Log("Core picked up");
        base.Collected(player);
        GetCollected();
        // Plays collectible audio
        audioManager.PlaySFX(audioManager.itemPickUp);
        // Tells player script to add 1 to the count
        playerScript.AddCore(1);
        // Stops boss fight because boss is dead
        menu.BossFightBGM();
    }
}