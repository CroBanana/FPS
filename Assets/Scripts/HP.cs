using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    int hp =100;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDmg(int dmgAmount){
        hp=hp-dmgAmount;
        Debug.Log(hp);
        if(hp<=0){
            Destroy(gameObject);

        }
    }
}
