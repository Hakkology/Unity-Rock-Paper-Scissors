using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MenuSettingsPanel : SettingsBasePanel
{
    public void OnBackToMenuPressedWithSave()
    {
        SoundManager.Instance.soundController.RequestSound(SoundID.ButtonClick);
        GameSettings.Instance.SaveSettings();
        MenuGUIManager.Instance.settingsPanel.ClosePanel();
        MenuGUIManager.Instance.creditsPanel.ClosePanel();
        MenuGUIManager.Instance.mainMenuPanel.OpenPanel();
    }
}
