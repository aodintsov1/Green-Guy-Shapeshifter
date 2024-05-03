using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/*
public class HealthItem : MonoBehaviour
{
    public TextMeshProUGUI keyWarningText;
    private void OnTriggerEnter2D(Collider2D other)
    {
        KeyCheck keyCheck = GetComponent<KeyCheck>();
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            collision.GetComponent<PlayerHealth>().UpdateHealth(50);
            LevelManager.instance.KeyWarning();
            keyWarningText.text = "Health gained!";
            StartCoroutine(DeactivateTextAfterDelay(keyWarningText, 5f));
        }
    }
    IEnumerator DeactivateTextAfterDelay(TextMeshProUGUI textElement, float delay)
    {
        yield return new WaitForSeconds(delay);
        textElement.gameObject.SetActive(false);
    }
}
*/