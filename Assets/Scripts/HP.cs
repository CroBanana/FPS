using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public float hp =100;
    private float startHP;
    private bool exploadable;
    public float explodingRadius =6;

    // Start is called before the first frame update
    private void Start() {
        startHP=hp;
        if(transform.CompareTag("Barrel"))
            exploadable=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(exploadable){
            if(hp<=0){
                if(gameObject.CompareTag("Barrel")){
                    Vector3 position = transform.position;
                    GameMaster.Explosion(position,transform.tag,gameObject,explodingRadius);
                    Destroy(gameObject);
                }
            }
            if(hp<startHP){
                hp-=Time.deltaTime*5;
            }
        }

    }

    public void TakeDmg(int dmgAmount){
        hp=hp-dmgAmount;
        Debug.Log(hp+ "   "+ gameObject.name);
        if(hp<=0 && !transform.CompareTag("Barrel")){

            if(gameObject.layer ==18)
                gameObject.GetComponent<DestructableArmor>().ArmorDestroyed();
            else
                Destroy(gameObject);

        }
    }
}
