using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Settings")]
    public GameObject[] mapPrefabs; 
    public Transform player;        
    public float segmentLength = 16.6f; 
    public int initialSegments = 5;    

    private float spawnY = 0f;        
    private int spawnedCount = 0;

    void Start()
    {
        for (int i = 0; i < initialSegments; i++)
        {
            SpawnMap();
        }
    }

    void Update()
    {
        if (player.position.y > spawnY - (segmentLength * initialSegments))
        {
            SpawnMap();
            RemoveOldestMap();
        }
    }

    void SpawnMap()
    {
        int randomIndex = Random.Range(0, mapPrefabs.Length);

        GameObject mapInstance = Instantiate(mapPrefabs[randomIndex], new Vector3(0, spawnY, 0), Quaternion.identity);

        mapInstance.name = "MapSegment_" + spawnedCount;
        mapInstance.transform.parent = this.transform;

        spawnY += segmentLength; 
        spawnedCount++;
    }

    void RemoveOldestMap()
    {
        if (transform.childCount > initialSegments + 2)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}