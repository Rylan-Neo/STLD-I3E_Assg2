/*
* Author: Rylan Neo
* Date of creation: 22nd June 2024
* Description: Script for Gold collectible
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Collectible
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
        Debug.Log("Gold picked up");
        base.Collected(player);
        GetCollected();
        // Plays collectible audio
        audioManager.PlaySFX(audioManager.itemPickUp);
        // Tells player script to add 1 to the count
        playerScript.AddGold(1);
    }
}
