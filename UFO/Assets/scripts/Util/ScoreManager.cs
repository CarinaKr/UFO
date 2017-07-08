using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private static int _score = 0;

    public bool isMainMenu;
    public Text scoreText;

    public HighScoreList hsl;
    public Text[] players;
    public Text[] scores;

    public static void increaseScore(int score)
    {
        _score += score;
        //scoreText.text = "Score: " + _score;
    }

    void Awake()
    {
        if(isMainMenu)
        {
            //aktuelle Highscores holen
            List<Attempt> list = hsl.scoreList;
            for(int i = 0; i < list.Count; i++)
            {
                players[i].text = "" + list[i].name;
                scores[i].text = "" + list[i].score;
            }
        }
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
