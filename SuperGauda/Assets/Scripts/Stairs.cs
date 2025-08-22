using UnityEngine;

public class Stairs : MonoBehaviour
{
    public Transform destination;    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player") && !other.CompareTag("Player2")) return;

        if (destination != null)
        {
            other.transform.position = destination.position;
            Debug.Log("Teleported to: " + destination.position);
        }
    }
}
