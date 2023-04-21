using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneLoader : MonoBehaviour
{
     public void Instructions()
    {
        Initiate.Fade("Instructions", Color.black, 3f);
    }
    public void BackFromInstructions()
    {
        Initiate.Fade("Main Menu", Color.black, 3f);
    }

}
