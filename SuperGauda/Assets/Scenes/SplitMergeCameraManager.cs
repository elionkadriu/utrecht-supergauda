using UnityEngine;

public class SplitMergeCameraManager : MonoBehaviour
{
    public Transform p1;
    public Transform p2;

    public Camera camP1;
    public Camera camP2;
    public Camera camShared;

    [Header("Switch Distances (world units)")]
    public float mergeDistance = 6f;   // try 6 for testing
    public float splitDistance = 8f;   // must be > mergeDistance

    bool sharedActive;

    void Start() => SetShared(false, force:true);

    void Update()
    {
        if (!p1 || !p2) return;

        float d = Vector2.Distance(p1.position, p2.position);

        if (!sharedActive && d <= mergeDistance) SetShared(true);
        else if (sharedActive && d >= splitDistance) SetShared(false);

        // DEBUG hotkeys
        if (Input.GetKeyDown(KeyCode.M)) SetShared(true);
        if (Input.GetKeyDown(KeyCode.N)) SetShared(false);
    }

    void SetShared(bool enable, bool force = false)
    {
        if (sharedActive == enable && !force) return;
        sharedActive = enable;

        camShared.enabled = enable;
        camP1.enabled = !enable;
        camP2.enabled = !enable;
    }
}