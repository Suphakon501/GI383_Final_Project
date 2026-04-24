using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public float interval = 1f; // กี่วินาทีต่อ 1 คะแนน
    private float timer = 0f;

    public TMP_Text scoreText;
    public bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        timer += Time.deltaTime;

        if (timer >= interval)
        {
            score += 1;
            timer = 0f; // รีเซ็ต
            DisplayScore();
        }
    }

    void DisplayScore()
    {
        scoreText.text = score.ToString();
    }

}