/*
* Author: Rylan Neo
* Date of creation: 25th June 2024
* Description: Script for gun dealing damage
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGun : MonoBehaviour
{
    public AudioManager audioManager;
    public int damage = 24;
    public float bulletRange = 50f;
    private Transform playerCamera;

    void Start()
    {
        playerCamera = Camera.main.transform;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    /// <summary>
    /// Casts a ray from the player's camera forward direction (PlayerCamera.forward) to simulate shooting.
    /// </summary>
    public void Shoot()
    {
        Ray gunRay = new Ray(playerCamera.position, playerCamera.forward); //creates a new ray named gunRay, ray starts from the position of the PlayerCamera
        if (Physics.Raycast(gunRay, out RaycastHit hitInfo, bulletRange)) //checks if the ray hits any collider within a maximum distance of BulletRange.
        {
            Enemy enemy = hitInfo.collider.gameObject.GetComponent<Enemy>(); //GameObject that was hit by the raycast 
            if (enemy != null)
            {
                enemy.Damage(damage); //hit GameObject, this line reduces its CurrentHealth
                Debug.Log("currenthealth: "); //check that an enemy was shot
            }
            audioManager.PlaySFX(audioManager.gunFire);
        }
    }
}
