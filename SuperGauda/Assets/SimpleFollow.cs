using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    public Transform target;
    public float smooth = 12f;

    void LateUpdate()
    {
        if (!target) return;
        Vector3 p = target.position;
        p.z = -10f; // keep camera in front
        transform.position = Vector3.Lerp(transform.position, p, smooth * Time.deltaTime);
    }
}