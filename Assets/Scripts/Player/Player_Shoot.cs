using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public void Shoot(){
        if(canFire ){
            //Debug.Log("Fired");
            hands.SetBool("Fire",true);
            curentGunScript.muzzleFlash.Play();
            curentGunScript.magSize--;
            curentGunScript.fireSpeed=curentGunScript.fireSpeedConstant;
            canFire=false;
            MenuAndSettings.instance.ammo.text = curentGunScript.magSize.ToString()+"/"+curentGunScript.maxMag.ToString();
            CheckIfHitSometing();
        }
    }

    public void CheckIfHitSometing(){
        if(Physics.Raycast(thisCamera.transform.position,
                                thisCamera.transform.forward,
                                out hitInfo,
                                100.0f,
                                raycastLayers))
                {
                    if(hitInfo.collider.gameObject.CompareTag("Enemy") || hitInfo.collider.gameObject.CompareTag("Barrel")){
                        GameMaster.CalculateDMG(hitInfo.transform.gameObject,  curentGunScript.dmgAmount);
                        GameMaster.CreateParticle(hitInfo.point,null,hitInfo.point+hitInfo.normal,null);
                    }

                    if(hitInfo.collider.gameObject.layer == 12)
                        GameMaster.Destructable(hitInfo.transform.gameObject, hitInfo.collider.tag, hitInfo.point,Vector3.zero);
                    if(hitInfo.collider.gameObject.layer == 13)
                        GameMaster.Destructable(hitInfo.transform.gameObject, hitInfo.collider.tag, hitInfo.point, hitInfo.point+hitInfo.normal);
                    if(hitInfo.collider.CompareTag("Light"))
                        GameMaster.ShootLight(hitInfo.collider.gameObject, hitInfo.point);
                }
                Debug.DrawRay(thisCamera.transform.position, thisCamera.transform.forward * 1000, Color.blue);

    }
}
