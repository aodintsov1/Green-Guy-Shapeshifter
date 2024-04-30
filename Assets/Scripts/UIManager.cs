using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject keyWarning;
    [SerializeField] GameObject continueButton;
    public void ToggleDeathScreen()
    {
        deathScreen.SetActive(!deathScreen.activeSelf); 
    }
    public void ToggleKeyWarning()
    {
        keyWarning.SetActive(!keyWarning.activeSelf);
    }
    public GameObject GetContinueButton()
    {
        return continueButton.gameObject;
    }
}
