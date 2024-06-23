using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Crystal : Collectible
{
    void GetCollected()
    {
        Destroy(gameObject);
    }

    public override void Collected(Player player)
    {
        Debug.Log("Crystal picked up");
        base.Collected(player);
        GetCollected();
    }
}
