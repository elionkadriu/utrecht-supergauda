using UnityEngine;

public class Interactor : MonoBehaviour
{
    public KeyCode key = KeyCode.E;
    public float radius = 1.2f;
    public LayerMask interactableMask;

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            var hit = Physics2D.OverlapCircle((Vector2)transform.position, radius, interactableMask);
            if (!hit) return;

            var target = hit.GetComponent<IInteractable>();
            if (target != null) target.Interact(this);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}