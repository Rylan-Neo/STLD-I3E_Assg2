using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public Transform player;
    public GameObject bullet;
    public float speed;
    public Transform bulletStart;

    public void AttackPlayer()
    {
        GameObject bulletObj = Instantiate(bullet, bulletStart.position, bulletStart.rotation) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        Vector3 direction = (player.position - bulletStart.position).normalized;
        bulletRig.AddForce(bulletStart.forward * speed, ForceMode.Impulse);
        Destroy(bulletObj, 5f);
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
