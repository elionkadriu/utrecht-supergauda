using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class Door : MonoBehaviour
{
    public int requiredStars = 3;
    public Sprite closedSprite;
    public Sprite openSprite;

    bool opened;
    Collider2D col;
    SpriteRenderer sr;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        sr  = GetComponent<SpriteRenderer>();
        SetVisual(false);
    }

    void OnEnable()
    {
        if (StarsManager.Instance)
            StarsManager.Instance.onStarsChanged.AddListener(OnStarsChanged);

        if (StarsManager.Instance) OnStarsChanged(StarsManager.Instance.Count);
    }

    void OnDisable()
    {
        if (StarsManager.Instance)
            StarsManager.Instance.onStarsChanged.RemoveListener(OnStarsChanged);
    }

    void OnStarsChanged(int n)
    {
        if (!opened && n >= requiredStars) Open();
    }

    void Open()
    {
        opened = true;
        SetVisual(true);
        if (col) col.enabled = false; // walk through
        Debug.Log("Door opened!");
    }

    void SetVisual(bool open) { if (sr) sr.sprite = open ? openSprite : closedSprite; }
}