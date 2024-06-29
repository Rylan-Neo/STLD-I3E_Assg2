using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Collectible
{
    public Player playerScript;
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
        Debug.Log("Gold picked up");
        base.Collected(player);
        GetCollected();
        playerScript.AddGold(1);
    }
}
