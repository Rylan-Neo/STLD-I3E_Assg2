/*
* Author: Rylan Neo
* Date of creation: 27th June 2024
* Description: crab bullet
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Reference to player
    public Player player;
    // When the bullet contacts player, player will take a massive amount of damage
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.Damage(32);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

}
