using UnityEngine;

public class OpenDoor : MonoBehaviour
{
	public GameObject door;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(!collider.CompareTag("Chest")) return;

		//open the door
		Destroy(door);
	}
}
