/*
* Author: Rylan Neo
* Date of creation: 18th June 2024
* Description: Script for enemy to have the Hp bar follow player camera
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
}
