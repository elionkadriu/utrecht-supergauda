// Trapdoor2D.cs
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Trapdoor2D : MonoBehaviour
{
    [Header("Who can trigger?")]
    public bool useMassCheck = false;     // leave off if you tag one player as "heavy"
    public float minMassToTrigger = 80f;  // used only when useMassCheck = true

    [Header("Teleport target")]
    public Transform basementSpawn;       // assign the BasementSpawn transform

    [Header("Optional visual")]
    public Animator animator;             // plays "Open" on trigger

    void OnTriggerEnter2D(Collider2D other)
    {
        // Trigger events require a Rigidbody2D somewhere in the overlap.
        var rb = other.attachedRigidbody;
        if (rb == null) return;

        // Decide if this is the heavy player
        bool isHeavy = false;

        // Option B: use actual mass if you prefer
        if (useMassCheck && rb.mass >= minMassToTrigger) isHeavy = true;

        if (!isHeavy) return;

        // Visual: open the trap
        if (animator) animator.SetTrigger("Open"); // fires the Animator trigger :contentReference[oaicite:4]{index=4}

        // Teleport just this player down to the basement
        if (basementSpawn != null)
            other.transform.position = basementSpawn.position;
    }
}
