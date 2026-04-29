using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    public Slider ldlSlider;
    public float increaseAmount = 10f;

    [Header("Particle Effect")]
    public GameObject hitParticle;

    [Header("Blink Effect")]
    public SpriteRenderer spriteRenderer;
    public float blinkDuration = 0.5f;
    public float blinkSpeed = 0.1f;

    [Header("UI Reference")]
    public GameObject gameOverUI; // ลาก Game Over Panel มาใส่ที่นี่ (ตัวเดียวกับที่ใช้ในระบบกล้อง)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LDL"))
        {
            Vector3 hitPos = other.bounds.center;

            if (hitParticle != null)
            {
                GameObject p = Instantiate(hitParticle, hitPos, Quaternion.identity);
                Destroy(p, 1f);
            }

            StartCoroutine(Blink());
            AddLDL();
        }
    }

    IEnumerator Blink()
    {
        float timer = 0f;
        while (timer < blinkDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkSpeed);
            timer += blinkSpeed;
        }
        spriteRenderer.enabled = true;
    }

    void AddLDL()
    {
        ldlSlider.value += increaseAmount;

        // เมื่อค่า LDL เต็มหลอด (MaxValue)
        if (ldlSlider.value >= ldlSlider.maxValue)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over: LDL Full!");

        // 1. แสดง UI ตัวเดียวกับระบบกล้อง
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // 2. หยุดเวลาเกม
        Time.timeScale = 0f;
    }
}