using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableArmor : MonoBehaviour
{
    private Rigidbody rig;
    public bool SplitRestOfBody;
    private GameObject player;
    public MeshCollider[] disableRest;
    public bool impactFromBody;
    public GameObject center;

    public RobotGround robot;
    public bool gunArm;
    public bool swordArm;
    public bool legL;
    public bool legR;
    public bool head;
    public bool battery;

    public bool affectsRobot;
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        robot = transform.root.GetComponent<RobotGround>();
    }

    public void ArmorDestroyed(){
        if(disableRest != null){
            foreach (var rest in disableRest)
            {
                rest.gameObject.GetComponent<DestructableArmor>().ArmorDestroyed();
                Destroy(rest.gameObject.GetComponent<HP>());
            }
        }
        if(SplitRestOfBody){
            Debug.Log("Parent: "+transform.parent.name);
            
            MeshCollider[] children = transform.parent.GetComponentsInChildren<MeshCollider>();
            foreach (var part in children)
            {
                if(part.transform == transform)
                    continue;
                Destroy( part.GetComponent<Rigidbody>());
                Destroy(part.GetComponent<HP>());
                Destroy(part.GetComponent< DestructableArmor>());
                part.transform.SetParent(transform);
                part.gameObject.layer = 0;
            }
        }

        transform.SetParent(null);
        Debug.Log("Armor destroyed");
        rig.isKinematic=false;
        gameObject.transform.SetParent(null);
        //Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y+1, player.transform.position.z);
        if(!impactFromBody){
            Vector3 direction = player.transform.position - transform.position;
            rig.AddForceAtPosition(-1*direction.normalized*1000, player.transform.position);
        }else
        {
            Vector3 direction = center.transform.position - transform.position;
            rig.AddForceAtPosition(1*direction.normalized*1000, center.transform.position);
        }
        DisableRobotFunction();
        //rig.AddExplosionForce(500, pointOfInpact,0.5f);
        StartCoroutine("DisableRig");
    }

    void DisableRobotFunction(){
        if(gunArm){
            robot.LostParts("Gun");
        }
        if(swordArm)
            robot.swordFunctioning=false;
        if(legL)
            robot.LostParts("Leg");
        if(legR)
            robot.LostParts("Leg");
        if(head)
            robot.LostParts("Head");
        if(battery)
            robot.LostParts("Battery");
    }

    IEnumerator DisableRig(){
        yield return new WaitForSecondsRealtime(7);
        rig.isKinematic = true;
        Debug.Log("Disabled");
    }
}
