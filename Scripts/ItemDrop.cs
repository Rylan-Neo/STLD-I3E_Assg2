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
    [SerializeField]
    private GameObject spawnObject;
    [SerializeField]
    private AudioClip collectAudio;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            AudioSource.PlayClipAtPoint(collectAudio, transform.position, 1f);
        SpawnObject();
        Destroy(gameObject);
    }

    void SpawnObject()
    {
        Instantiate(spawnObject, transform.position, spawnObject.transform.rotation);
    }
}
