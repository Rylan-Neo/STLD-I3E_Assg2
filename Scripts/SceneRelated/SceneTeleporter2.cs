/*
* Author: Rylan Neo
* Date of creation: 28th June 2024
* Description: Scene Teleporter 2, does shady and janky background work
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTeleporter2 : SceneTeleporter
{
    // It's just a on trigger teleport
    private void OnTriggerEnter(Collider other)
    {
        player.transform.position = target.transform.position;
        if (gameObject.tag == "TempBugFix")
        {
            Destroy(gameObject);
        }
    }
}
