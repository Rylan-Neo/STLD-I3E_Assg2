/*
* Author: Rylan Neo
* Date of creation: 25th June 2024
* Description: Script for gun to allow it to shoot
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Gun : MonoBehaviour
{
    /// <summary>
    /// This variable allows us to connect other variables to create custom gun behavior
    /// </summary>
    public UnityEvent onGunShoot;

    /// <summary>
    /// Number of seconds acting as a buffer between firing
    /// </summary>
    public float fireCoolDown;

    // change the gun shooting either to semi or auto (deafult = auto)
    public bool automatic;

    /// <summary>
    /// Store current cooldown of the weapon
    /// </summary>
    private float currentCooldown;

    /// <summary>
    /// checks if the left mouse button is currently being held down. Checks if the current cooldown of the weapon is less than or equal to zero.
    /// </summary>
    void Start()
    {
        currentCooldown = fireCoolDown;
    }

    // Update will shoot the gun
    void Update()
    {
        if (automatic)
        {
            if (Input.GetMouseButton(0))
            {
                if (currentCooldown <= 0f)
                {
                    onGunShoot?.Invoke();
                    currentCooldown = fireCoolDown;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentCooldown <= 0f)
                {
                    onGunShoot?.Invoke();
                    currentCooldown = fireCoolDown;
                }
            }
        }
        currentCooldown -= Time.deltaTime;
    }
}
