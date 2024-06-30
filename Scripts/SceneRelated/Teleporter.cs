/*
* Author: Rylan Neo
* Date of creation: 28th June 2024
* Description: Teleporter
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : Collectible
{
    public AudioManager audioManager;
    public Transform target;
    bool teleport = false;

    // Teleports player yeah.
    private void OnTriggerStay(Collider other)
    {
        if (teleport)
        {
            other.transform.position = target.transform.position;
            teleport = false;
        }
    }

    // Needs to be collected to play sound
    public override void Collected(Player player)
    {
        Debug.Log("Teleporting ");
        base.Collected(player);
        audioManager.PlaySFX(audioManager.teleport);
        teleport = true;
    }
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
}
