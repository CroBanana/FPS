using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FlyingRobot
{
    void WhereToMove()
    {
        if (playerFound)
        {
            target.x = player.transform.position.x;
            target.y = player.transform.position.y + 1.5f;
            target.z = player.transform.position.z;
            if(gun==null){
                target.y-=0.5f;
                Movement(0);
            }else
            {
                Movement(distanceToPlayer);
            }
            //Debug.Log("Moving to player");
        }
        else if (patrolPoints != null)
        {
            if (currentPoint >= patrolPoints.Length)
            {
                currentPoint = 0;
            }
            else if (Vector3.Distance(transform.position, patrolPoints[currentPoint].transform.position) <= distanceToPatroll + 0.2f)
            {
                currentPoint++;
                if (currentPoint > patrolPoints.Length)
                {
                    currentPoint = 0;
                }
            }
            target.x = patrolPoints[currentPoint].transform.position.x;
            target.y = patrolPoints[currentPoint].transform.position.y;
            target.z = patrolPoints[currentPoint].transform.position.z;
            Movement(distanceToPatroll);
        }
    }

    void Movement(float minDistanceToTarget)
    {

        if (moveToTarget && Vector3.Distance(transform.position, target) > minDistanceToTarget)
        {
            Debug.DrawRay(transform.position, -transform.up, Color.green);
            if (Physics.Raycast(transform.position, -transform.up, out hit, 2f, layers))
            {
                levitateHeight = hit.transform.position.y + 2f;
                //Debug.Log("Its here");
            }
            else
            {
                levitateHeight = target.y;
            }
            transform.position = Vector3.MoveTowards(transform.position,
                                                    target,
                                                    moveSpeed * Time.deltaTime);
            if(gun == null && Vector3.Distance(transform.position, target) < 0.5f){
                Destroy(gameObject);
            }
        }

        rot = Quaternion.LookRotation(target - transform.position);

        transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotateSpeed * Time.deltaTime);

    }

    void Flight()
    {
        // letenje tj hover dodaje force svaki update kako bi ostajao u zraku
        force = flightNumber * (levitateHeight - (transform.position.y / 2));
        force = Mathf.Clamp(force, minimumForce, maxforce);

        forceWings.y = force;
        if (transform.position.y < levitateHeight)
        {
            leftW?.AddForce(forceWings);
            rightW?.AddForce(forceWings);
        }
    }

    
}
