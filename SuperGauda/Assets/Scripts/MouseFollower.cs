using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MouseFollower : MonoBehaviour
{
    public Transform cheese;              // set by spawner
    public Transform human;               // set by spawner
    public float speed = 2.5f;            // move speed
    public float stopRadius = 0.45f;      // cluster distance around cheese
    public float avoidHumanRadius = 2.5f; // flee distance from human

    Rigidbody2D rb;

    void Awake() { rb = GetComponent<Rigidbody2D>(); }

    void FixedUpdate()
    {
        if (!cheese) return;

        Vector2 pos = rb.position;
        Vector2 toCheese = (Vector2)cheese.position - pos;
        float dCheese = toCheese.magnitude;

        // flee human if close
        Vector2 move = Vector2.zero;
        if (human)
        {
            Vector2 toHuman = (Vector2)human.position - pos;
            float dHuman = toHuman.magnitude;
            if (dHuman < avoidHumanRadius)
            {
                move = (-toHuman.normalized) * Mathf.Lerp(1.2f, 0.2f, dHuman / avoidHumanRadius);
            }
        }

        // otherwise seek cheese (but don't overlap its center)
        if (move == Vector2.zero && dCheese > stopRadius)
            move = toCheese.normalized;

        rb.MovePosition(pos + move * speed * Time.fixedDeltaTime);
    }
}