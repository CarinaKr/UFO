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
    private GameObject viewInstructions;

    public UIFader faderMain;
    public UIFader faderAlt;

    public int levelToLoad;

    void Awake()
    {
        StartCoroutine(faderAlt.FadeOut());
        scorelist = GameObject.Find("Highscore List");
        instructions = GameObject.Find("Instructions");
        startGame = GameObject.Find("StartGame");
        viewInstructions = GameObject.Find("ViewInstructions");
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
    }

    private IEnumerator StartHandler()
    {
        // If the camera is already fading, ignore.
        if (m_CameraFade.IsFading)
            yield break;
        
        // Wait for the camera to fade out.
        yield return StartCoroutine(m_CameraFade.BeginFadeOut(true));

        // Load the level.
        SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
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
}
