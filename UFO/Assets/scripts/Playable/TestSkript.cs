using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;


public class TestSkript : MonoBehaviour {

    [SerializeField] private SelectionRadial m_SelectionRadial;
    [SerializeField] private VRInteractiveItem m_InteractiveItem;

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
            Debug.Log("Action Complete");
    }
}
