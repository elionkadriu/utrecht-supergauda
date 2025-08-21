using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform destination;
    bool worksForPlayer2 = true;

    public Transform GetDestination(bool isPlayer2)
    {
        if(!worksForPlayer2 && isPlayer2) return null;
        return destination;
    }
}
