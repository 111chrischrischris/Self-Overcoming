using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class keysText : MonoBehaviour
{
    Text myText;


    private void Awake()
    {
       myText =  GetComponent<Text>();
    }
    void Update()
    {
        myText.text = "Keys: " + GameManager.Instance.keyCount.ToString(); 
    }
}
