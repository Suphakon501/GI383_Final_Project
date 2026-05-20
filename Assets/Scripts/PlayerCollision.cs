using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

        if (ldlSlider.value >= ldlSlider.maxValue)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("InfoScene");
    }

    public void GoToInfoScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("InfoScene");
    }
}