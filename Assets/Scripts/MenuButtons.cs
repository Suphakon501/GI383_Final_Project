using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject settingPanel;
    public GameObject settingButton;

    private void Start()
    {
        if (settingPanel != null)
        {
            settingPanel.SetActive(false);
        }

        if (settingButton != null)
        {
            settingButton.SetActive(true);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void OpenSetting()
    {
        if (settingPanel != null)
        {
            settingPanel.SetActive(true);
        }

        if (settingButton != null)
        {
            settingButton.SetActive(false);
        }
    }

    public void CloseSetting()
    {
        if (settingPanel != null)
        {
            settingPanel.SetActive(false);
        }

        if (settingButton != null)
        {
            settingButton.SetActive(true);
        }
    }
}
