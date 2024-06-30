/*
* Author: Rylan Neo
* Date of creation: 18th June 2024
* Description: Script for the health drugs that player will be using to heal themselves
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrug : Collectible
{
    /// <summary>
    /// Linked to player script
    /// </summary>
    public Player playerScript;
    // Enable the drug icon for the first time after the drug is collected for the first time
    public bool drugsEnabled = false;
    public AudioManager audioManager;

    /// <summary>
    /// On awake, find the player script
    /// </summary>
    private void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Destroy object after collected
    void GetCollected()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Player picks up the drug. If it is the first time, enables the healing icon
    /// </summary>
    /// <param name="player"></param>
    public override void Collected(Player player)
    {
        Debug.Log("Drug picked up");
        base.Collected(player);
        GetCollected();
        // being lazy, just use same function for use and add
        playerScript.UseDrug(-1);
        // Plays collectible audio
        audioManager.PlaySFX(audioManager.itemPickUp);
        if (!drugsEnabled) 
        {
            drugsEnabled=true;
            playerScript.GetComponent<Player>().EnableHealing();
        }
    }
}
