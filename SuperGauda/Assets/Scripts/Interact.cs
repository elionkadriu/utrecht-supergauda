using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
	public UnityEvent<PlayerInteract> OnInteract;

	private void OnTriggerEnter2D(Collider2D other)
	{
        if(!other.CompareTag("Player") && !other.CompareTag("Player2")) return;

		other.GetComponent<PlayerInteract>().interactField = this;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
        if(!other.CompareTag("Player") && !other.CompareTag("Player2")) return;
		
		other.GetComponent<PlayerInteract>().interactField = null;
	}
}
