using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MenuSettingsPanel : SettingsBasePanel
{
    public void OnBackToMenuPressedWithSave()
    {
        GameSettings.Instance.SaveSettings();
        MenuGUIManager.Instance.settingsPanel.ClosePanel();
        MenuGUIManager.Instance.creditsPanel.ClosePanel();
        MenuGUIManager.Instance.mainMenuPanel.OpenPanel();
    }
}
