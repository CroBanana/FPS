using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RobotGround
{
    public void LostParts(string partName){
        if(partName == "LegLeft" && !lostLeftLeg){
            lostLeftLeg = true;
            DisableLegs();
        }
        if(partName == "LegRight" && !lostRightLeg){
            lostRightLeg = true;
            DisableLegs();
        }
            
        if(partName == "Head"){
            fireRateOld=fireRateOld/2;
        }
        if(partName =="Battery")
            StartCoroutine("RunningOutOfPower");
        if(partName == "Gun"){
            gunFunctioning=false;
            gunCanFire = false;
            navAgent.speed = runSpeed;
            //Debug.Log("Gun arm destroyed");
        }
    }

    void DisableLegs(){
        canMove--;
            if(canMove == -2){
                foreach (MeshCollider col in leg1.GetComponentsInChildren<MeshCollider>() )
                {
                    col.GetComponent<Rigidbody>().isKinematic=false;
                }
                foreach (MeshCollider col in leg2.GetComponentsInChildren<MeshCollider>() )
                {
                    col.GetComponent<Rigidbody>().isKinematic=false;
                }
                mainBoneRig.isKinematic = false;
                anim.SetBool("Walk", false);
                anim.SetBool("Run", false);
                navAgent.isStopped = true;
            }
    }

    IEnumerator RunningOutOfPower(){
        yield return new WaitForSecondsRealtime(7);
        hasPower= false;
        navAgent.isStopped=true;
        anim.enabled = false;
        foreach (LookAtPlayer item in transform.GetComponentsInChildren<LookAtPlayer>())
        {
            item.enabled = false;
        }
        /*
        foreach(var parameter in anim.parameters){
            anim.SetBool(parameter.name, false);
        }*/
        Debug.Log("Disabled");
    }
}
