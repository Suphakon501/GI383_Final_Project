using System.Collections.Generic;
using UnityEngine;

public class MapSpawner2D : MonoBehaviour
{
    [Header("Prefabs")]
    public List<GameObject> mapPrefabs;

    [Header("Player")]
    public Transform player;

    [Header("Spawn Settings")]
    public int initialSpawn = 5;
    public float spawnAhead = 20f;
    public float overlapFix = 0f; // ปรับได้ถ้ายังมีรอย

    [Header("Cleanup")]
    public float destroyBelow = 30f;

    private float currentY = 0f;
    private Queue<GameObject> spawned = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < initialSpawn; i++)
        {
            Spawn();
        }
    }

    void Update()
    {
        // Spawn เพิ่ม
        if (player.position.y + spawnAhead > currentY)
        {
            Spawn();
        }

        // ลบอันเก่า
        while (spawned.Count > 0)
        {
            GameObject first = spawned.Peek();

            if (first.transform.position.y < player.position.y - destroyBelow)
            {
                Destroy(first);
                spawned.Dequeue();
            }
            else break;
        }
    }

    void Spawn()
    {
        GameObject prefab = mapPrefabs[Random.Range(0, mapPrefabs.Count)];

        // ? ใช้ค่าที่กำหนดเอง (แม่นสุด)
        MapPiece pieceData = prefab.GetComponent<MapPiece>();

        if (pieceData == null)
        {
            Debug.LogError("? prefab ไม่มี MapPiece: " + prefab.name);
            return;
        }

        float height = pieceData.height;

        Vector3 spawnPos = new Vector3(0, currentY + height / 2f, 0);

        GameObject piece = Instantiate(prefab, spawnPos, Quaternion.identity);

        spawned.Enqueue(piece);

        currentY += height - overlapFix;
    }
}