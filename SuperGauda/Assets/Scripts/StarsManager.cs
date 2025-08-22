using UnityEngine;
using UnityEngine.Events;

public class StarsManager : MonoBehaviour
{
    public static StarsManager Instance { get; private set; }

    public UnityEvent<int> onStarsChanged;
    public int Count { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void AddStar(int amount)
    {
        Count += amount;
        onStarsChanged?.Invoke(Count);
    }
}