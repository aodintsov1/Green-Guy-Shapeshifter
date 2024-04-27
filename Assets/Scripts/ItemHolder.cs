using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    private List<Key.KeyType> keyList;
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
        KeyCheck keyCheck = collider.GetComponent<KeyCheck>();
        if (keyCheck != null)
        {
            if (ContainsKey(keyCheck.GetKeyType()))
            {
                keyCheck.OpenDoor();
            }
        }
    }
}
