using UnityEngine;
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
        Game.Instance.SetPlayerTeam(choice);
    }

    private void SelectSide(PSideChoice choice)
    {
        Game.Instance.SetPlayerSide(choice);
    }
}
