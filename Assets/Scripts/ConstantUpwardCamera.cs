using UnityEngine;

public class ConstantUpwardCamera : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform player;
    public float scrollSpeed = 2f;
    public float yOffset = 0f;

    [Header("Fail Conditions")]
    public float failLimit = 5f;
    private float failTimer = 0f;
    private bool isGameOver = false;

    [Header("UI Reference")]
    public GameObject gameOverUI; // ลากโฟลเดอร์ UI ที่สร้างไว้มาใส่ที่นี่

    void Update()
    {
        // ถ้าเกมจบแล้ว ไม่ต้องรัน Code ด้านล่างต่อ
        if (player == null || isGameOver) return;

        // --- ส่วนของกล้อง ---
        float autoScrollY = transform.position.y + (scrollSpeed * Time.deltaTime);
        float playerY = player.position.y + yOffset;
        float finalY = Mathf.Max(autoScrollY, playerY);
        transform.position = new Vector3(transform.position.x, finalY, transform.position.z);

        // --- เช็คการแพ้ ---
        if (player.position.y < transform.position.y)
        {
            failTimer += Time.deltaTime;
            if (failTimer >= failLimit)
            {
                TriggerGameOver();
            }
        }
        else
        {
            failTimer = 0f;
        }
    }

    void TriggerGameOver()
    {
        isGameOver = true;

        // 1. แสดง UI Game Over
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // 2. หยุดเวลาในเกมทั้งหมด (ทำให้เคลื่อนที่ไม่ได้/ฟิสิกส์ไม่ทำงาน)
        Time.timeScale = 0f;

        Debug.Log("Game Over: ทุกอย่างหยุดนิ่ง!");
    }
}