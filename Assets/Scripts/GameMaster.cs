using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;
    
    public static GameObject particleExplosion;
    public static GameObject particlePipeFlame;
    public static GameObject particlePipeShower;
    public static GameObject particleBulletImpact;
    public static LayerMask explosionAffected;
    public static List<RobotGround> allEnemies;
    public static List<FlyingRobot> allFlying;
    public static bool botsAwareOfPlayer = false;
    public static GameObject player;
    public static GameObject gate;
    public static List<Room> allRooms;
    public static Transform curentRoom;
    private void Awake() {
        if(Instance != null){
            Debug.Log("There is more then 1 instance");
            return;
        }
        Instance = this;
    }
    private void Start() {
        //SceneManager.LoadScene(1);
        player = GameObject.Find("Player");
        Time.timeScale = 1;
        SetMouseSpeed(MenuAndSettings.instance.mouseSpeedModifier);
        MenuAndSettings.instance.play.SetActive(false);
        MenuAndSettings.instance.resume.SetActive(true);
        MenuAndSettings.instance.menu.SetActive(false);
        MenuAndSettings.instance.hud.SetActive(true);
    }


    // Start is called before the first frame update
    public static void CalculateDMG(GameObject target,int DMGAmount){
        target.GetComponent<HP>().TakeDmg(DMGAmount);
        if(target.CompareTag("Enemy")){
            EnemyGotDMG();
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            ESC();
        }
    }


    public static void Destructable(GameObject target, string tag, Vector3 pointOfInpact, Vector3 lookAt){
        if(tag == "Window"){
            target.GetComponent<Destructable>().WindowHit(pointOfInpact);
        }
        if(tag == "PipeFlame" || tag =="Barrel"){
            CreateParticle( pointOfInpact, particlePipeFlame,lookAt,target.transform);
        }
        if(tag == "Pipe"){
            CreateParticle(pointOfInpact, particlePipeShower,lookAt,target.transform);
        }
    }

    public static void Explosion(Vector3 position, string tag,GameObject ignore,float explodingRadius){
        if(tag == "Barrel"){
            GameObject part= Instantiate(particleExplosion, position, Quaternion.identity);
            Collider[] colliders = Physics.OverlapSphere(position,explodingRadius,explosionAffected);
            foreach (Collider col in colliders)
            {
                Debug.Log(col.tag + "    "+ col.name);
                if(col.transform.gameObject != ignore)
                {
                    Debug.Log(col.tag + "    "+ col.name);
                    if(col.CompareTag("Enemy") || col.CompareTag("Player") || col.CompareTag("Barrel")){
                        //Debug.Log(col.name);
                        try
                        {
                            GameMaster.CalculateDMG(col.transform.gameObject,  50);
                        }
                        catch (System.Exception)
                        {
                            
                        }
                    }

                    if(col.gameObject.layer == 12)
                        GameMaster.Destructable(col.gameObject, col.tag, col.transform.position,Vector3.zero);
                }
            }
        }
    }

    public static void CreateParticle(Vector3 pointOfInpact, GameObject particle,Vector3 lookAt,Transform target){
        if(particle ==null){
            GameObject part= Instantiate(particleBulletImpact, pointOfInpact,Quaternion.identity,target);
            part.transform.LookAt(lookAt);

        }else
        {
            GameObject part= Instantiate(particle, pointOfInpact,Quaternion.identity,target);
            part.transform.LookAt(lookAt);
        }

        //Vector3 direction = part.transform.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        //part.transform.rotation = rotation;
    }

    public static void EnemyGotDMG(){
        if(!botsAwareOfPlayer){
            foreach (var enemy in allEnemies)
            {
                if(enemy.playerFound== false)
                    enemy.FoundPlayer(GameObject.Find("Player").transform);
            }
            foreach (var enemy in allFlying)
            {
                enemy.PlayerFound();
            }
            botsAwareOfPlayer = true;
        }
            
    }

    public static void ShootLight(GameObject target, Vector3 pointOfInpact){
        if(target.CompareTag("Light")){
            Vector3 direction = pointOfInpact - target.transform.position;
            target.GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized*500,target.transform.position);
        }
    }


    public static void CanGateOpen(){
        if(allEnemies.Count==0 && allFlying.Count==0){
            foreach (Transform child in gate.transform)
            {
                if(child.CompareTag("OpenGate"))
                    child.gameObject.SetActive(false);
                if(child.CompareTag("NextRoom"))
                    child.gameObject.SetActive(true);
            }
        }
    }

    public static void NextRoom(){
        foreach (Transform child in curentRoom)
        {
            child.gameObject.SetActive(false);
        }
        try
        {
            int index = Random.Range(0, allRooms.Count);
            player.GetComponent<CharacterController>().enabled=false;
            player.transform.position = allRooms[index].spawnPoint.transform.position;
            player.GetComponent<CharacterController>().enabled=true;
            Debug.Log("Next Room !" + player.name+ "   " +index);
        }
        catch (System.Exception)
        {
            Debug.Log("No more rooms game ended");
            MenuAndSettings.instance.EndGame("Finished");
        }
    }

    public static void SetMouseSpeed(float speedModifier){
        try
        {
            player.GetComponentInChildren<MouseLook>().mouseSpeedModifier = speedModifier;
            Debug.Log("Its set");
        }
        catch (System.Exception)
        {
            
            Debug.Log("Someting went wrong");
        }
        
    }

    public static void ESC(){
        if(!MenuAndSettings.instance.blockESC){
            if(MenuAndSettings.instance.menu.activeSelf == true){
                MenuAndSettings.instance.menu.SetActive(false);
                MenuAndSettings.instance.hud.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                SetMouseSpeed(MenuAndSettings.instance.mouseSpeedModifier);
                Time.timeScale = 1;
            }

            else
            {
                MenuAndSettings.instance.menu.SetActive(true);
                MenuAndSettings.instance.hud.SetActive(false);
                Cursor.lockState = CursorLockMode.Confined;
                Time.timeScale = 0;
            }
        }
        
    }
}
