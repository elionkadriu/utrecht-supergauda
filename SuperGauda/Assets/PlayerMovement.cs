using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player1Movement : MonoBehaviour
{
    public float moveSpeed = 2.5f;  // slower, because Player1 is strong but not fast

    private Rigidbody2D rb;
    private Vector2 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Read WASD input (set in Unity's Input Manager as Horizontal_P1 / Vertical_P1)
        float moveX = Input.GetAxisRaw("Horizontal_P1"); // A/D or Left/Right
        float moveY = Input.GetAxisRaw("Vertical_P1");   // W/S or Up/Down

        movement = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        // Move with physics (smoother collisions)
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}