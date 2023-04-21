using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class timer : MonoBehaviour
{

    public GameObject textDisplay;
    public int secondsLeft;
    bool takingAway = false;
    int currMinute = 1;

    public void Start()
    {
        textDisplay.GetComponent<Text>().text = "1:" + secondsLeft.ToString();
        

    }

    public void Update()
    {
        if (!takingAway && secondsLeft > 0 && currMinute >= 0) //decrease time as long as its not 0
        {
            StartCoroutine(TimerTake($"{currMinute}:"));
        }
        else if (!takingAway && secondsLeft > 0 && currMinute == -1) //if we are at the final minute and second then we display "00:00"
        {

            textDisplay.GetComponent<Text>().text = "00:00";
            GameManager.Instance.Timeout(); //tell the player that the time is out
        }
        

            
    }
    IEnumerator TimerTake(string firstNum)
    {
        takingAway = true;

        yield return new WaitForSeconds(1f); //waiting for 1 second
        secondsLeft--;
        if(secondsLeft < 10)
        {

            textDisplay.GetComponent<Text>().text = firstNum + "0" + secondsLeft.ToString(); //extra zero for when the seconds are less than 10
        }
        else
        {
        textDisplay.GetComponent<Text>().text = firstNum + secondsLeft.ToString(); //displaying the text

        }
        if (secondsLeft < 1) //if the current minute is finished then we decrmement the minutes variable and restart the seconds
        {
            currMinute--;
            secondsLeft = 60;
        }
        
        takingAway = false;
    
    }


}
