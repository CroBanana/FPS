using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{

    public void HandsInputs()
    {
        //promjena oruzja
        if (Input.mouseScrollDelta.y > 0)
        {
            curentWeapon += 1;
            hands.SetBool("SwitchingWeapon", true);
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            curentWeapon -= 1;
            hands.SetBool("SwitchingWeapon", true);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            hands.SetBool("Aim", true);
            speed = speedAim;
            MenuAndSettings.instance.DeactivateCursor();
            isZooming=true;
            mouseLook.ads=ads;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            hands.SetBool("Aim", false);
            speed = speedNormal;
            MenuAndSettings.instance.ActivateCursor();
            isZooming=false;
            mouseLook.ads = 1f;
        }


        //probjera je li magazin prazan i ako je napravi se reload animacija
        //ako nije igrac moze stisnut r kako bi napunio magazin
        if (curentGunScript.magSize <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            if (curentGunScript.magSize == curentGunScript.maxMag)
                return;
            hands.SetBool("Reload", true);
        }
        else
        {
            if(Input.GetKey(KeyCode.Mouse0)){
                Shoot();
            }
            if(Input.GetKeyUp(KeyCode.Mouse0)){
                hands.SetBool("Fire", false);
            }

        }

    }


    public void SwitchingWeaponFinished()
    {
        Debug.Log(weapons.Length);
        if (curentWeapon < 0)
        {
            curentWeapon = weapons.Length - 1;
        }
        if (curentWeapon > weapons.Length - 1)
        {
            curentWeapon = 0;
        }

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        hands.SetBool("Reload", false);
        hands.SetInteger("WeaponType", curentWeapon);
        hands.SetBool("SwitchingWeapon", false);
        weapons[curentWeapon].SetActive(true);
        curentGunScript = weapons[curentWeapon].GetComponent<GunScript>();
        MenuAndSettings.instance.ammo.text = curentGunScript.magSize.ToString()+"/"+curentGunScript.maxMag.ToString();
    }

    public void Fire()
    {
        //curentGunScript.muzzleFlash.Play();
        //curentGunScript.magSize--;
    }



    public void ReloadFinished()
    {
        curentGunScript.magSize = curentGunScript.maxMag;
        hands.SetBool("Reload", false);
        MenuAndSettings.instance.ammo.text = curentGunScript.magSize.ToString()+"/"+curentGunScript.maxMag.ToString();
    }

    public void ReloadStart()
    {
        speed = speedNormal;
        hands.SetBool("Aim", false);
        hands.SetBool("Fire", false);
    }

}
