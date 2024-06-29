using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Crystal : Collectible
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
        Debug.Log("Crystal picked up");
        base.Collected(player);
        GetCollected();
        playerScript.AddCrystal(1);
    }
}
