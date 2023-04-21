using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camScript : MonoBehaviour
{
    public Transform player;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.1f;
    float offsetY, offsetZ;
    
    
    void Start()
    {
        offsetY = transform.position.y; //Camera starting Y position
        offsetZ = transform.position.z;

    }

    void FixedUpdate()
    {
        //Smoothly move the camera to the player
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, player.position.y + offsetY, offsetZ), ref velocity, smoothTime); 
    }
}
