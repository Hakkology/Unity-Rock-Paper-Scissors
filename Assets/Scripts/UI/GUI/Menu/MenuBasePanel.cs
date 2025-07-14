using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBasePanel : BasePanel
{
    public void OnPlayPressed()
    {
        //MenuGUIManager.Instance.mainMenuPanel.ClosePanel();
        SoundManager.Instance.soundController.RequestSound(SoundID.ButtonClick);
        StartCoroutine(DelayedSceneLoad());
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

    private IEnumerator DelayedSceneLoad()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Arena");
        
    }
}