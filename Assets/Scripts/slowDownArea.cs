using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowDownArea : MonoBehaviour
{
    bool slowedDown = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!slowedDown)
        {
            playerScript.Instance.speed -= 5;
            slowedDown = true;
        } 
    }

}
