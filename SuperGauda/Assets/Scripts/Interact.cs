using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
	public UnityEvent<PlayerInteract> OnInteract;

	private void OnTriggerEnter2D(Collider2D other)
	{
        if(!other.CompareTag("Player") && !other.CompareTag("Player2")) return;

		other.GetComponent<PlayerInteract>().interactField = this;
        other.GetComponent<PlayerInteract>().OnInteractFieldEnter.Invoke();
	}

	private void OnTriggerExit2D(Collider2D other)
	{
        if(!other.CompareTag("Player") && !other.CompareTag("Player2")) return;
		
		other.GetComponent<PlayerInteract>().interactField = null;
        other.GetComponent<PlayerInteract>().OnInteractFieldExit.Invoke();
	}
}
