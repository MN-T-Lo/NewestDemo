using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class UI : MonoBehaviour
{

    private void Start()
    {
        
    }


    public void Load()
    {
        SceneManager.LoadScene("Main");
    }


    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Exit()
    {
        DataPersistence.Instance.Save();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

}
