using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RobotGround
{
    public void Patrol(){
        if(Vector3.Distance(transform.position,patrolPoints[patrolIndex].position)<2){
            patrolIndex++;
            anim.SetBool("Walk",true);
            Debug.Log(patrolPoints.Length-1);
            if(patrolIndex>patrolPoints.Length-1){
                patrolIndex=0;
            }
            navAgent.destination = patrolPoints[patrolIndex].position;
        }
    }
    
    void PlayerDetection()
    {
        
        Vector3 direction = player.transform.position - transform.position;
        //Debug.Log(Vector3.Angle(transform.forward, direction));
        Debug.DrawRay(transform.position, direction * 15f, Color.red);
        if (Vector3.Angle(transform.forward, direction) < 60)
        {
            if (Physics.Raycast(transform.position, direction, out hit, findingPlayerDistance, layers))
            {
                FoundPlayer();
            }
        }
        
    }

    public void FoundPlayer(){
        if (hit.transform.name == "Player")
            {
                playerFound=true;
                navAgent.destination = player.transform.position;
                anim.SetBool("Walk",true);
                anim.SetBool("Gun", true);
                gunCanFire = true;
                topBody.enabled = true;
            }
    }

    void MoveToDistanceOfPlayer(float distance, string animation){
        //Debug.Log(Vector3.Distance(transform.position, player.transform.position));

        navAgent.destination=player.transform.position;
        if(Vector3.Distance(transform.position, player.transform.position)<distance){
            if(!hasStopped){
                Debug.Log("Stopped");
                navAgent.isStopped = true;
                hasStopped = true;
                anim.SetBool(animation, false);
            }
        }
        else {
            Debug.Log("Didnt stop");
            if(hasStopped){
                navAgent.isStopped = false;
                hasStopped = false;
                anim.SetBool(animation, true);
            }
        }

        if(suicideRun && Vector3.Distance(transform.position, player.transform.position)<2f){
            try
            {
                GameMaster.CalculateDMG(robotCore, 100);
            }
            catch (System.Exception)
            {
                
            }
        }

    }

}
