using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrug : Collectible
{
    public Player playerScript;
    public bool drugsEnabled = false;

    private void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void GetCollected()
    {
        Destroy(gameObject);
    }

    public override void Collected(Player player)
    {
        Debug.Log("Drug picked up");
        base.Collected(player);
        GetCollected();
        playerScript.UseDrug(-1);
        if (!drugsEnabled) 
        {
            drugsEnabled=true;
            playerScript.GetComponent<Player>().EnableHealing();
        }
    }
}
