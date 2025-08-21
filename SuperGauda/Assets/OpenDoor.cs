using UnityEngine;

public class OpenDoor : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(!collider.CompareTag("Chest")) return;

		//open the door
	}
}
