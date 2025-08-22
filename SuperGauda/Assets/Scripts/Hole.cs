using UnityEngine;

public class Hole : MonoBehaviour
{
    public Transform destination;    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player2")) return;

        if (destination != null)
        {
            other.transform.position = destination.position;
            Debug.Log("Teleported to: " + destination.position);
        }
    }
}
