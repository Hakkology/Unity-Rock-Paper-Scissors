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

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }
    [SerializeField] private PlayerPanel playerPanel;

    private bool _gameStart;
    private PSideChoice pSideChoice = PSideChoice.Rock;
    private PTeamChoice pTeamChoice = PTeamChoice.Red;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartGame()
    {

    }


    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(.5f);
        Arena.Instance.ResetArena();
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