using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public float score = 0;

    [Header("Score Settings")]
    public float greenScorePerSecond = 1.5f;
    public float yellowScorePerSecond = 1f;
    public float redScorePerSecond = 0f;

    private float currentScoreRate = 0f;

    public TMP_Text scoreText;
    public bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        score += currentScoreRate * Time.deltaTime;

        DisplayScore();
    }

    void DisplayScore()
    {
        scoreText.text = score.ToString("0");
    }

    public void EnterGreenZone()
    {
        currentScoreRate = greenScorePerSecond;
    }

    public void EnterYellowZone()
    {
        currentScoreRate = yellowScorePerSecond;
    }

    public void EnterRedZone()
    {
        currentScoreRate = redScorePerSecond;
    }
}