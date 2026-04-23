using UnityEngine;

public class AutoMoveSlider : MonoBehaviour
{
    [Header("Movement Settings")]
    public float autoSpeed = 100f;       // ความเร็วเริ่มต้น
    public float maxAutoSpeed = 500f;    // ความเร็วสูงสุด
    public float acceleration = 3f;      // ความเร็วจะเพิ่มขึ้น

    public float scrollSpeed = 80f;      // แรงเม้าส์

    [Header("Boundary Settings")]
    public float minX = -92f;
    public float maxX = 90f;

    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (autoSpeed < maxAutoSpeed)
        {
            autoSpeed += acceleration * Time.deltaTime;
        }

        float x = rect.anchoredPosition.x;

        x -= autoSpeed * Time.deltaTime;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        x += scroll * scrollSpeed;

        x = Mathf.Clamp(x, minX, maxX);

        rect.anchoredPosition = new Vector2(x, rect.anchoredPosition.y);
    }
}