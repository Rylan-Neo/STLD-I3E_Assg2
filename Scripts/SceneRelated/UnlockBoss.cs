/*
* Author: Rylan Neo
* Date of creation: 29th June 2024
* Description: Code changing BGM and recognising that the boss fight has started
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockBoss : Collectible
{
    // Connects to menu and player
    public Menu menu;
    public Player player;

    // Key unlocks the gate
    public GameObject gate;
    public GameObject key;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>();
        key.SetActive(false);
    }

    // When the altar is "collected", places the key in if the player has it and starts the boss fight
    public override void Collected(Player player)
    {
        Debug.Log("Altar activated up");
        base.Collected(player);

        // Needs to recognise that player has a key to unock the boss
        if (player.keyPickedUp == true)
        {
            gate.SetActive(false);
            key.SetActive(true);
            menu.BossFightBGM();
            Debug.Log("Boss fight engaged");
        }
    }
}
