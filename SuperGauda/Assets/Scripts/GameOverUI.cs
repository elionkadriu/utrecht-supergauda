using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if TMP_PRESENT || UNITY_TEXTMESHPRO
using TMPro;
#endif

public class GameOverUI : MonoBehaviour
{
    [Header("Hook these")]
    public Canvas gameOverCanvas;     // GameOverCanvas
#if TMP_PRESENT || UNITY_TEXTMESHPRO
    public TMP_Text reasonTMP;        // ReasonText (TMP)
#else
    public Text reasonText;           // ReasonText (legacy)
#endif
    public Button retryButton;        // RetryButton
    public Button quitButton;         // optional

    bool shown;

    void Awake()
    {
        // Ensure hidden on start
        if (gameOverCanvas) gameOverCanvas.enabled = false;
        if (retryButton) retryButton.onClick.AddListener(OnRetry);
        if (quitButton)  quitButton.onClick.AddListener(OnQuit);
    }

    public void Show(string reason)
    {
        if (shown) return;
        shown = true;

        // pause gameplay; UI still works while timeScale is 0
        Time.timeScale = 0f;

        if (gameOverCanvas) gameOverCanvas.enabled = true;

#if TMP_PRESENT || UNITY_TEXTMESHPRO
        if (reasonTMP) reasonTMP.text = reason;
#else
        if (reasonText) reasonText.text = reason;
#endif
    }

    public void OnRetry()
    {
        Time.timeScale = 1f;
        var s = SceneManager.GetActiveScene();
        SceneManager.LoadScene(s.buildIndex);
    }

    public void OnQuit()
    {
        Time.timeScale = 1f;
        // If you don’t have a menu scene, fallback to reloading:
        var s = SceneManager.GetActiveScene();
        SceneManager.LoadScene(s.buildIndex);
        // or Application.Quit() for a build
        // Application.Quit();
    }
}