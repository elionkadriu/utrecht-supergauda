using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class Lever : MonoBehaviour, IInteractable
{
    public Sprite offSprite;
    public Sprite onSprite;
    public UnityEvent onTurnOn;
    public UnityEvent onTurnOff;

    public bool IsOn { get; private set; }
    SpriteRenderer sr;
    Collider2D col;

    void Awake()
    {
        sr  = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        col.isTrigger = true;
        SetVisual(false);
    }

    public void Interact(Interactor by) => Toggle();

    public void Toggle()
    {
        IsOn = !IsOn;
        SetVisual(IsOn);
        if (IsOn) onTurnOn?.Invoke(); else onTurnOff?.Invoke();
    }

    public void ForceOff()
    {
        IsOn = false;
        SetVisual(false);
        onTurnOff?.Invoke();
    }

    void SetVisual(bool on)
    {
        if (onSprite) sr.sprite = on ? onSprite : offSprite;
        else sr.color = on ? new Color(1f,0.9f,0.6f) : Color.white; // fallback if only one sprite
    }
}