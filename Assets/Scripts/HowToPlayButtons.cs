using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayButtons : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Next()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
