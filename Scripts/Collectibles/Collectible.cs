/*
* Author: Rylan Neo
* Date of creation: 19th June 2024
* Description: Collectible parent script
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using StarterAssets;

/// <summary>
/// Parent class for collectible
/// </summary>
public class Collectible : MonoBehaviour
{
    /// <summary>
    /// We use this script for a variety of function including placing objects since it is connected to raytrace
    /// </summary>
    /// <param name="player"></param>
    public virtual void Collected(Player player)
    {
        Debug.Log($"{gameObject.name} picked up by {player.gameObject.name}");
    }
}
