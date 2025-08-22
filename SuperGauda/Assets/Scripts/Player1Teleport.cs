using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    public bool isPlayer2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Portal portal = other.GetComponent<Portal>();
        if (portal != null && portal.GetDestination(isPlayer2) != null)
        {
            transform.position = portal.GetDestination(isPlayer2).position;
            Debug.Log("Teleported to: " + portal.GetDestination(isPlayer2).position);
        }
    }
}
