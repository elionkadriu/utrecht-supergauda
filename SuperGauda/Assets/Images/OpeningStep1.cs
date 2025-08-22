using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using TMPro;

public class OpeningStep1 : MonoBehaviour
{
    [Header("UI - Opening")]
    public CanvasGroup blackPanel;     // CanvasGroup on BlackPanel (drag this!)
    public TMP_Text openingText;       // “It’s gonna be fine…”
    public Image castleImage;          // Optional: your PNG with the paragraph
    public TMP_Text castleText;        // Optional: live TMP paragraph

    [Header("UI - Dialogue")]
    public GameObject dialoguePanel;   // Bottom bar (disabled by default)
    public TMP_Text dialogueText;      // Text inside dialogue panel

    [Header("Timeline (optional)")]
    public PlayableDirector director;  // If you have one, drag it here

    [Header("Timings")]
    public float textFadeSeconds = 1f;
    public float fadeToSceneSeconds = 0.6f;

    [TextArea(3,10)]
    public string[] dialogueLines = new string[]
    {
        "[Approaching the castle. Wind. Distant cackling. The doors loom.]",
        "A: Welpo, here we are.",
        "B: Yup.",
        "A: Guess… we should go in?",
        "B: Yup.",
        "A: I mean, nobody’s ever died in there, right? Maybe it’s not that bad.",
        "B: You don’t think it’s weird the last few “volunteers” never talk about it after?",
        "A: Maybe they don’t want to spoil the fun?",
        "[They reach the door.]",
        "B: Uh-huh. Let’s just go in, fail fast, and go home.",
        "A: No—don’t you want your wish granted?",
        "B: I wish I didn’t have to do this.",
        "A: Fair.",
        "[They step inside. Click. A floor tile depresses. Fog erupts around B.]",
        "B: What the—?!",
        "[The fog clears. B is now… a cheese wheel.]",
        "A: What the—",
        "B: …See! This is why people DON’T want to come here!",
        "A: This is bad.",
        "B: Yeah, no shit, Sherlock—I’m a sentient cheese wheel and this dump is probably riddled with mice!",
        "A: I don’t mind chasing mice, but that’s not what I mean. I… I…",
        "[A’s face flushes. Eyes water. Wheeze.]",
        "A: I’m lactose intolerant.",
        "B: …What.",
        "A: I can’t stay too close to dairy or I break out in hives. Prolonged exposure might actually kill me.",
        "B: You’re kidding.",
        "A: Our only choice is to make it to the top and ask the witch to turn you back!",
        "B: Wait, let’s not be hasty, I’m sure there are other—",
        "A: Onwards, partner! This is our only shot!",
        "B: …I hate you."
    };

    int _lineIndex = 0;
    bool _waitingForInput = false;

    private IEnumerator Start()
    {
        // --- initial states ---
        if (blackPanel) blackPanel.alpha = 1f;
        if (openingText) { openingText.gameObject.SetActive(true); openingText.alpha = 0f; }
        if (castleImage) castleImage.gameObject.SetActive(false);
        if (castleText)  castleText.gameObject.SetActive(false);
        if (dialoguePanel) dialoguePanel.SetActive(false);

        // 1) black → fade in opening text
        if (openingText) yield return FadeTMP(openingText, 1f, textFadeSeconds);

        // wait for any key
        yield return WaitForAnyKey();

        // 2) show the castle screen (PNG and/or live text)
        if (openingText) openingText.gameObject.SetActive(false);
        if (castleImage) castleImage.gameObject.SetActive(true);
        if (castleText)  castleText.gameObject.SetActive(true);

        // wait again for any key
        yield return WaitForAnyKey();

        // hide castle screen
        if (castleImage) castleImage.gameObject.SetActive(false);
        if (castleText)  castleText.gameObject.SetActive(false);

        // 3) fade the black overlay away (reveals your 2D scene)
        if (blackPanel)
        {
            yield return FadeCanvasGroup(blackPanel, 0f, fadeToSceneSeconds);
            blackPanel.blocksRaycasts = false;
            blackPanel.gameObject.SetActive(false);
        }

        // 4) start Timeline if you have one (optional)
        if (director != null)
        {
            director.time = 0;
            SetTimelineSpeed(1);
            director.Play();

            // Safety: show the first line even if signals aren't wired yet.
            // Delete this line later when your Timeline signals are working.
            Invoke(nameof(TimelineCue_NextLine), 0.25f);
        }
        else
        {
            // No Timeline? Just start the dialogue immediately.
            ShowNextLine_NoTimeline();
        }
    }

    // === CALLED BY TIMELINE SIGNALS ===
    public void TimelineCue_NextLine()
    {
        // Pause Timeline while the line is on screen
        PauseTimeline();
        ShowNextLinePanel();
    }

    // === NO-TIMELINE path ===
    void ShowNextLine_NoTimeline()
    {
        ShowNextLinePanel(); // same UI, just without pausing anything
    }

    void ShowNextLinePanel()
    {
        if (dialoguePanel) dialoguePanel.SetActive(true);

        if (dialogueText && _lineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[_lineIndex++];
        }
        else
        {
            // End of lines — hide panel and (optionally) load next scene here
            if (dialoguePanel) dialoguePanel.SetActive(false);
        }

        _waitingForInput = true;
    }

    void Update()
    {
        // Advance dialogue on any key
        if (_waitingForInput && Input.anyKeyDown)
        {
            _waitingForInput = false;

            // Hide panel if there are no more lines; otherwise keep it visible
            if (_lineIndex >= dialogueLines.Length)
            {
                if (dialoguePanel) dialoguePanel.SetActive(false);
                ResumeTimeline(); // harmless if no timeline
            }
            else
            {
                if (director != null) PauseTimeline(); // keep paused between lines
                ShowNextLinePanel();
                return;
            }

            // Resume Timeline after each line when using signals
            ResumeTimeline();
        }

        // Optional skip key (Esc)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (dialoguePanel) dialoguePanel.SetActive(false);
            if (director != null)
            {
                SetTimelineSpeed(1);
                director.time = director.duration;
            }
            if (blackPanel) blackPanel.gameObject.SetActive(false);
        }
    }

    // --- helpers ---
    IEnumerator WaitForAnyKey()
    {
        yield return null; // don't catch the key that started Play mode
        while (!Input.anyKeyDown) yield return null;
    }

    IEnumerator FadeTMP(TMP_Text t, float target, float dur)
    {
        float start = t.alpha, time = 0f;
        while (time < dur)
        {
            t.alpha = Mathf.Lerp(start, target, time / dur);
            time += Time.deltaTime;
            yield return null;
        }
        t.alpha = target;
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float target, float dur)
    {
        float start = cg.alpha, time = 0f;
        while (time < dur)
        {
            cg.alpha = Mathf.Lerp(start, target, time / dur);
            time += Time.deltaTime;
            yield return null;
        }
        cg.alpha = target;
    }

    void PauseTimeline()  { if (director != null) { var r = director.playableGraph.GetRootPlayable(0); if (r.IsValid()) r.SetSpeed(0); } }
    void ResumeTimeline() { if (director != null) { var r = director.playableGraph.GetRootPlayable(0); if (r.IsValid()) r.SetSpeed(1); } }
    void SetTimelineSpeed(double s) { if (director != null) { var r = director.playableGraph.GetRootPlayable(0); if (r.IsValid()) r.SetSpeed(s); } }
}
