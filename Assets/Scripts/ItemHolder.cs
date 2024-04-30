using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Key;

public class ItemHolder : MonoBehaviour
{
    private List<Key.KeyType> keyList;
    public GameObject keyWarning;
    public GameObject redKeyWarning;
    public GameObject greenKeyWarning;
    public GameObject blueKeyWarning;
    public TextMeshProUGUI keyWarningText;
    private void Awake()
    {
        keyList = new List<Key.KeyType>();
    }
    public void AddKey(Key.KeyType keyType)
    {
        Debug.Log("Added key: " + keyType);
        keyList.Add(keyType);
    }
    public bool ContainsKey(Key.KeyType keyType)
    {
        return keyList.Contains(keyType);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Key key = collider.GetComponent<Key>();
        if (key != null)
        {
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
        }
        else
        {
            KeyCheck keyCheck = collider.GetComponent<KeyCheck>();
            if (keyCheck != null)
            {
                if (ContainsKey(keyCheck.GetKeyType()))
                {
                    keyCheck.OpenDoor();
                }
                else
                {
                    UpdateKeyWarningText();
                }
            }
        }
    }
    private void UpdateKeyWarningText()
    {
        if (!ContainsKey(Key.KeyType.Red))
        {
            
            keyWarningText.text = "Missing Red Key!";
        }
        if (!ContainsKey(Key.KeyType.Green))
        {
            keyWarningText.text = "Missing Green Key!";
        }
        if (!ContainsKey(Key.KeyType.Blue))
        {
            keyWarningText.text = "Missing Blue Key!";
        }
    }
}