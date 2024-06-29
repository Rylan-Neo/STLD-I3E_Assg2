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
    public int maxHealth = 200;
    public int currentHealth;
    public HealthBar healthBar;
    public TextMeshProUGUI healthDisplay;
    public int damage;
    public ItemDrop itemDrop;
    public Player playerScript;
    public void Damage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            itemDrop.GuarenteedDrop();
        }
        healthDisplay.text = currentHealth.ToString();
        Debug.Log("Enemy" + currentHealth);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Damage(10);
            playerScript.Damage(damage);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // Starts at full hp
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
