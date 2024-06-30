/*
* Author: Rylan Neo
* Date of creation: 22nd June 2024
* Description: Script for item drop after a collectable casing/box is destroyed.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    /// <summary>
    /// The chance of the health drug dropping is 25%,
    /// </summary>
    const float m_dropChance = 1f / 4f;
    /// <summary>
    /// Object that the script will spawn
    /// </summary>
    [SerializeField]
    private GameObject spawnObject;

    // Destroys the object and randomly might drop a health drug
     public void DestroyShell()
    {
        if (Random.Range(0f, 1f) <= m_dropChance)
        {
            SpawnObject();
        }
        Destroy(gameObject);
    }
    
    // This function is run when the object is required to be a guarenteed drop
    public void GuarenteedDrop()
    {
        SpawnObject();
        Destroy(gameObject);
    }

    // Spawns the object.
    void SpawnObject()
    {
        Instantiate(spawnObject, transform.position, spawnObject.transform.rotation);
        Debug.Log("Item should have spawned");
    }
}
