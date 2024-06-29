using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : Collectible
{
    public ItemDrop itemDrop;
    public override void Collected(Player player)
    {
        Debug.Log("Box picked up");
        base.Collected(player);
        itemDrop.DestroyShell();
    }
}
