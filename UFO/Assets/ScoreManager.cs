using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private static int _score = 0;
    
    public Text scoreText;

    public static void increaseScore(int score)
    {
        _score += score;
        //scoreText.text = "Score: " + _score;
    }

    void Start()
    {
        StartCoroutine("UpdateScore");
    }

    IEnumerator UpdateScore()
    {
        while(true)
        {
            scoreText.text = "Score: " + _score;
            yield return new WaitForSeconds(1f);
        }
    }
}
