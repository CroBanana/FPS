using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public Player playerScript;
    public void Fire()
    {
        playerScript.Fire();
        //Debug.Log("Fire");
    }

    public void ReloadStart()
    {
        playerScript.ReloadStart();
    }

    public void ReloadFinished()
    {
        playerScript.ReloadFinished();
    }

    public void SwitchingWeaponFinished()
    {
        playerScript.SwitchingWeaponFinished();
    }

}
