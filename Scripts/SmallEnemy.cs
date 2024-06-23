/*
* Author: Rylan Neo
* Date of creation: 18th June 2024
* Description: Controlling enemy healthbar and damage statictics.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SmallEnemy : MonoBehaviour
{
    public int maxHealth = 200;
    public static int currentHealth;
    public HealthBar healthBar;
    public TextMeshProUGUI healthDisplay;
    void Damage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Damage(10);
            Debug.Log("Enemy" + currentHealth);
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
