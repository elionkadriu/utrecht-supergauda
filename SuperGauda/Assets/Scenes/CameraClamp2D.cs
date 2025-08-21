using UnityEngine;

[DefaultExecutionOrder(100)]               // run after follow/framer scripts
[RequireComponent(typeof(Camera))]
public class CameraClamp2D : MonoBehaviour
{
    public Collider2D bounds;              // assign CameraBounds (BoxCollider2D)
    Camera cam;

    void Awake(){ cam = GetComponent<Camera>(); }

    void LateUpdate()
    {
        if (!bounds) return;

        // Cap zoom so the full view fits inside bounds
        float maxByHeight = bounds.bounds.extents.y;
        float maxByWidth  = bounds.bounds.extents.x / Mathf.Max(cam.aspect, 0.0001f);
        float maxSize     = Mathf.Min(maxByHeight, maxByWidth);
        if (cam.orthographic) cam.orthographicSize = Mathf.Min(cam.orthographicSize, maxSize);

        // Clamp position so edges never leave bounds
        float halfH = cam.orthographicSize;
        float halfW = halfH * cam.aspect;

        Bounds b  = bounds.bounds;
        Vector3 p = transform.position;
        p.x = Mathf.Clamp(p.x, b.min.x + halfW, b.max.x - halfW);
        p.y = Mathf.Clamp(p.y, b.min.y + halfH, b.max.y - halfH);
        p.z = -10f; // keep camera in front
        transform.position = p;
    }
}