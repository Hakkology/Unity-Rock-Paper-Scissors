using System.Collections;
using UnityEngine;

public enum PTeam
{
    Red,
    Blue
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private EType? pTypeChoice = null;
    private PTeam? pTeamChoice = null;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public IEnumerator StartGame()
    {
        if (pTeamChoice == null || pTypeChoice == null)
        {
            Debug.Log("Choose team or side.");
            yield break;
        }

        HUDManager.Instance.UpdatePlayerText("You: " + pTeamChoice.ToString() + " " + pTypeChoice.ToString());

        yield return new WaitForSeconds(.5f);
        Arena.Instance.RestartArena();
    }

    public IEnumerator GameOver(bool hasWinner, PTeam winningTeam = default, EType winningSide = default)
    {
        yield return new WaitForSeconds(.5f);
        HUDManager.Instance.DisableHud();

        if (hasWinner)
        {
            string text = winningTeam.ToString() + " " + winningSide.ToString() + " wins";
            bool isPlayerWinnerType = pTypeChoice == winningSide;
            bool isPlayerWinningTeam = pTeamChoice == winningTeam;
            GUIManager.Instance.statusPanel.ShowStatusPanel(hasWinner, isPlayerWinnerType, isPlayerWinningTeam, text);
        }
        else
        {
            GUIManager.Instance.statusPanel.ShowStatusPanel(hasWinner, false, false, "");
        }

        GUIManager.Instance.playerPanel.ResetPanel();
        GUIManager.Instance.playerPanel.TogglePanel(true);
    }

    public void SetPlayerTeam(PTeam choice)
    {
        pTeamChoice = choice;
        Debug.Log("Team selected: " + choice);
    }

    public void SetPlayerSide(EType choice)
    {
        pTypeChoice = choice;
        Debug.Log("Side selected: " + choice);
    }

    public void ResetPlayerChoices()
    {
        pTeamChoice = null;
        pTypeChoice = null;
    }

}