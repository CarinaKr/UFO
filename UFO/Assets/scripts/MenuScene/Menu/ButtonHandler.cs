using System.Collections;
using System.Collections.Generic;
using VRStandardAssets.Utils;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonHandler : MonoBehaviour {

    [SerializeField]
    private VRCameraFade m_CameraFade;

    public int levelToLoad;

    public void DetermineHandler(string handlerName)
    {
        if (handlerName.Equals("Start"))
        {
            StartCoroutine(StartHandler());
        }
        else if (handlerName.Equals("View Instructions"))
        {

        }
        else if (handlerName.Equals("View Highscore"))
        {

        }
        else if (handlerName.Equals("Exit"))
        {

        }
        else if (handlerName.Equals("Return to Main"))
        {

        }
    }

    private IEnumerator StartHandler()
    {
        Debug.Log("In Cor");
        // If the camera is already fading, ignore.
        if (m_CameraFade.IsFading)
            yield break;

        Debug.Log("In Cor2");
        // Wait for the camera to fade out.
        yield return StartCoroutine(m_CameraFade.BeginFadeOut(true));

        // Load the level.
        SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
    }
}
