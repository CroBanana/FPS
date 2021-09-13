using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesForGM : MonoBehaviour
{
    public  GameObject particleExplosion;
    public  GameObject particlePipeFlame;
    public  GameObject particlePipeShower;
    public LayerMask explosionAffected;
    public List<RobotGround> allEnemies;

    // Start is called before the first frame update
    void Start()
    {
        GameMaster.particleExplosion=particleExplosion;
        GameMaster.particlePipeFlame = particlePipeFlame;
        GameMaster.particlePipeShower = particlePipeShower;
        GameMaster.explosionAffected = explosionAffected;
        RobotGround[] gos = GameObject.FindObjectsOfType<RobotGround>();
        foreach (var ground in gos)
        {
            allEnemies.Add(ground);
        }
        GameMaster.allEnemies = allEnemies;
    }
}
