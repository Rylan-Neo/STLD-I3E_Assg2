using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using StarterAssets;

public class Collectible : MonoBehaviour
{
    public virtual void Collected(Player player)
    {
        Debug.Log($"{gameObject.name} picked up by {player.gameObject.name}");
    }
}
