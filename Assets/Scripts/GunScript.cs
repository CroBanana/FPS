using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public int dmgAmount;
    public int maxMag;
    public int magSize;
    public float fireSpeed;
    public float fireSpeedConstant;

    public ParticleSystem muzzleFlash;

    public Transform cameraAimPosition;

}
