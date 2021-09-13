using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20;
    private GameObject player;

    private void Start() {
        player = GameObject.Find("Player");
        transform.LookAt(player.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime*speed);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.name);
        Destroy(gameObject);
        
    }
}
