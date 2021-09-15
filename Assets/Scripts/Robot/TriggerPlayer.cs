using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.name =="Player"){
            Debug.Log("Hit Player With Sword");
            GameMaster.CalculateDMG(other.gameObject, 20);
        }
    }
}
