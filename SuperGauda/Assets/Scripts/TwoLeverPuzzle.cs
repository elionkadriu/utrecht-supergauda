using UnityEngine;

public class TwoLeverPuzzle : MonoBehaviour
{
    public Lever leverA;
    public Lever leverB;
    public float windowSeconds = 1.2f;
    public GameObject starToReveal;   // disabled in scene at start
    public bool oneShot = true;

    float timer;
    bool pending;
    bool solved;

    void Start()
    {
        if (starToReveal) starToReveal.SetActive(false);
    }

    void Update()
    {
        if (solved) return;

        if (leverA && leverB && leverA.IsOn && leverB.IsOn)
        {
            solved = true;
            if (starToReveal) starToReveal.SetActive(true);
            return;
        }

        if (leverA.IsOn ^ leverB.IsOn) // exactly one on
        {
            if (!pending) { pending = true; timer = windowSeconds; }
            else
            {
                timer -= Time.deltaTime;
                if (timer <= 0f) { pending = false; leverA.ForceOff(); leverB.ForceOff(); }
            }
        }
        else pending = false;
    }
}