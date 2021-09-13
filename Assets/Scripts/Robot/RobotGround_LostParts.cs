using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RobotGround
{
    public void LostParts(string partName){
        if(partName == "Leg"){
            canMove--;
            if(canMove == -2){
                mainBoneRig.isKinematic = false;
                anim.SetBool("Walk", false);
                anim.SetBool("Run", false);
            }
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
            Debug.Log("Gun arm destroyed");
        }
    }

    IEnumerator RunningOutOfPower(){
        yield return new WaitForSecondsRealtime(7);
        hasPower= false;
        foreach(var parameter in anim.parameters){
            anim.SetBool(parameter.name, false);
        }
        Debug.Log("Disabled");
    }
}
