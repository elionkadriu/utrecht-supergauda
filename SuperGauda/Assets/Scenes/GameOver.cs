using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Trigger(string reason)
    {
        Debug.Log("Game Over: " + reason);
        // For now just reload so you can keep testing:
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}