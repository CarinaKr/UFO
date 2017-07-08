using System.Collections;
using System.Collections.Generic;
using VRStandardAssets.Utils;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonHandler : MonoBehaviour {

    [SerializeField]
    private VRCameraFade m_CameraFade;
    private GameObject scorelist;
    private GameObject instructions;
    private GameObject startGame;
    private GameObject submit;
    private GameObject viewInstructions;

    public UIFader faderMain;
    public UIFader faderAlt;

    public int gameLevelNumber;
    public int menuLevelNumber;
    public bool isGameOver;

    void Awake()
    {
        scorelist = GameObject.Find("Highscore List");
        StartCoroutine(faderAlt.FadeOut());
        if (!isGameOver)
        {
            instructions = GameObject.Find("Instructions");
            startGame = GameObject.Find("StartGame");
            viewInstructions = GameObject.Find("ViewInstructions");
        }
        else
        {
            submit = GameObject.Find("Submit");
        }
        
    }

    public void DetermineHandler(string handlerName)
    {
        if (handlerName.Equals("Start"))
        {
            StartCoroutine(StartHandler());
        }
        else if (handlerName.Equals("View Instructions"))
        {
            StartCoroutine(ViewInstructions());
        }
        else if (handlerName.Equals("View Highscore"))
        {
            StartCoroutine(ViewHighScore());
        }
        else if (handlerName.Equals("Exit"))
        {
            Application.Quit();
        }
        else if (handlerName.Equals("Return to Main"))
        {
            StartCoroutine(BackToMain());
        }
        else if (handlerName.Equals("SubmitScore"))
        {
            StartCoroutine(SubmitScore());
        }
        else if (handlerName.Equals("Finish"))
        {
            StartCoroutine(FinishGame());
        }
    }

    private IEnumerator StartHandler()
    {
        // If the camera is already fading, ignore.
        if (m_CameraFade.IsFading)
            yield break;
        
        // Wait for the camera to fade out.
        yield return StartCoroutine(m_CameraFade.BeginFadeOut(true));

        // Load the level.
        SceneManager.LoadScene(gameLevelNumber, LoadSceneMode.Single);
    }

    private IEnumerator ViewInstructions()
    {
        StartCoroutine(faderMain.FadeOut());
        yield return new WaitForSeconds(1f);
        startGame.SetActive(false);
        viewInstructions.SetActive(false);
        instructions.SetActive(true);
        scorelist.SetActive(false);
        StartCoroutine(faderAlt.FadeIn());
    }

    private IEnumerator BackToMain()
    {
        StartCoroutine(faderAlt.FadeOut());
        startGame.SetActive(true);
        viewInstructions.SetActive(true);
        yield return new WaitForSeconds(1f);
        StartCoroutine(faderMain.FadeIn());
    }

    private IEnumerator ViewHighScore()
    {
        StartCoroutine(faderMain.FadeOut());
        yield return new WaitForSeconds(1f);
        startGame.SetActive(false);
        viewInstructions.SetActive(false);
        instructions.SetActive(false);
        scorelist.SetActive(true);
        StartCoroutine(faderAlt.FadeIn());
    }

    private IEnumerator SubmitScore()
    {
        //load list and submit score to list
        NameInput input = GameObject.Find("Handles").GetComponent<NameInput>();
        ScoreManager manager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        manager.SendMessage("AddCurrentAttempt", new Attempt(input.playerName, input.playerScore));
        manager.SaveScoreList();
        //fade out menu, wait 1sec, fade in other menu
        submit.SetActive(false);
        StartCoroutine(faderMain.FadeOut());
        yield return new WaitForSeconds(1f);
        StartCoroutine(faderAlt.FadeIn());

    }

    private IEnumerator FinishGame()
    {

        // If the camera is already fading, ignore.
        if (m_CameraFade.IsFading)
            yield break;

        // Wait for the camera to fade out.
        yield return StartCoroutine(m_CameraFade.BeginFadeOut(true));

        // Load the level.
        SceneManager.LoadScene(menuLevelNumber, LoadSceneMode.Single);

        //switch back to main menu again
        //yield return new WaitForSeconds(2f);
    }
}
