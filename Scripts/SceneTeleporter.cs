using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTeleporter : MonoBehaviour
{
    public Transform target;
    bool teleport = false;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (!teleport)
        {
            player.transform.position = target.transform.position;
            teleport = true;
        }
    }
}
