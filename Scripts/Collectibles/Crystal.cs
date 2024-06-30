/*
* Author: Rylan Neo
* Date of creation: 22nd June 2024
* Description: Script for crystal collectible
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Crystal : Collectible
{
    //Needs to reference player script to store info
    public Player playerScript;
    public AudioManager audioManager;
    private void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
        Debug.Log("Crystal picked up");
        base.Collected(player);
        GetCollected();
        // Plays collectible audio
        audioManager.PlaySFX(audioManager.itemPickUp);
        // Tells player script to add 1 to the count
        playerScript.AddCrystal(1);
    }
}
