using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform spawnPoint;
    public List<FlyingRobot> roomEnemiesFlying;
    public List<RobotGround> roomEnemiesGround;
    public GameObject nextRoomGate;
    // Start is called before the first frame update
    void Awake()
    {
        nextRoomGate = transform.Find("NextRoomGate").gameObject;
        spawnPoint = transform.Find("PlayerSpawn");
        foreach (FlyingRobot robot in transform.GetComponentsInChildren<FlyingRobot>())
        {
            roomEnemiesFlying.Add(robot);
        }
        foreach (RobotGround robot in transform.GetComponentsInChildren<RobotGround>())
        {
            roomEnemiesGround.Add(robot);
        }
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        Debug.Log(roomEnemiesFlying.Count + "  flying");
        Debug.Log(roomEnemiesGround.Count + "  Ground");
    }

    private void OnTriggerEnter(Collider other) {
        if(other.name =="Player"){
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
                GameMaster.curentRoom = transform;
                GameMaster.allEnemies = roomEnemiesGround;
                GameMaster.allFlying = roomEnemiesFlying;
                GameMaster.gate = nextRoomGate;
                GameMaster.allRooms.Remove(this);
            }
        }
    }
}
