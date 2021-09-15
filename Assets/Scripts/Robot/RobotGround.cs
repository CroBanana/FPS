using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class RobotGround : MonoBehaviour
{

    public Animator anim;

    private NavMeshAgent navAgent;
    public float walkSpeed = 2.3f;
    public float runSpeed = 5;
    public GameObject player;
    public LookAtPlayer topBody;

    public Transform[] patrolPoints;
    private int patrolIndex=0;
    public bool playerFound = false;
    public float findingPlayerDistance;
    public float distanceToPlayer;
    public float meleDistanceToPlayer;

    //gun stuff
    public bool gunFunctioning = true;
    public bool gunCanFire = false;
    public float fireRate = 1;
    private float fireRateOld;
    public GameObject bullet;
    public Transform firePosition;

    //Sword Stuff
    public float  attackRate = 1f;
    float attackRateOld ;
    private bool canAttack;

    //reference na objekte koji se mogu uništiti
    public bool swordFunctioning = true;
    public int canMove=0;
    public bool hasPower = true;
    public Rigidbody mainBoneRig;
    public bool suicideRun;
    public GameObject robotCore;

    public bool hasStopped = false;
    RaycastHit hit;
    public LayerMask layers;
    public LookAtPlayer lookAt;
    // Start is called before the first frame update
    void Start()
    {
        fireRateOld=fireRate;
        attackRateOld = attackRate;
        topBody.enabled = false;
        player = GameObject.Find("Player");
        anim = GetComponentInChildren<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.destination = patrolPoints[0].position;
        anim.SetBool("Walk", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(!gunFunctioning && !swordFunctioning)
            suicideRun = true;
        //hasPower se ondnosi na bateriju ako igrac uspije unistit bateriju robot ce se kretati jos nekiliko sekundi onda prestati
        if(hasPower){
            if(!playerFound){
                //canMove oznacuje koliko robot ima noggu kada izgubi jednu nemoze se kretati
                if(canMove==0)
                    Patrol();
                PlayerDetection();
            }
            else
            {
                if(canMove ==0)
                    MoveToDistanceOfPlayer(distanceToPlayer,"Walk");
                if(gunFunctioning){
                    if(gunCanFire)
                        FireGun();
                }
                else if(swordFunctioning){
                    anim.SetBool("Gun", false);
                    anim.SetBool("SwordRdy", true);
                    if(canMove ==0){
                        //Debug.Log("Running");
                        MoveToDistanceOfPlayer(meleDistanceToPlayer, "Run");
                    }
                    SwordAttack();
                }
                else if(suicideRun){
                    MoveToDistanceOfPlayer(meleDistanceToPlayer, "Run");
                }

            }
        }

    }
    private void LateUpdate() {
        if(robotCore == null){
            Destroy(gameObject);
        }
    }

    void FireGun(){
        fireRate -= Time.deltaTime;
        if(fireRate <= 0){
            fireRate = fireRateOld;
            Instantiate(bullet,firePosition.transform.position,Quaternion.identity,null);
        }
    }

    public void SwordAttack(){
        if(canAttack && !anim.GetBool("Attack")){
            if(Vector3.Distance(transform.position, player.transform.position)<meleDistanceToPlayer+1){
                Debug.Log("Attacking!! "+ Vector3.Distance(transform.position, player.transform.position));
                anim.SetBool("Attack", true);
                lookAt.enabled = false;
                canAttack=false;
                attackRate= attackRateOld;
            }
        }
        else{
            attackRate-=Time.deltaTime;
            if(attackRate<0){
                canAttack=true;
            }
        }
    }

    private void OnDestroy() {
        GameMaster.allEnemies.Remove(this);
        GameMaster.CanGateOpen();
    }
}
