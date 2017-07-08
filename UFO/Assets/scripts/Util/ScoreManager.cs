using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private static int _score = 0;

    public bool isMenu;
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
        if(isMenu)
        {
            getCurrentHSL();
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

    //aktuelle Highscores holen
    void getCurrentHSL()
    {
        List<Attempt> list = hsl.scoreList;
        for (int i = 0; i < list.Count; i++)
        {
            players[list.Count-i-1].text = "" + list[i].name;
            scores[list.Count-i-1].text = "" + list[i].score;
        }
    }

    public void AddCurrentAttempt(Attempt att)
    {
        hsl.AddScore(att);
        getCurrentHSL();
    }

    public void SaveScoreList()
    {
        hsl.SaveScore();
    }
}
