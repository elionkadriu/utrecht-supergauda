using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MovementStartSfx_ArrowsOnly : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip clip;               // your 2s sound
    public float rearmIdleTime = 0.15f;  // how long all arrow keys must be released to re-arm
    public float minInterval   = 0.05f;  // tiny anti-double-fire
    public bool play2D = true;           // simplest: 2D sound

    [Header("Optional: only fire if we actually moved this frame")]
    public bool requireActualMotion = false;
    public float motionThreshold = 0.02f; // world units

    AudioSource _audio;
    bool _armed = true;
    float _idleSince = -1f;
    float _lastPlay = -999f;
    Vector2 _prevPos;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _audio.playOnAwake = false;
        _audio.dopplerLevel = 0f;
        _audio.spatialBlend = play2D ? 0f : 1f; // 0 = 2D, 1 = positional
        if (_audio.clip) _audio.clip = null;    // we use PlayOneShot
        _prevPos = transform.position;
    }

    void Update()
    {
        bool anyArrowHeld =
            Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow);

        if (anyArrowHeld)
        {
            if (_armed && Time.time - _lastPlay > minInterval && clip &&
                (!requireActualMotion || ActuallyMoved()))
            {
                _audio.PlayOneShot(clip);        // ✅ one ping when arrows first pressed
                _lastPlay = Time.time;
                _armed = false;                  // disarm until all arrows are released
                _idleSince = -1f;
            }
        }
        else
        {
            if (_idleSince < 0f) _idleSince = Time.time; // started being idle
            if (!_armed && (Time.time - _idleSince) >= rearmIdleTime)
                _armed = true;                   // re-arm after full release
        }
    }

    bool ActuallyMoved()
    {
        Vector2 p = transform.position;
        float movedSqr = (p - _prevPos).sqrMagnitude;
        _prevPos = p;
        return movedSqr > motionThreshold * motionThreshold;
    }
}
