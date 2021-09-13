using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
    public Player player;
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Layer" +other.gameObject.layer);
        if(other.gameObject.layer ==6 && !player.grounded){
            player.grounded=true;
            Debug.Log("Grounded = true");
        }

    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.layer ==6 && player.grounded){
            player.grounded=false;
            Debug.Log("Grounded = false");
        }
    }
}
