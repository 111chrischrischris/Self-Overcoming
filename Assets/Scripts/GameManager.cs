using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    // Start is called before the first frame update
    public GameObject timeoutText;
    public int keyCount = 0;


    void Awake ()   
       {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy (gameObject);
        }
      }


    void Start()
    {
        Instance = this;

                
    }

   
    public void Respawn()
    {
      
        Initiate.Fade(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex).name, Color.black, 2.5f); //reload the scene
    }

    public void NextLevel()
    {
        /*There is currently a bug in unity where for some reason sometimes scenes dont save in the build settings so instead we 
        have to work around this by looking at the last digit in each scene name
        */
        string currSceneName = SceneManager.GetActiveScene().name; //current scene name
        if (currSceneName.Contains("Level"))
        {
            try
            {

                Initiate.Fade($"Level{ int.Parse((currSceneName[(currSceneName.Length - 1)]).ToString()) + 1}", Color.black, 1f); //We go to the next level
            }
            catch
            {
                Initiate.Fade("Main Menu", Color.black, 1f); //If there is not next level, then we go back to the main menu
            }
        }
        else if (currSceneName.Contains("Main Menu"))
        {
            Initiate.Fade("Level1", Color.black, 1f); //If we pressed play from the main menu then the only option is to proceed to level 1
        }

    }

    public void Timeout()
    {
        timeoutText = GameObject.Find("time out text");
        timeoutText.SetActive(true);
        StartCoroutine(GO_setter(timeoutText, 2f,false));
        Invoke("Respawn", 2f);
    }


    //This Coroutiner sets any given gameobject's "SetActive" to a certain booolean after a duration
    public IEnumerator GO_setter(GameObject GO_toinvert, float duration, bool onoff)
    {
        yield return new WaitForSeconds(duration);
        GO_toinvert.SetActive(onoff);
    }
    

     }
