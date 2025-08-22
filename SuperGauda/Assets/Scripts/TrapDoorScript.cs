using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform destination;    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;

        if (destination != null)
        {
            other.transform.position = destination.position;
            Debug.Log("Teleported to: " + destination.position);
        }
    }
}
