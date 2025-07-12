using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGUIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private BasePanel mainMenuPanel;
    [SerializeField] private BasePanel settingsPanel;
    [SerializeField] private BasePanel creditsPanel;

    private void Start()
    {
        settingsPanel.ClosePanel();
        creditsPanel.ClosePanel();
        mainMenuPanel.OpenPanel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel.IsOpen)
            {
                settingsPanel.ClosePanel();
                mainMenuPanel.OpenPanel();
            }
            else if (creditsPanel.IsOpen)
            {
                creditsPanel.ClosePanel();
                mainMenuPanel.OpenPanel();
            }
            else if (mainMenuPanel.IsOpen)
            {
                OnExitPressed();
            }
        }
    }

    public void OnPlayPressed()
    {
        mainMenuPanel.ClosePanel();
        SceneManager.LoadScene("ShooterScene");
    }

    public void OnCreditsPressed()
    {
        mainMenuPanel.ClosePanel();
        settingsPanel.ClosePanel();
        creditsPanel.OpenPanel();
    }

    public void OnSettingsPressed()
    {
        mainMenuPanel.ClosePanel();
        creditsPanel.ClosePanel();
        settingsPanel.OpenPanel();
    }

    public void OnBackToMenuPressed()
    {
        settingsPanel.ClosePanel();
        creditsPanel.ClosePanel();
        mainMenuPanel.OpenPanel();
    }

    public void OnExitPressed()
    {
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
