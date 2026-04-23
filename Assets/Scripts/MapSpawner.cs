using UnityEngine;

public partial class MapSpawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject mapPrefab;    // ใส่ Prefab ของแมพที่นี่
    public Transform player;        // ใส่ตัวละคร Player เพื่อให้เช็คระยะทาง
    public float mapHeight = 8f;    // ความสูงของแมพ 1 ชิ้น (ปรับให้ตรงกับ Scale Y ของคุณ)
    public int initialMaps = 3;     // จำนวนแมพที่จะเจนรอไว้ตอนเริ่มเกม

    private float spawnY = 0f;      // จุดที่จะวางแมพถัดไป
    private float safeMargin = 15f; // ระยะห่างที่ถ้าผู้เล่นขยับเข้าใกล้ จะเจนแมพใหม่ทันที

    void Start()
    {
        // เจนแมพชุดแรกตอนเริ่มเกม
        for (int i = 0; i < initialMaps; i++)
        {
            SpawnMap();
        }
    }

    void Update()
    {
        // ถ้าผู้เล่นขยับขึ้นไปจนใกล้ถึงจุดสิ้นสุดของแมพปัจจุบัน ให้สร้างอันใหม่
        if (player.position.y + safeMargin > spawnY)
        {
            SpawnMap();
        }
    }

    void SpawnMap()
    {
        // สร้าง Object จาก Prefab
        GameObject temp = Instantiate(mapPrefab, new Vector3(0, spawnY, 0), Quaternion.identity);

        // ขยับจุด Spawn ไปข้างหน้าตามความสูงของแมพ
        spawnY += mapHeight;

        // (Optional) ลบแมพเก่าทิ้งอัตโนมัติหลังจากผ่านไป 20 วินาที เพื่อประหยัด RAM
        Destroy(temp, 5f);
    }
}