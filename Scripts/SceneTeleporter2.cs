using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTeleporter2 : SceneTeleporter
{
    private void OnTriggerEnter(Collider other)
    {
        player.transform.position = target.transform.position;
        if (gameObject.tag == "TempBugFix")
        {
            Destroy(gameObject);
        }
    }
}
