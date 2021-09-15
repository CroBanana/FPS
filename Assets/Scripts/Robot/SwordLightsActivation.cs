using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordLightsActivation : MonoBehaviour
{
    public GameObject[] swordLights;
    public Animator anim;
    public LookAtPlayer lookAt;

    private void Start() {
        anim = GetComponent<Animator>();

        foreach (var item in swordLights)
        {
            item.SetActive(false);
        }
    }
    public void SwordLightActivation(){
        foreach (var item in swordLights)
        {
            item.SetActive(true);
        }
    }

    public void AttackDone(){
        Debug.Log("AttackDone");
        anim.SetBool("Attack", false);
        lookAt.enabled=true;
    }
}
