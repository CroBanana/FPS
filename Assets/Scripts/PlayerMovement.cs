using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerMovement : MonoBehaviour
{
    float horizontal,vertical;
    CharacterController controller;

    //brzina kretanja i gravitacija
    public float speed;
    public float gravity = -9.81f;
    Vector3 velocity;
    public float jumpHeight=3f;

    // provjera je li na zemlji
    public LayerMask groundMask;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public bool grounded;

    //raycast sta moze pogoditi
    Camera thisCamera ;
    RaycastHit hitInfo;
    public LayerMask raycastLayers;
    public LayerMask raycastGuns;

    //oruzja
    public GameObject[] guns;
    public int selectedGunIndex=0;
    public GameObject selectedGun;
    public GunScript gunScript;
    public float fireSpeed;
    public int magSize;

    //oruzja animacije
    public Animator gunAnimator;
    public bool reload;


    //za testiranje kasnije ce bit samo ruke
    public GameObject gunPos;

    private void Start() {
        controller= GetComponent<CharacterController>();
        thisCamera = transform.GetComponentInChildren<Camera>();

        selectedGun = guns[selectedGunIndex];
        gunScript = selectedGun.GetComponent<GunScript>();
        fireSpeed = gunScript.fireSpeed;
        gunAnimator= selectedGun.GetComponent<Animator>();
        magSize = gunScript.magSize;
        foreach (var gun in guns)
        {
            if(gun != selectedGun){
                gun.SetActive(false);
                gun.GetComponent<Animator>().enabled=false;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        JumpFall();
        OnSwitchWeapon();
        Reload();
        PickUpGun();
    }

    #region Movement
    void Move(){
        grounded = Physics.CheckSphere(groundCheck.position,groundDistance, groundMask);

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right*horizontal+ transform.forward * vertical;
        controller.Move(move*speed *Time.deltaTime);


    }

    void JumpFall(){
        if(!grounded){
            velocity.y += gravity * Time.deltaTime;
        }else{
            velocity.y=0f;
            if(Input.GetButtonDown("Jump")){
                Debug.Log("PRESSED!!!");
                velocity.y = Mathf.Sqrt(jumpHeight *-2f * gravity);
            }
        }
        controller.Move (velocity * Time.deltaTime);
    }

    #endregion

    private void FixedUpdate() {

        Shoot();

        //Debug.DrawRay(thisCamera.transform.position, thisCamera.transform.forward * 1000, Color.blue);

    }

    void Shoot(){
        if(fireSpeed>-1){
            fireSpeed  = fireSpeed -Time.deltaTime;
        }
        if(fireSpeed<=0 && magSize>0){
            if(Input.GetKey(KeyCode.Mouse0)){
                if(Physics.Raycast(thisCamera.transform.position, 
                                thisCamera.transform.forward, 
                                out hitInfo, 
                                100.0f,
                                raycastLayers))
                {
                    GameMaster.CalculateDMG(hitInfo.transform.gameObject, gunScript.dmgAmount);
                }
                fireSpeed = gunScript.fireSpeed;
                magSize--;
                Debug.DrawRay(thisCamera.transform.position, thisCamera.transform.forward * 1000, Color.blue);

            }

        }

    }

    void OnSwitchWeapon(){
        if(Input.GetAxis("Mouse ScrollWheel")>0f){

            selectedGunIndex=selectedGunIndex+1;
            if(selectedGunIndex>guns.Length-1){
                selectedGunIndex=0;
            }
            SelectNewGun();
        }
        if(Input.GetAxis("Mouse ScrollWheel")<0f){

            selectedGunIndex=selectedGunIndex-1;
            if(selectedGunIndex<0){
                selectedGunIndex=guns.Length-1;
            }
            SelectNewGun();
        }
    }

    void SelectNewGun(){
        selectedGun.SetActive(false);
        gunAnimator.enabled = false;
        selectedGun = guns[selectedGunIndex];
        selectedGun.SetActive(true);
        gunAnimator = selectedGun.GetComponent<Animator>();
        gunAnimator.enabled = true;
        gunScript = selectedGun.GetComponent<GunScript>();
        gunAnimator = selectedGun.GetComponent<Animator>();
        magSize = gunScript.magSize;
    }

    void Reload(){

        if(magSize!= gunScript.maxMag){
            if(Input.GetKey(KeyCode.R))
            gunAnimator.SetBool("Reload", true);
        }

    }

    public void ReloadComplete(){
        magSize= gunScript.maxMag;
        gunAnimator.SetBool("Reload",false);
    }

    public void PickUpGun(){
        Debug.DrawRay(thisCamera.transform.position, thisCamera.transform.forward * 5f, Color.blue);
        if(Physics.Raycast(thisCamera.transform.position, 
                                thisCamera.transform.forward, 
                                out hitInfo, 
                                5f,
                                raycastGuns))
                {
                    Debug.Log("GUNGUNGUN");

                    if(Input.GetKeyDown(KeyCode.E)){

                        //pokupljanje oruzja
                        //mice se roditelj objekta
                        //zamjenjuje se u listi oruzja trenutno oruzje
                        selectedGun.transform.SetParent(null);
                        gunAnimator.enabled = false;
                        selectedGun.transform.position = hitInfo.transform.position;
                        selectedGun.transform.rotation = hitInfo.transform.rotation;
                        hitInfo.transform.SetParent(thisCamera.transform);
                        hitInfo.transform.position = gunPos.transform.position;
                        guns[selectedGunIndex] = hitInfo.transform.gameObject;
                        selectedGun= guns[selectedGunIndex];
                        SelectNewGun();
                    }
                }
    }
}
