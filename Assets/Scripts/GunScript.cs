using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public int dmgAmount;
    public int maxMag;
    public int magSize;
    public float fireSpeed;

    public PlayerMovement playerMovement;

    private void Start() {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    public void ReloadComplete(){
        playerMovement.ReloadComplete();
    }

}
