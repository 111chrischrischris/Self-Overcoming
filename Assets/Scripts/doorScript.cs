using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    public bool activated = false;
    public GameObject parent;
    public GameObject noKeysPanel;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            activated = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            activated = false;
        }
    }


    private void Update()
    {
        if (activated)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(GameManager.Instance.keyCount >= 1)
                {
                    OpenLevel();
                    
                }
                else
                {
                    NoKeys();
                }
            }
        }
    }


    void NoKeys()
    {
        noKeysPanel.SetActive(true);
    }

    void OpenLevel()
    {
        parent.SetActive(false);
    }  public void Backtofirstlevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1); //loads the first level using the index
    }

}
