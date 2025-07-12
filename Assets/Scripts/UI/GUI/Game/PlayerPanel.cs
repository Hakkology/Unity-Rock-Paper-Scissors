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

        redTeamButton.onClick.AddListener(() => SelectTeam(PTeamChoice.Red));
        blueTeamButton.onClick.AddListener(() => SelectTeam(PTeamChoice.Blue));

        rockButton.onClick.AddListener(() => SelectSide(PSideChoice.Rock));
        paperButton.onClick.AddListener(() => SelectSide(PSideChoice.Paper));
        scissorsButton.onClick.AddListener(() => SelectSide(PSideChoice.Scissors));
    }

    private void SelectTeam(PTeamChoice choice)
    {
        GameManager.Instance.SetPlayerTeam(choice);
        teamInfoText.text = $"Team: {choice}";
    }

    private void SelectSide(PSideChoice choice)
    {
        GameManager.Instance.SetPlayerSide(choice);
        sideInfoText.text = $"Side: {choice}";
    }

    public void StartGame()
    {
        StartCoroutine(GameManager.Instance.StartGame());
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
