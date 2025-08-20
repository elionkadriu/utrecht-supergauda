using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player2Movement : MonoBehaviour
{
    public float moveSpeed = 4.0f; // Cheese is faster than the human

    private Rigidbody2D rb;
    private Vector2 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Arrow key input (set up in Input Manager as Horizontal_P2 / Vertical_P2)
        float moveX = Input.GetAxisRaw("Horizontal_P2");
        float moveY = Input.GetAxisRaw("Vertical_P2");

        movement = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        // Physics-based movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}