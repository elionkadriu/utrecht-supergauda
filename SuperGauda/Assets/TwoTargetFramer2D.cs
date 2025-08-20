using UnityEngine;

[RequireComponent(typeof(Camera))]
public class TwoTargetFramer2D : MonoBehaviour
{
    public Transform p1;
    public Transform p2;

    [Header("Framing")]
    public float padding = 2f;   // space around players
    public float minSize = 4f;   // minimum zoom in
    public float maxSize = 12f;  // maximum zoom out
    public float smooth = 8f;    // smoothing for move/zoom

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (!p1 || !p2) return;

        // Center point between players
        Vector3 midpoint = (p1.position + p2.position) * 0.5f;
        midpoint.z = -10f;

        // Distance between them → controls zoom
        float dist = Vector2.Distance(p1.position, p2.position);
        float aspect = Mathf.Max(cam.aspect, 0.01f);
        float targetSize = (dist * 0.5f) / aspect + padding;
        targetSize = Mathf.Clamp(targetSize, minSize, maxSize);

        // Smooth position & zoom
        transform.position = Vector3.Lerp(transform.position, midpoint, smooth * Time.deltaTime);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, smooth * Time.deltaTime);
    }
}