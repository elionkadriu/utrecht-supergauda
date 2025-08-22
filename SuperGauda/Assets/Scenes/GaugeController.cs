using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    [Header("Players")]
    public Transform p1;                 // Player1_Human
    public Transform p2;                 // Player2_Cheese

    [Header("UI")]
    public Slider lactoseGauge;          // 0..1
    public Slider miceGauge;             // 0..1

    [Header("Distance thresholds (world units)")]
    public float closeThreshold = 2.5f;  // lactose rises if distance < this
    public float farThreshold   = 6.0f;  // mice rises if distance > this

    [Header("Times to fill (seconds)")]
    public float lactoseFillTime = 40f;  // 40s to lose if too close
    public float miceFillTime    = 30f;  // 30s to lose if too far

    [Header("Drain speeds (seconds back to empty)")]
    public float lactoseDrainTime = 28f; // how fast it empties when safe
    public float miceDrainTime    = 25f;

    void Update()
    {
        if (!p1 || !p2 || !lactoseGauge || !miceGauge) return;

        float d = Vector2.Distance(p1.position, p2.position);

        // --- Lactose: close is bad ---
        bool lactoseActive = (d < closeThreshold);
        float lDelta = (lactoseActive ? +Time.deltaTime / Mathf.Max(lactoseFillTime, 0.01f)
                                      : -Time.deltaTime / Mathf.Max(lactoseDrainTime, 0.01f));
        lactoseGauge.value = Mathf.Clamp01(lactoseGauge.value + lDelta);

        // --- Mice: far is bad ---
        bool miceActive = (d > farThreshold);
        float mDelta = (miceActive ? +Time.deltaTime / Mathf.Max(miceFillTime, 0.01f)
                                   : -Time.deltaTime / Mathf.Max(miceDrainTime, 0.01f));
        miceGauge.value = Mathf.Clamp01(miceGauge.value + mDelta);

        // Game over checks
        if (lactoseGauge.value >= 1f) TriggerGameOver("Lactose overload!");
        if (miceGauge.value    >= 1f) TriggerGameOver("Swarmed by mice!");
    }

    void TriggerGameOver(string reason)
{
    var ui = FindObjectOfType<GameOverUI>();
    if (ui) ui.Show(reason);
    else Debug.Log("GAME OVER: " + reason);
    enabled = false; // stop updating bars
}
}