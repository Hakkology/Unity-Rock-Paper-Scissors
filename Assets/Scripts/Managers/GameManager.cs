using System.Collections;
using UnityEngine;

public enum PTeamChoice
{
    Red,
    Blue
}

public enum PSideChoice
{
    Rock,
    Paper,
    Scissors,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private PSideChoice pSideChoice = PSideChoice.Rock;
    private PTeamChoice pTeamChoice = PTeamChoice.Red;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(.5f);
        Arena.Instance.RestartArena();
    }

    public IEnumerator GameOver(PTeamChoice winningTeam, PSideChoice winningSide)
    {
        yield return new WaitForSeconds(.5f);
    }
    
    public void SetPlayerTeam(PTeamChoice choice)
    {
        pTeamChoice = choice;
        Debug.Log("Team selected: " + choice);
    }

    public void SetPlayerSide(PSideChoice choice)
    {
        pSideChoice = choice;
        Debug.Log("Side selected: " + choice);
    }

}