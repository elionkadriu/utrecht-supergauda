using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    [Header("Players")]
    public Transform player1;            // Human
    public Transform player2;            // Cheese

    [Header("UI")]
    public Slider lactoseGauge;          // 0..1
    public Slider miceGauge;             // 0..1

    [Header("Distance thresholds (world units)")]
    public float closeThreshold = 2.5f;  // lactose rises if distance < this
    public float farThreshold   = 6.0f;  // mice rises if distance > this

    [Header("Rates (per second)")]
    public float riseRate = 1.0f;        // fill speed while in danger
    public float fallRate = 1.5f;        // drain speed while safe

    [Header("Time to fill (seconds)")]
    public float maxTime = 6f;           // seconds of “bad state” to fill a bar

    float lactoseT;                      // 0..maxTime
    float miceT;                         // 0..maxTime

    void Update()
    {
        if (!player1 || !player2) return;

        float d = Vector2.Distance(player1.position, player2.position);

        // lactose: close is bad
        float lDelta = (d < closeThreshold ? riseRate : -fallRate) * Time.deltaTime;
        // mice: far is bad
        float mDelta = (d > farThreshold   ? riseRate : -fallRate) * Time.deltaTime;

        lactoseT = Mathf.Clamp(lactoseT + lDelta, 0f, maxTime);
        miceT    = Mathf.Clamp(miceT    + mDelta, 0f, maxTime);

        if (lactoseGauge) lactoseGauge.value = lactoseT / maxTime;
        if (miceGauge)    miceGauge.value    = miceT    / maxTime;

        // (Optional) trigger game over when full
        if (lactoseT >= maxTime) TryGameOver("Lactose overload!");
        if (miceT    >= maxTime) TryGameOver("Swarmed by mice!");
    }

    void TryGameOver(string reason)
    {
        var go = FindObjectOfType<GameOver>(); // only if you added the GameOver script
        if (go) go.Trigger(reason);
        else Debug.Log("Game Over: " + reason);
        enabled = false; // stop updating after game over
    }
}