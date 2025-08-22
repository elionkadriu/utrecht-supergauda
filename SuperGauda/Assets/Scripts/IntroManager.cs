using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    [Header("Flow")]
    public float introDuration = 5f;       // auto-continue after this many seconds
    public string nextSceneName = "MainGame"; // set to your main scene name
    public float minSkipDelay = 0.5f;      // avoid accidental skip right at start

    [Header("Optional Fade (CanvasGroup on IntroCanvas)")]
    public CanvasGroup canvasGroup;        // drag IntroCanvas here
    public float fadeInTime = 0.6f;
    public float fadeOutTime = 0.5f;

    float t;

    void Start()
    {
        if (canvasGroup) { canvasGroup.alpha = 0f; StartCoroutine(Fade(canvasGroup, 0f, 1f, fadeInTime)); }
    }

    void Update()
    {
        t += Time.deltaTime;

        if (t >= introDuration || (t >= minSkipDelay && Input.anyKeyDown))
        {
            StartCoroutine(GoNext());
        }
    }

    IEnumerator GoNext()
    {
        enabled = false; // stop Update from running twice
        if (canvasGroup) yield return Fade(canvasGroup, canvasGroup.alpha, 0f, fadeOutTime);
        var scene = string.IsNullOrEmpty(nextSceneName) ? SceneManager.GetActiveScene().buildIndex + 1 : -1;
        if (scene >= 0) SceneManager.LoadScene(scene);
        else SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator Fade(CanvasGroup cg, float a, float b, float time)
    {
        float x = 0f;
        while (x < time)
        {
            x += Time.deltaTime;
            cg.alpha = Mathf.Lerp(a, b, x / time);
            yield return null;
        }
        cg.alpha = b;
    }

    // Optional: public method for a Skip button
    public void SkipNow() { StartCoroutine(GoNext()); }
}