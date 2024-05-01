using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public PlayerController playerController;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && playerController.isFishForm)
        {
            LevelManager.instance.GameEnd();
            gameObject.SetActive(false);
        }
    }
}