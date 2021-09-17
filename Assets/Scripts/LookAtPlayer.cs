using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private GameObject Player;

    private void Awake() {
        Player = GameObject.Find("Player");
    }

    private void LateUpdate() {
        if(Player!=null)
            transform.LookAt( Player.transform.position);
    }
}
