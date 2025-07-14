using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerPanel : BasePanel
{
    [Header("Team Buttons")]
    [SerializeField] private Button redTeamButton;
    [SerializeField] private Button blueTeamButton;

    [Header("Side Buttons")]
    [SerializeField] private Button rockButton;
    [SerializeField] private Button paperButton;
    [SerializeField] private Button scissorsButton;

    [Header("Info Texts")]
    [SerializeField] private TextMeshProUGUI teamInfoText;
    [SerializeField] private TextMeshProUGUI sideInfoText;

    public void TogglePanel(bool open)
    {
        if (open)
            this.OpenPanel();
        else
            this.ClosePanel();
    }

    protected override void Awake()
    {
        base.Awake();

        redTeamButton.onClick.AddListener(() => SelectTeam(PTeam.Red));
        blueTeamButton.onClick.AddListener(() => SelectTeam(PTeam.Blue));

        rockButton.onClick.AddListener(() => SelectSide(EType.Rock));
        paperButton.onClick.AddListener(() => SelectSide(EType.Paper));
        scissorsButton.onClick.AddListener(() => SelectSide(EType.Scissors));
    }

    private void SelectTeam(PTeam? choice)
    {
        SoundManager.Instance.soundController.RequestSound(SoundID.ButtonClick);
        if (choice == null)
        {
            teamInfoText.text = $"Choose Team";
        }
        else
        {
            GameManager.Instance.SetPlayerTeam(choice.Value);
            teamInfoText.text = $"Team: {choice}";
        }

    }

    private void SelectSide(EType? choice)
    {
        SoundManager.Instance.soundController.RequestSound(SoundID.ButtonClick);
        if (choice == null)
        {
            teamInfoText.text = $"Choose Side";
        }
        else
        {
            GameManager.Instance.SetPlayerSide(choice.Value);
            sideInfoText.text = $"Side: {choice}";
        }
    }

    public void ResetPanel()
    {
        GameManager.Instance.ResetPlayerChoices();
        SelectSide(null);
        SelectTeam(null);
    }

    public void StartGame()
    {
        SoundManager.Instance.soundController.RequestSound(SoundID.GameStart);
        StartCoroutine(GameManager.Instance.StartGame());
    }

    public void GoToMenu()
    {
        SoundManager.Instance.soundController.RequestSound(SoundID.ButtonClick);
        SceneManager.LoadScene("Menu");
    }
}
