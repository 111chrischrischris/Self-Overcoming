using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedUpArea : MonoBehaviour
{

    bool spedup = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!spedup)
        {
            playerScript.Instance.speed += 10; //increment the speed if we havent already
            spedup = true;


        }
    }
}
