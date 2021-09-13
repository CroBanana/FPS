using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    private Rigidbody rig;
    public GameObject player;
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void WindowHit(Vector3 pointOfInpact){
        //Debug.Log("Window hit");
        rig.isKinematic=false;
        //Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y+1, player.transform.position.z);
        Vector3 direction = player.transform.position - transform.position;
        rig.AddForceAtPosition(-1*direction.normalized*1000, pointOfInpact);
        //rig.AddExplosionForce(500, pointOfInpact,0.5f);
    }
}
