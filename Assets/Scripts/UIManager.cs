using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject continueButton;
    public void ToggleDeathScreen()
    {
        deathScreen.SetActive(!deathScreen.activeSelf); 
    }
    public GameObject GetContinueButton()
    {
        return continueButton.gameObject;
    }
}
