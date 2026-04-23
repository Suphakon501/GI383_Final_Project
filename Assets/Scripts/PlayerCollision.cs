using UnityEngine;
using UnityEngine.UI; 

public class PlayerCollision : MonoBehaviour
{
    public Slider ldlSlider;      
    public float increaseAmount = 10f; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LDL"))
        {
            AddLDL();
            //ชนแล้วอยากให้สีเหลืองหายไปเลย
            // Destroy(other.gameObject); 
        }
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
        Debug.Log("Game Over!");
        Time.timeScale = 0; 
    }
}