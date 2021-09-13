using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FlyingRobot : MonoBehaviour
{
    public bool test = false;
    [Header("Materials for eyes")]
    //materijali za promjenu boje oka u ovisnosti animacija
    public Material eyeActive, eyeInactive;
    Material[] bodyMaterials;
    MeshRenderer body_mr;

    // svi potrebni dijelovi robota ovisno kad se koji unisti nesto ce se desavati
    [Header("Main objects Neded")]
    public GameObject body;
    public GameObject weapon, leftWing, rightWing;
    public GameObject leftWIngParticle, rightWingParticle;
    Rigidbody leftW, rightW;

    //za pripremanje oruzja
    public GameObject gunFlap1, gunFlap2, gun;
    public float extractSpeed, distanceToMove;
    public GameObject whereToMove;

    //potrebno za letenje;
    [Header("Flight options")]
    bool flight = false;
    public Vector3 forceWings;
    public float flightNumber;
    public float levitateHeight;
    public float minimumForce;
    public float maxforce = 30;
    public bool changeDamping = false;

    //jacina s kojom se robot dize ili spusta
    private float force;


    // kretanje prema točci
    [Header("Movement options")]
    public GameObject player;
    public GameObject[] patrolPoints;
    public int currentPoint = 0;
    public float moveSpeed, rotateSpeed;
    public bool moveToTarget = false;
    public float distanceToPlayer, distanceToPatroll;

    //direkcija prema igraču
    private Vector3 direction, target;
    //rotacija samog robota kamo da gleda
    private Quaternion rot;

    [Header("Engaging player")]
    public bool fire = false;

    //detekritanje igrača
    [Header("Detection")]
    Ray ray;
    public LayerMask layers;
    RaycastHit hit;
    public bool playerFound;
    public float findingPlayerDistance;

    // Start is called before the first frame update
    void Start()
    {
        leftWIngParticle.SetActive(false);
        rightWingParticle.SetActive(false);
        leftW = leftWing.GetComponent<Rigidbody>();
        rightW = rightWing.GetComponent<Rigidbody>();
        body_mr = body.GetComponent<MeshRenderer>();
        bodyMaterials = body_mr.materials;
        bodyMaterials[1] = eyeInactive;
        body_mr.materials = bodyMaterials;
        gun.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (flight)
        {
            Flight();
            WhereToMove();
            PlayerDetected();
        }

    }

    void PlayerDetected()
    {
        direction = player.transform.position - transform.position;
        //Debug.Log(Vector3.Angle(transform.forward, direction));
        Debug.DrawRay(transform.position, direction * 15f, Color.red);
        if (Vector3.Angle(transform.forward, direction) < 60 && playerFound == false)
        {
            if (Physics.Raycast(transform.position, direction, out hit, findingPlayerDistance, layers))
            {
                //Debug.Log(hit.transform.name);
                if (hit.transform.name == "Player")
                {
                    //Debug.Log("ItsHere");
                    gunFlap1.SetActive(false);
                    gunFlap2.SetActive(false);
                    gun.SetActive(true);
                    playerFound = true;

                }
            }
        }
    }


    public void RobotActivated()
    {
        bodyMaterials[1] = eyeActive;
        body_mr.materials = bodyMaterials;

    }

    public void FlightRDY()
    {
        gameObject.AddComponent<FixedJoint>().connectedBody = leftW;
        gameObject.AddComponent<FixedJoint>().connectedBody = rightW;
        leftWIngParticle.SetActive(true);
        rightWingParticle.SetActive(true);

        flight = true;
        moveToTarget = true;
    }

    public void AbleToFire()
    {
        fire = true;
    }



}
