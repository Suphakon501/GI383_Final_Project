using UnityEngine;
using UnityEngine.UI;

public class BloodVesselMinigame : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform playerBar;
    public RectTransform targetArea;
    public RectTransform bloodVessel;
    public Animator characterAnimator;

    [Header("Game Settings")]
    public float playerSpeed = 500f;      
    public float scrollSensitivity = 300f; 
    public float trackHeight = 400f;
    public float maxVesselWidth = 1f;

    [Header("Health & Difficulty")]
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    public float healthDrainRate = 12f;

    [Header("Regeneration Settings")]
    public float initialHealthRegenRate = 3f;
    public float minHealthRegenRate = 0.5f;
    private float currentHealthRegenRate;

    private float difficultyMultiplier = 1f;
    private float gameTimer = 0f;

    private float targetMoveTimer = 0f;

    void Start()
    {
        currentHealthRegenRate = initialHealthRegenRate;
    }

    void Update()
    {
        IncreaseDifficulty();
        HandlePlayerMovement();
        MoveTargetArea();
        CheckOverlapAndHealth();
        UpdateAnimationsAndVisuals();
    }

    private void HandlePlayerMovement()
    {
        float keyboardInput = Input.GetAxis("Vertical");

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        Vector2 newPos = playerBar.anchoredPosition;

        if (Mathf.Abs(keyboardInput) > 0.01f)
        {
            newPos.y += keyboardInput * playerSpeed * Time.deltaTime;
        }

        if (Mathf.Abs(scrollInput) > 0.01f)
        {
            newPos.y += scrollInput * scrollSensitivity;
        }

        newPos.y = Mathf.Clamp(newPos.y, -trackHeight / 2f, trackHeight / 2f);
        playerBar.anchoredPosition = newPos;
    }

    private void MoveTargetArea()
    {
        targetMoveTimer += Time.deltaTime * (0.5f * difficultyMultiplier);
        float noise = Mathf.PerlinNoise(targetMoveTimer, 0f);
        float targetY = Mathf.Lerp(-trackHeight / 2f, trackHeight / 2f, noise);

        Vector2 newPos = targetArea.anchoredPosition;
        newPos.y = Mathf.Lerp(newPos.y, targetY, Time.deltaTime * 3f * difficultyMultiplier);
        targetArea.anchoredPosition = newPos;
    }

    private void CheckOverlapAndHealth()
    {
        float distance = Mathf.Abs(playerBar.anchoredPosition.y - targetArea.anchoredPosition.y);
        float acceptableDistance = (playerBar.rect.height / 2f) + (targetArea.rect.height / 2f);

        bool isOverlapping = distance <= acceptableDistance;

        if (isOverlapping)
        {
            currentHealth += currentHealthRegenRate * Time.deltaTime;
        }
        else
        {
            currentHealth -= healthDrainRate * Time.deltaTime;
        }

        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    private void UpdateAnimationsAndVisuals()
    {
        float healthPercent = currentHealth / maxHealth;
        bloodVessel.localScale = new Vector3(healthPercent * maxVesselWidth, 1f, 1f);

        if (healthPercent <= 0.3f)
        {
            characterAnimator.SetFloat("FaceState", 2f);
        }
        else if (healthPercent <= 0.6f)
        {
            characterAnimator.SetFloat("FaceState", 1f);
        }
        else
        {
            characterAnimator.SetFloat("FaceState", 0f);
        }
    }

    private void IncreaseDifficulty()
    {
        gameTimer += Time.deltaTime;
        difficultyMultiplier = 1f + (gameTimer / 5f * 0.5f);
        currentHealthRegenRate = Mathf.Max(minHealthRegenRate, initialHealthRegenRate / difficultyMultiplier);
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        enabled = false;
    }
}