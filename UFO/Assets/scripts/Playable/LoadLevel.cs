using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

    public int levelToLoad;

    [SerializeField] private SelectionRadial m_SelectionRadial;
    [SerializeField] private VRInteractiveItem m_InteractiveItem;
    [SerializeField] private VRCameraFade m_CameraFade;

    private bool m_GazeOver;

    private void OnEnable()
    {
        m_InteractiveItem.OnOver += HandleOver;
        m_InteractiveItem.OnOut += HandleOut;
        m_SelectionRadial.OnSelectionComplete += HandleSelectionComplete;
    }

    private void OnDisable()
    {
        m_InteractiveItem.OnOver -= HandleOver;
        m_InteractiveItem.OnOut -= HandleOut;
        m_SelectionRadial.OnSelectionComplete -= HandleSelectionComplete;
    }

    private void HandleOver()
    {
        m_SelectionRadial.Show();

        m_GazeOver = true;
    }

    private void HandleOut()
    {
        m_SelectionRadial.Hide();

        m_GazeOver = false;
    }

    private void HandleSelectionComplete()
    {
        if (m_GazeOver)
            StartCoroutine(ActivateButton());
    }

    private IEnumerator ActivateButton()
    {
        // If the camera is already fading, ignore.
        if (m_CameraFade.IsFading)
            yield break;

        // Wait for the camera to fade out.
        yield return StartCoroutine(m_CameraFade.BeginFadeOut(true));

        // Load the level.
        SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
    }
}
