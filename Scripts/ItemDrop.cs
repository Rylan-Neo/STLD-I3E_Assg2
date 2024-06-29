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
    const float m_dropChance = 1f / 4f;
    [SerializeField]
    private GameObject spawnObject;

     public void DestroyShell()
    {
        if (Random.Range(0f, 1f) <= m_dropChance)
        {
            SpawnObject();
        }
        Destroy(gameObject);
    }

    public void GuarenteedDrop()
    {
        SpawnObject();
        Destroy(gameObject);
    }

    void SpawnObject()
    {
        Instantiate(spawnObject, transform.position, spawnObject.transform.rotation);
        Debug.Log("Item should have spawned");
    }
}
