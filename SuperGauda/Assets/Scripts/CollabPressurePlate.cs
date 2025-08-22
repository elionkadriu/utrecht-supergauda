using UnityEngine;
using UnityEngine.Events;

public class CollabPressurePlate : MonoBehaviour
{
    public UnityEvent WhenThisOneIsTriggeredLast;
    public CollabPressurePlate otherPlate;
    [HideInInspector]
    public int playersOnIt = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player") && !other.CompareTag("Player2")) return;

        playersOnIt++;
        if(otherPlate.playersOnIt > 0) WhenThisOneIsTriggeredLast.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("Player") && !other.CompareTag("Player2")) return;

        playersOnIt--;
    }
}
