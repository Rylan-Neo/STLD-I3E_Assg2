/*
* Author: Rylan Neo
* Date of creation: 18th June 2024
* Description: Controlling enemy healthbar and damage statictics.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    // Reference to audio manager and the player script
    public AudioManager audioManager;
    public Player playerScript;

    // The enemy stats
    public int maxHealth = 200;
    public int currentHealth;
    public int damage;

    // 
    public HealthBar healthBar;
    public TextMeshProUGUI healthDisplay;
    public ItemDrop itemDrop;

    // Start is called before the first frame update
    void Start()
    {
        // Starts at full hp
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Crab will take damage when shot by gun
    public void Damage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            if (gameObject.tag != "Boss")
            {
                audioManager.PlaySFX(audioManager.enemyDeath);
            }
            else
            {
                audioManager.PlaySFX(audioManager.bossDeath);
            }
            //Plays a sound, and enemy drops an item
            Destroy(gameObject);
            itemDrop.GuarenteedDrop();
        }

        // Displays the health after being damaged
        healthDisplay.text = currentHealth.ToString();
        Debug.Log("Enemy" + currentHealth);
    }

    // Enemies take a fixed amount of damage when colliding with the player
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Damage(10);
            playerScript.Damage(damage);
        }
    }
}
