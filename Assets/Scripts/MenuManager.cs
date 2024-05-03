using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public void ChangeSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void Quitgame()
    {
        Application.Quit();
    }
}