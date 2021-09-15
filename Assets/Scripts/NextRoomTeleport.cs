using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoomTeleport : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.name =="Player"){
            GameMaster.NextRoom();
        }
    }
}
