/*
* Author: Rylan Neo
* Date of creation: 20th June 2024
* Description: dangerous hazard
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public Player player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    // Damages the player over time
    public void Owch()
    {
        player.Damage(2);
    }

    // Only does so upon the player stanging in it somewhat. idk why i used box collider...
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Invoke("Owch", 0.4f);
        }
    }

}
