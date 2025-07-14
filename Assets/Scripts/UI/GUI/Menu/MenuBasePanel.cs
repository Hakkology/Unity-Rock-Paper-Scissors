using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBasePanel : BasePanel 
{
    public async void OnPlayPressed()
    {
        MenuGUIManager.Instance.mainMenuPanel.ClosePanel();
        await Task.Delay(300); 
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
}