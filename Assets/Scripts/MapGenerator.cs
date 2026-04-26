using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Settings")]
    public GameObject[] mapPrefabs; // ใส่ Prefab ของแมพที่นี่
    public Transform player;        // ลาก Player มาใส่
    public float segmentLength = 16.6f; // ความยาวแต่ละช่วง
    public int initialSegments = 5;    // จำนวนแมพที่จะให้เกิดรอไว้ตอนเริ่ม

    private float spawnY = 0f;         // ตำแหน่ง Y ถัดไปที่จะเสกแมพ
    private int spawnedCount = 0;

    void Start()
    {
        // เสกแมพชุดแรกออกมารอไว้ก่อน
        for (int i = 0; i < initialSegments; i++)
        {
            SpawnMap();
        }
    }

    void Update()
    {
        // ถ้าผู้เล่นขยับขึ้นไปจนใกล้ถึงแมพแผ่นล่าสุด ให้เสกแผ่นใหม่เพิ่ม
        if (player.position.y > spawnY - (segmentLength * initialSegments))
        {
            SpawnMap();
            // (Optional) ลบแผ่นเก่าทิ้งเพื่อประหยัด RAM
            RemoveOldestMap();
        }
    }

    void SpawnMap()
    {
        // สุ่มเลือกแมพจาก Array
        int randomIndex = Random.Range(0, mapPrefabs.Length);

        // เสกแมพที่ตำแหน่ง spawnY
        GameObject mapInstance = Instantiate(mapPrefabs[randomIndex], new Vector3(0, spawnY, 0), Quaternion.identity);

        // ตั้งชื่อให้ไล่ลำดับ (เพื่อง่ายต่อการจัดการ)
        mapInstance.name = "MapSegment_" + spawnedCount;
        mapInstance.transform.parent = this.transform;

        spawnY += segmentLength; // ขยับจุดเสกถัดไปขึ้นไป 16.6
        spawnedCount++;
    }

    void RemoveOldestMap()
    {
        // ถ้ามีแมพเยอะเกินไป ให้ลบอันที่อยู่ล่างสุดทิ้ง
        if (transform.childCount > initialSegments + 2)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}