using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCheck : MonoBehaviour
{
    [SerializeField] private Key.KeyType keyType;
    public Key.KeyType GetKeyType()
    {

        return keyType;

    }
    public void OpenDoor()
    {
        FindObjectOfType<AudioManager>().Play("Sci Fi Door");
        gameObject.SetActive(false);
    }
}