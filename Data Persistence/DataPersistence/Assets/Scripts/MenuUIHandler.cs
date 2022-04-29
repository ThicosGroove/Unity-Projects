using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public InputField input;

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif

    }

    public void KeepCurrentName()
    {
        string s;
        s = input.text;

        PlayerDataHandler.Instance.PlayerName = s;
    }
}
