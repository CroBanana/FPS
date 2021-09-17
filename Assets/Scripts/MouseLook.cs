using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float ads =1;
    public float mouseSpeedModifier;
    public Transform playerBody;
    public Transform hands;

    float xRotation ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mousex = Input.GetAxis("Mouse X") * mouseSpeedModifier * ads ;
        float mouseY = Input.GetAxis("Mouse Y")* mouseSpeedModifier *ads;
        
        xRotation -=mouseY;
        xRotation = Mathf.Clamp(xRotation,-90f,90f);

        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);

        playerBody.Rotate(Vector3.up * mousex);

    }
}
