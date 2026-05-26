using UnityEngine;

public class SliderZoneEffect : MonoBehaviour
{
    public RectTransform slider;
    public Transform player;

    [Header("Zone UI")]
    public RectTransform greenZone;
    public RectTransform yellowZone;
    public RectTransform redZone;

    [Header("Base Float Force")]
    public float greenForce = 8f;
    public float yellowForce = 5f;
    public float redForce = 2f;

    [Header("Difficulty Scaling")]
    public float difficultyMultiplier = 1f;
    public float difficultyIncreaseRate = 0.01f;

    [Header("Animation")]
    public Animator animator;

    [Header("Score")]
    public ScoreManager scoreManager;

    private string currentState = "";

    private int currentPhase = 1;
    private float gameTime;

    void Update()
    {
        gameTime += Time.deltaTime;

        UpdatePhase();

        // หลังเข้า phase 3 จะเพิ่ม difficulty เรื่อยๆ
        if (currentPhase >= 3)
        {
            difficultyMultiplier += difficultyIncreaseRate * Time.deltaTime;
        }

        bool inGreen = IsOverlapping(slider, greenZone);
        bool inYellow = IsOverlapping(slider, yellowZone);
        bool inRed = IsOverlapping(slider, redZone);

        if (inGreen)
        {
            MovePlayer(greenForce);

            scoreManager.EnterGreenZone();
        }
        else if (inYellow)
        {
            MovePlayer(yellowForce);

            scoreManager.EnterYellowZone();
        }
        else
        {
            MovePlayer(redForce);

            scoreManager.EnterRedZone();
        }
    }

    void UpdatePhase()
    {
        if (gameTime >= 120f)
        {
            currentPhase = 3;
            PlayAnim("Phase3");
        }
        else if (gameTime >= 60f)
        {
            currentPhase = 2;
            PlayAnim("Phase2");
        }
        else
        {
            currentPhase = 1;
            PlayAnim("normal");
        }
    }

    void MovePlayer(float baseForce)
    {
        float finalForce = baseForce * difficultyMultiplier;

        player.Translate(Vector3.up * finalForce * Time.deltaTime);
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