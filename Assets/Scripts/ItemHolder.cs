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
    public float warningDisplayDuration = 5f;
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
                    if (!ContainsKey(Key.KeyType.Red))
                    {
                        LevelManager.instance.KeyWarning();
                        keyWarningText.text = "Missing Red Key!";
                        StartCoroutine(DeactivateTextAfterDelay(keyWarningText, 5f));
                    }
                    else if (!ContainsKey(Key.KeyType.Green))
                    {
                        LevelManager.instance.KeyWarning();
                        keyWarningText.text = "Missing Green Key!";
                        StartCoroutine(DeactivateTextAfterDelay(keyWarningText, 5f));
                    }
                    else if (!ContainsKey(Key.KeyType.Blue))
                    {
                        LevelManager.instance.KeyWarning();
                        keyWarningText.text = "Missing Blue Key!";
                        StartCoroutine(DeactivateTextAfterDelay(keyWarningText, 5f));
                    }
                }
            }
        }
    }
    IEnumerator DeactivateTextAfterDelay(TextMeshProUGUI textElement, float delay)
    {
        yield return new WaitForSeconds(delay);
        textElement.gameObject.SetActive(false);
    }

}