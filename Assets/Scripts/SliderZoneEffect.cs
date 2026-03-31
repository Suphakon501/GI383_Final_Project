using UnityEngine;

public class SliderZoneEffect : MonoBehaviour
{
    public RectTransform slider;   // á·è§ÊÕ¢ÒÇ
    public Transform player;       // µÑÇ¼ÙéàÅè¹

    [Header("Zone UI")]
    public RectTransform greenZone;
    public RectTransform yellowZone;

    [Header("Float Force")]
    public float greenForce = 5f;
    public float yellowForce = 2f;

    void Update()
    {
        float x = slider.anchoredPosition.x;

        // ?? àªç¤ÍÂÙèã¹â«¹äË¹¨Ò¡ UI
        if (IsInsideZone(x, greenZone))
        {
            // ?? à¢ÕÂÇ
            player.Translate(Vector3.up * greenForce * Time.deltaTime);
        }
        else if (IsInsideZone(x, yellowZone))
        {
            // ?? àËÅ×Í§
            player.Translate(Vector3.up * yellowForce * Time.deltaTime);
        }
        // ?? ¹Í¡¹Ñé¹ = á´§
    }

    bool IsInsideZone(float x, RectTransform zone)
    {
        float center = zone.anchoredPosition.x;
        float halfWidth = zone.rect.width / 2f;

        float min = center - halfWidth;
        float max = center + halfWidth;

        return x >= min && x <= max;
    }
}