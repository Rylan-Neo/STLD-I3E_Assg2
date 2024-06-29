using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : Collectible
{
    public Transform target;
    bool teleport = false;


    private void OnTriggerStay(Collider other)
    {
        if (teleport)
        {
            other.transform.position = target.transform.position;
            teleport = false;
        }
    }

    public override void Collected(Player player)
    {
        Debug.Log("Teleporting ");
        base.Collected(player);
        teleport = true;
    }
}
