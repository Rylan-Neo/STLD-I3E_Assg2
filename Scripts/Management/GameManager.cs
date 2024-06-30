/*
* Author: Rylan Neo
* Date of creation: 18th June 2024
* Description: game Manager
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Anything under game manager does not destroy after scene swap
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
    }
}
