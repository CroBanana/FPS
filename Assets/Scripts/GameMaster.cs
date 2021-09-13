using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;
    
    public static GameObject particleExplosion;
    public static GameObject particlePipeFlame;
    public static GameObject particlePipeShower;
    public static LayerMask explosionAffected;
    public static List<RobotGround> allEnemies;
    public static bool botsAwareOfPlayer = false;
    private void Awake() {
        if(Instance != null){
            Debug.Log("There is more then 1 instance");
            return;
        }
        Instance = this;
    }


    // Start is called before the first frame update
    public static void CalculateDMG(GameObject target,int DMGAmount){
        target.GetComponent<HP>().TakeDmg(DMGAmount);
        if(target.CompareTag("Enemy"))
            EnemyGotDMG();
    }

    public static void Destructable(GameObject target, string tag, Vector3 pointOfInpact, Vector3 lookAt){
        if(tag == "Window"){
            target.GetComponent<Destructable>().WindowHit(pointOfInpact);
        }
        if(tag == "PipeFlame" || tag =="Barrel"){
            CreateParticle( pointOfInpact, particlePipeFlame,lookAt,target.transform);
        }
        if(tag == "Pipe"){
            CreateParticle(pointOfInpact, particlePipeShower,lookAt,target.transform);
        }
    }

    public static void Explosion(Vector3 position, string tag,GameObject ignore,float explodingRadius){
        if(tag == "Barrel"){
            GameObject part= Instantiate(particleExplosion, position, Quaternion.identity);
            Collider[] colliders = Physics.OverlapSphere(position,explodingRadius,explosionAffected);
            foreach (Collider col in colliders)
            {
                Debug.Log(col.tag + "    "+ col.name);
                if(col.transform.gameObject != ignore)
                {
                    Debug.Log(col.tag + "    "+ col.name);
                    if(col.CompareTag("Enemy") || col.CompareTag("Player") || col.CompareTag("Barrel")){
                        //Debug.Log(col.name);
                        try
                        {
                            GameMaster.CalculateDMG(col.transform.gameObject,  50);
                        }
                        catch (System.Exception)
                        {
                            
                        }
                    }

                    if(col.gameObject.layer == 12)
                        GameMaster.Destructable(col.gameObject, col.tag, col.transform.position,Vector3.zero);
                }
            }
        }
    }

    public static void CreateParticle(Vector3 pointOfInpact, GameObject particle,Vector3 lookAt,Transform target){
        GameObject part= Instantiate(particle, pointOfInpact,Quaternion.identity,target);
        part.transform.LookAt(lookAt);
        //Vector3 direction = part.transform.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        //part.transform.rotation = rotation;
    }

    public static void EnemyGotDMG(){
        foreach (var enemy in allEnemies)
        {
            enemy.FoundPlayer();
        }
    }
}
