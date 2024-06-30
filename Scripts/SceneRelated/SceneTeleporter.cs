/*
* Author: Rylan Neo
* Date of creation: 28th June 2024
* Description: Scene Teleporter, does shady and janky background work
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTeleporter : MonoBehaviour
{
    public Transform target;
    bool teleport = false;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        // In teh cave scene this is what places the player in teh cave, or else player spawns in the void.
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (!teleport)
        {
            player.transform.position = target.transform.position;
            teleport = true;
        }
    }
}
