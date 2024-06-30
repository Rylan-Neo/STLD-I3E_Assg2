/*
* Author: Rylan Neo
* Date of creation: 30th June 2024
* Description: Enable end card
*/
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class EndGame : Collectible
{
    public Menu menu;
    public Player player;

    // The key to end the game
    public GameObject key;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>();
        key.SetActive(false);
    }

    // When collected, sets the endcard active and the game ended
    public override void Collected(Player player)
    {
        Debug.Log("Altar activated up");
        base.Collected(player);

        // Recognises that the ship core has been returned
        if (player.shipCore >= 1)
        {            
            key.SetActive(true);
            menu.EndCard();
        }
    }
}
