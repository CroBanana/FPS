using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    CharacterController controller;
    //brzina kretanja i gravitacija
    public float speedNormal;
    public float speedAim;
    public float speed;
    float gravity = -9.81f;
    public float jumpHeight = 3f;
    Vector3 velocity = new Vector3(0,0,0);

    // provjera je li na zemlji
    public bool grounded;
    public Transform groundSpherePosition;

    //raycast sta moze pogoditi
    [Header("Raycast sta moze pogoditi")]
    public LayerMask raycastLayers;
    public Camera thisCamera;
    RaycastHit hitInfo;


    [Header("Oruzja i animator za ruke")]
    //animator za ruke i oruzja
    public Animator hands;
    public bool fire = false;
    public GameObject[] weapons;
    public GunScript curentGunScript;
    public int curentWeapon = 0;
    public bool canFire;
    
    //zoom in and out
    public bool isZooming;
    public int zoom= 30;
    public int normal = 60;
    public float zoomSmooth = 5;
    public MouseLook mouseLook;
    public float ads=0.8f;

    //Ikona za ciljanje
    public Texture2D cursorTexture;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.SetCursor(cursorTexture,Vector2.zero, CursorMode.Auto);
        Cursor.lockState= CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        hands.SetInteger("WeaponType", 0);
        foreach (var weapon in weapons)
        {
            weapon.SetActive(false);
        }
        weapons[curentWeapon].SetActive(true);
        curentGunScript = weapons[curentWeapon].GetComponent<GunScript>();

    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        Move();
        JumpFall();
        GroundCheck();
        //animator inputs
        HandsInputs();
        FireRate();
        Zoom();
    }

    private void FixedUpdate() {

    }

    public void FireRate(){
        if(curentGunScript.fireSpeed>-1)
            curentGunScript.fireSpeed-=Time.deltaTime;

        if(curentGunScript.fireSpeed<=0){
            canFire=true;
        }
    }

    public void Zoom(){
        if(isZooming){
            thisCamera.fieldOfView = Mathf.Lerp(thisCamera.fieldOfView, zoom,Time.deltaTime*zoomSmooth);
        }
        else if(thisCamera.fieldOfView <= normal)
        {
            thisCamera.fieldOfView = Mathf.Lerp(thisCamera.fieldOfView, normal,Time.deltaTime*zoomSmooth);
        }
    }
    private void OnDestroy() {
        MenuAndSettings.instance.EndGame("Player");
    }
}
