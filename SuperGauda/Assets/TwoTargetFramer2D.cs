using UnityEngine;

[RequireComponent(typeof(Camera))]
public class TwoTargetFramer2D : MonoBehaviour
{
    public Transform p1;
    public Transform p2;

    [Header("Framing")]
    public float padding = 2f;   // space around players
    public float minSize = 4f;   // don’t zoom too far in
    public float maxSize = 12f;  // don’t zoom too far out
    public float smooth = 8f;    // movement/zoom smoothing

    Camera cam;

    void Awake() { cam = GetComponent<Camera>(); }

    void LateUpdate()
    {
        if (!p1 || !p2) return;

        // center between players
        Vector3 midpoint = (p1.position + p2.position) * 0.5f;
        midpoint.z = -10f;

        // distance → target size (fit horizontally, then pad)
        float dist = Vector2.Distance(p1.position, p2.position);
        float aspect = Mathf.Max(cam.aspect, 0.01f);
        float targetSize = (dist * 0.5f) / aspect + padding;
        targetSize = Mathf.Clamp(targetSize, minSize, maxSize);

        // smooth move & zoom
        transform.position = Vector3.Lerp(transform.position, midpoint, smooth * Time.deltaTime);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, smooth * Time.deltaTime);
    }
}