using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportTarget;
    public PlayerController playerController;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && playerController.isSpiderForm)
        {
            FindObjectOfType<AudioManager>().Play("Small steps…_Short");
            other.transform.position = teleportTarget.position;
        }
    }
}
