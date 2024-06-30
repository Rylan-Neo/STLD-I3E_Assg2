/*
* Author: Rylan Neo
* Date of creation: 20th June 2024
* Description: Breakable box
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : Collectible
{
    /// <summary>
    /// connects to item drop script upon collection
    /// </summary>
    public ItemDrop itemDrop;
    public AudioManager audioManager;

    // Starts by finding audio manager
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    /// <summary>
    /// Box is collected, plays sound
    /// </summary>
    /// <param name="player"></param>
    public override void Collected(Player player)
    {
        Debug.Log("Box picked up");
        base.Collected(player);
        audioManager.PlaySFX(audioManager.boxBreak);
        // Asks item script to drop whatever it is holding
        itemDrop.DestroyShell();
    }
}
