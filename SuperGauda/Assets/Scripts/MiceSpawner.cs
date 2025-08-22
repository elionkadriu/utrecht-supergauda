using UnityEngine;

public class MiceSpawner : MonoBehaviour
{
    public Transform human;               // Player1_Human
    public Transform cheese;              // Player2_Cheese (usually this.transform)
    public GameObject mousePrefab;

    [Header("Spawn logic")]
    public int   maxMice       = 4;
    public float spawnInterval = 1.5f;    // seconds between spawns
    public float minSpawnRadius = 3f;     // min distance from cheese
    public float maxSpawnRadius = 5f;     // max distance from cheese
    public float farThreshold   = 6f;     // start spawning when players are farther than this

    float timer;

    void Reset() { cheese = transform; }

    void Update()
    {
        if (!human || !cheese || !mousePrefab) return;

        float d = Vector2.Distance(human.position, cheese.position);

        if (d > farThreshold)
        {
            // players are far → keep spawning up to max
            timer += Time.deltaTime;
            if (timer >= spawnInterval && transform.childCount < maxMice)
            {
                timer = 0f;
                SpawnOne();
            }
        }
        else
        {
            // players close → clear mice
            timer = 0f;
            for (int i = transform.childCount - 1; i >= 0; i--)
                Destroy(transform.GetChild(i).gameObject);
        }
    }

    void SpawnOne()
    {
        Vector2 dir = Random.insideUnitCircle.normalized;
        float r = Random.Range(minSpawnRadius, maxSpawnRadius);
        Vector3 pos = cheese.position + (Vector3)(dir * r);

        var m = Instantiate(mousePrefab, pos, Quaternion.identity, transform);
        var f = m.GetComponent<MouseFollower>();
        if (f) { f.cheese = cheese; f.human = human; }
    }
}