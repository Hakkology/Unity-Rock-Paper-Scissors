using UnityEngine;
using TMPro;

public class CreditsPanel : BasePanel
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI developerText;
    [SerializeField] private TextMeshProUGUI disclaimerText;   

    [Header("Content")]
    [SerializeField] private string developerName = "Your Name";
    [SerializeField, TextArea] 
    private string disclaimer = 
@"All assets are randomly added to the game for this portfolio project. Feel free to contact me if you wish your name to be referenced."; 

    protected override void Awake()
    {
        base.Awake();

        headerText.text     = "FATALIX SOFT\nWEB GAMES";
        developerText.text  = $"Developed by \n{developerName}";
        disclaimerText.text = disclaimer;   
    }
}
