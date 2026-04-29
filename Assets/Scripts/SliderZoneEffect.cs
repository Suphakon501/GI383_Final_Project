using UnityEngine;

public class SliderZoneEffect : MonoBehaviour
{
    public RectTransform slider;
    public Transform player;

    [Header("Zone UI")]
    public RectTransform greenZone;
    public RectTransform yellowZone;
    public RectTransform redZone;

    [Header("Float Force")]
    public float greenForce = 8f;
    public float yellowForce = 5f;
    public float redForce = 2f;

    [Header("Animation")]
    public Animator animator;

    private string currentState = "";

    void Update()
    {
        bool inGreen = IsOverlapping(slider, greenZone);
        bool inYellow = IsOverlapping(slider, yellowZone);
        bool inRed = IsOverlapping(slider, redZone);

        if (inGreen)
        {
            player.Translate(Vector3.up * greenForce * Time.deltaTime);
            PlayAnim("normal");   
        }
        else if (inYellow)
        {
            player.Translate(Vector3.up * yellowForce * Time.deltaTime);
            PlayAnim("Phase2");
        }
        else
        {
            player.Translate(Vector3.up * redForce * Time.deltaTime);
            PlayAnim("Phase3");
        }
    }

    void PlayAnim(string stateName)
    {
        if (animator == null) return;

        if (currentState == stateName) return;

        animator.Play(stateName);
        currentState = stateName;
    }

    bool IsOverlapping(RectTransform rect1, RectTransform rect2)
    {
        if (rect1 == null || rect2 == null) return false;

        float min1 = rect1.anchoredPosition.x - (rect1.rect.width * rect1.pivot.x);
        float max1 = min1 + rect1.rect.width;

        float min2 = rect2.anchoredPosition.x - (rect2.rect.width * rect2.pivot.x);
        float max2 = min2 + rect2.rect.width;

        return min1 <= max2 && max1 >= min2;
    }
}