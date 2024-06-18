/*
* Author: Rylan Neo
* Date of creation: 12th June 2024
* Description: All code controlling menu functions.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public int maxHealth = 200;
    public static int currentHealth;
    public HealthBar healthBar;
    public TextMeshProUGUI healthDisplay;
    // All purpose damage function
    void Damage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    // Damage test + health bar
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Damage(10);
            Debug.Log(currentHealth);
            healthDisplay.text = currentHealth.ToString();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Starts at full hp
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
