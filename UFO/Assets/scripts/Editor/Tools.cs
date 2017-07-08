using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tools
{
    [MenuItem("Tools/Clear PlayerPrefs %g")]
    private static void DelPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}