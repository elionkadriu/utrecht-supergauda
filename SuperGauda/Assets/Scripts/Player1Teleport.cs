using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Portal portal = other.GetComponent<Portal>();
        if (portal != null && portal.GetDestination() != null)
        {
            transform.position = portal.GetDestination().position;
            Debug.Log("Teleported to: " + portal.GetDestination().position);
        }
    }
}
