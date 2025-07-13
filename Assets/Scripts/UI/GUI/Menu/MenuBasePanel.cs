using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBasePanel : BasePanel 
{
    public void OnPlayPressed()
    {
        MenuGUIManager.Instance.mainMenuPanel.ClosePanel();
        SceneManager.LoadScene("Arena");
    }

    public void OnCreditsPressed()
    {
        MenuGUIManager.Instance.mainMenuPanel.ClosePanel();
        MenuGUIManager.Instance.settingsPanel.ClosePanel();
        MenuGUIManager.Instance.creditsPanel.OpenPanel();
    }

    public void OnSettingsPressed()
    {
        MenuGUIManager.Instance.mainMenuPanel.ClosePanel();
        MenuGUIManager.Instance.creditsPanel.ClosePanel();
        MenuGUIManager.Instance.settingsPanel.OpenPanel();
    }

    public void OnBackToMenuPressed()
    {
        MenuGUIManager.Instance.settingsPanel.ClosePanel();
        MenuGUIManager.Instance.creditsPanel.ClosePanel();
        MenuGUIManager.Instance.mainMenuPanel.OpenPanel();
    }
}