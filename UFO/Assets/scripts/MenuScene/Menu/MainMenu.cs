using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string zLevel;

    public void startLevel(string pLevel)
    {
        //Application.LoadLevel(pLevel);
        SceneManager.LoadScene(pLevel);
    }
}
