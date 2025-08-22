using UnityEngine;

public class OpenDoor_4 : MonoBehaviour
{
	public GameObject door;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(!collider.CompareTag("Player2")) return;

		//open the door
		Destroy(door);
		
	}	
}
