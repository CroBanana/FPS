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
                FoundPlayer(hit.transform);
            }
        }
        Debug.DrawRay(robotCore.transform.position, direction, Color.green);

        
    }

    public void FoundPlayer(Transform maybePlayer){
        if (maybePlayer.transform.name == "Player")
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
        Vector3 direction = player.transform.position - robotCore.transform.position;
        navAgent.destination=player.transform.position;
        if(Vector3.Distance(transform.position, player.transform.position)>distance){
            //Debug.Log("Didnt stop");
            if(hasStopped){
                navAgent.isStopped = false;
                hasStopped = false;
            }
            anim.SetBool(animation, true);
        }
        else if(Physics.Raycast(new Vector3 (transform.position.x, transform.position.y+1,transform.position.z), direction, out hit, Mathf.Infinity,layers)){

            if(hit.transform.name != "Player"){
                if(hasStopped){
                    navAgent.isStopped = false;
                    hasStopped = false;
                }
                anim.SetBool(animation, true);
            }else
            {
                if(!hasStopped){
                    //Debug.Log("Stopped");
                    navAgent.isStopped = true;
                    hasStopped = true;
                    anim.SetBool(animation, false);
                }
                anim.SetBool(animation, false);
            }
            Debug.DrawRay(robotCore.transform.position, direction, Color.green);
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
