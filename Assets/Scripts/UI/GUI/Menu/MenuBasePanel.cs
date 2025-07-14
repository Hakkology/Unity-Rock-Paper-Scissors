using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBasePanel : BasePanel 
{
    public async void OnPlayPressed()
    {
        MenuGUIManager.Instance.mainMenuPanel.ClosePanel();
        SoundManager.Instance.soundController.RequestSound(SoundID.ButtonClick);
        await Task.Delay(300); 
        SceneManager.LoadScene("Arena");
    }

    public void OnCreditsPressed()
    {
        SoundManager.Instance.soundController.RequestSound(SoundID.ButtonClick);
        MenuGUIManager.Instance.mainMenuPanel.ClosePanel();
        MenuGUIManager.Instance.settingsPanel.ClosePanel();
        MenuGUIManager.Instance.creditsPanel.OpenPanel();
    }

    public void OnSettingsPressed()
    {
        SoundManager.Instance.soundController.RequestSound(SoundID.ButtonClick);
        MenuGUIManager.Instance.mainMenuPanel.ClosePanel();
        MenuGUIManager.Instance.creditsPanel.ClosePanel();
        MenuGUIManager.Instance.settingsPanel.OpenPanel();
    }
}