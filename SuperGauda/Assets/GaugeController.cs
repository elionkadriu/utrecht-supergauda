/*using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    [Header("Players")]
    public Transform p1;                // Player1_Human
    public Transform p2;                // Player2_Cheese

    [Header("UI")]
    public Slider lactose;              // 0..1
    public Slider mice;                 // 0..1
    public Image lactoseFill;           // optional: assign the Fill image
    public Image miceFill;              // optional: assign the Fill image

    [Header("Distance thresholds (world units)")]
    public float tooClose = 2f;         // lactose rises when < this
    public float tooFar   = 4f;         // mice rises when > this

    [Header("Rates (per second)")]
    public float riseRate = 1.0f;       // how fast bars fill
    public float fallRate = 1.5f;       // how fast they drain

    [Header("Time to fill bar (seconds)")]
    public float max = 5f;              // seconds to reach game over

    [Header("Warning visuals")]
    public float warnAt = 0.7f;         // start flashing color above this

    float l, m;                         // current “time filled” for each (0..max)

    void Update()
    {
        if (!p1 || !p2) return;

        float d = Vector2.Distance(p1.position, p2.position);

        // lactose logic (close is bad)
        l += (d < tooClose ? riseRate : -fallRate) * Time.deltaTime;
        // mice logic (far is bad)
        m += (d > tooFar   ? riseRate : -fallRate) * Time.deltaTime;

        l = Mathf.Clamp(l, 0f, max);
        m = Mathf.Clamp(m, 0f, max);

        // normalize 0..1 for sliders
        float lv = l / max;
        float mv = m / max;

        if (lactose) lactose.value = lv;
        if (mice)    mice.value    = mv;

        // simple color warning (optional)
        if (lactoseFill) lactoseFill.color = Color.Lerp(Color.green, Color.red, lv);
        if (miceFill)    miceFill.color    = Color.Lerp(Color.green, Color.red, mv);

        // game over
        if (l >= max) TriggerGameOver("Lactose overload!");
        if (m >= max) TriggerGameOver("Swarmed by mice!");
    }

  void TriggerGameOver(string reason)
    {
        // Avoid multiple triggers
        enabled = false;

        // If you added the GameOver script earlier, call it. Otherwise just log.
        // var go = FindObjectOfType<GameOver>();
        // if (go) go.Trigger(reason);
        else Debug.Log("Game Over: " + reason);
    }
}*/