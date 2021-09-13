using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    void Move()
    {
        float horizontal;
        float vertical;
        //Debug.Log("Moving");

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);
    }

    void JumpFall()
    {

        if (!grounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("PRESSED!!!");
                velocity.y = Mathf.Sqrt(jumpHeight * -gravity);
            }
        }
        controller.Move(velocity * Time.deltaTime);
    }

    public void GroundCheck(){
        grounded =Physics.CheckSphere(groundSpherePosition.position, 0.5f,raycastLayers);

    }

    private void OnDrawGizmos() {
        //Gizmos.color=Color.red;
        //Gizmos.DrawSphere(groundSpherePosition.position,0.5f);
    }
}
