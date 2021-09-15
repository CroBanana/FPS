using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightJoints : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (HingeJoint child in transform.GetComponentsInChildren<HingeJoint>())
        {
            child.connectedBody = child.transform.parent.transform.GetComponent<Rigidbody>();
           // Debug.Log(child.gameObject.name);
        }
    }

    
}
