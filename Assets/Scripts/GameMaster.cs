using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;

    private void Awake() {
        if(Instance != null){
            Debug.Log("There is more then 1 instance");
            return;
        }
        Instance = this;
    }


    // Start is called before the first frame update
    public static void CalculateDMG(GameObject target,int DMGAmount){
        target.GetComponent<HP>().TakeDmg(DMGAmount);
    }
}
