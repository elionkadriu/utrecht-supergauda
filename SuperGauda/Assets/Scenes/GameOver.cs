using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour
{
    bool over;

    public void Trigger(string reason)
    {
        if (over) return;
        over = true;
        Debug.Log("GAME OVER: " + reason);
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.0f);  // short pause
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
    
}