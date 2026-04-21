using UnityEngine;

public class SliderZoneEffect : MonoBehaviour
{
    public RectTransform slider;   
    public Transform player;       

    [Header("Zone UI")]
    public RectTransform greenZone;
    public RectTransform yellowZone;

    [Header("Float Force")]
    public float greenForce = 5f;
    public float yellowForce = 2f;

    void Update()
    {
        float x = slider.anchoredPosition.x;

        if (IsInsideZone(x, greenZone))
        {
            player.Translate(Vector3.up * greenForce * Time.deltaTime);
        }
        else if (IsInsideZone(x, yellowZone))
        {
            player.Translate(Vector3.up * yellowForce * Time.deltaTime);
        }
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