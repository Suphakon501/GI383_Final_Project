using UnityEngine;

public class AutoMoveSlider : MonoBehaviour
{
    public float autoSpeed = 100f;   // วิ่งเองไปซ้าย
    public float scrollSpeed = 80f;  // แรงเม้าส์

    public float minX = -92f;
    public float maxX = 90f;

    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        float x = rect.anchoredPosition.x;

        // ✅ วิ่งไปทางซ้าย
        x -= autoSpeed * Time.deltaTime;

        // ✅ ลูกกลิ้ง (ยังคุมได้ทั้งซ้าย-ขวา)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        x += scroll * scrollSpeed;

        // ✅ จำกัดขอบ (ไม่เด้งกลับ)
        x = Mathf.Clamp(x, minX, maxX);

        rect.anchoredPosition = new Vector2(x, rect.anchoredPosition.y);
    }
}