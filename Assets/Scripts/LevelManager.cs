using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private void Awake()
    {
        if (LevelManager.instance == null) instance = this;
        else Destroy(gameObject);
    }
    public void GameOver()
    {
        UIManager _ui = GetComponent<UIManager>();
        if (_ui != null)
        {
            _ui.ToggleDeathScreen();
            GameObject continueButton = _ui.GetContinueButton();
            if (continueButton != null)
            {
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(continueButton);
            }
        }
    }
    public void KeyWarning()
    {
        UIManager _ui = GetComponent<UIManager>();
        if (_ui != null)
        {
            _ui.ToggleKeyWarning();
        }
    }
}
