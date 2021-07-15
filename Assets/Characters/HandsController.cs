using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsController : MonoBehaviour
{
    Animator anim;
    public bool fire = false;
    void Start()
    {
        anim = transform.GetComponent<Animator>();
        fire = anim.GetBool("Fire");
    }

    public void Fire(){
        Debug.Log("Test succesful");
        anim.SetBool("Reload", true);
        anim.SetBool("Fire", false);
    }
    
    public void ReloadFinished(){
        anim.SetBool("Reload", false);
        anim.SetBool("Fire", false);
    }

    private void Update() {
        if(fire==false)
            if(Input.GetKeyDown(KeyCode.Mouse0)){
                anim.SetBool("Fire", true);
            }
        if(Input.GetKeyDown(KeyCode.Mouse1)){
            anim.SetBool("Aim", true);
        }
        if(Input.GetKeyUp(KeyCode.Mouse1)){
            anim.SetBool("Aim", false);
        }
    }
    public void Reload(){
        anim.SetBool("Reload", true);
    }

}
