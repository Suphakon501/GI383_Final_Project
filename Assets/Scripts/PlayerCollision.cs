using UnityEngine;
using UnityEngine.UI; 

public class PlayerCollision : MonoBehaviour
{
    public Slider ldlSlider;      
    public float increaseAmount = 10f; 

    [Header("Particle Effect")]
    public GameObject hitParticle; // ?? ใส่ prefab particle ตรงนี้

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LDL"))
        {
            // ?? สร้าง particle ตอนชน
            if (hitParticle != null)
            {
                Instantiate(hitParticle, other.transform.position, Quaternion.identity);
            }

            AddLDL();

            // ลบ object
            Destroy(other.gameObject); 
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