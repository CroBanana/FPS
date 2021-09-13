using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    private ParticleSystem particle;
    public bool canParticleDMG;
    public int particleDMG;

    // Start is called before the first frame update
    void Start()
    {
        particle = gameObject.GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other) {
        Debug.Log("particle COllision");
        if(other.layer ==8 || other.layer == 7){
            Debug.Log("FlameDMG");
            if(canParticleDMG)
                GameMaster.CalculateDMG(other,particleDMG);
        }
    }
}
