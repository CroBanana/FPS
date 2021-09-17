using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesForGM : MonoBehaviour
{
    public  GameObject particleExplosion;
    public  GameObject particlePipeFlame;
    public  GameObject particlePipeShower;
    public GameObject particleBulletImpact;
    public LayerMask explosionAffected;
    public GameObject gate;
    public List<Room> allRooms;

    // Start is called before the first frame update
    void Start()
    {
        GameMaster.gate=gate;
        GameMaster.particleExplosion=particleExplosion;
        GameMaster.particlePipeFlame = particlePipeFlame;
        GameMaster.particlePipeShower = particlePipeShower;
        GameMaster.explosionAffected = explosionAffected;
        GameMaster.particleBulletImpact = particleBulletImpact;
        foreach (Transform room in transform)
        {
            allRooms.Add(room.GetComponent<Room>());
        }
        GameMaster.allRooms = allRooms;
    }
}
