using UnityEngine;

public class SliderZoneEffect : MonoBehaviour
{
    public RectTransform slider;
    public Transform player;

    [Header("Zone UI")]
    public RectTransform greenZone;
    public RectTransform yellowZone;
    public RectTransform redZone; // 1. เพิ่มช่องใส่ UI สีแดง

    [Header("Float Force")]
    public float greenForce = 8f;
    public float yellowForce = 5f;
    public float redForce = 2f;    // 2. เพิ่มแรงผลักสำหรับสีแดง

    void Update()
    {
        // 3. เพิ่มเงื่อนไขการเช็ค Overlap ของโซนแดง
        if (IsOverlapping(slider, greenZone))
        {
            player.Translate(Vector3.up * greenForce * Time.deltaTime);
        }
        else if (IsOverlapping(slider, yellowZone))
        {
            player.Translate(Vector3.up * yellowForce * Time.deltaTime);
        }
        else if (IsOverlapping(slider, redZone)) // เช็คโซนแดงเพิ่ม
        {
            player.Translate(Vector3.up * redForce * Time.deltaTime);
        }
    }

    bool IsOverlapping(RectTransform rect1, RectTransform rect2)
    {
        // ถ้าไม่ได้ลาก Object ใส่ในช่อง Inspector ให้ข้ามไปก่อนเพื่อกัน Error
        if (rect1 == null || rect2 == null) return false;

        float min1 = rect1.anchoredPosition.x - (rect1.rect.width * rect1.pivot.x);
        float max1 = min1 + rect1.rect.width;

        float min2 = rect2.anchoredPosition.x - (rect2.rect.width * rect2.pivot.x);
        float max2 = min2 + rect2.rect.width;

        return min1 <= max2 && max1 >= min2;
    }
}