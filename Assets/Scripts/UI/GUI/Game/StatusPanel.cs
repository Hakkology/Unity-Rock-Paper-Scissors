using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : BasePanel
{
    [Header("Star Images")]
    [SerializeField] private Sprite passiveStar;
    [SerializeField] private Sprite activeStar;

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private Image leftTeamImage;
    [SerializeField] private Image leftSideImage;

    public void TogglePanel(bool open, bool hasWinner, bool sidewins, bool teamwins,  string text)
    {
        if (open)
            ShowStatusPanel(hasWinner, sidewins, teamwins, text);
        else
            this.ClosePanel();
    }

    public void ShowStatusPanel(bool hasWinner, bool sidewins, bool teamwins, string text)
    {
        this.OpenPanel();
        UpdateUIElements(hasWinner, sidewins, teamwins, text);
    }

    public void UpdateUIElements(bool hasWinner, bool sidewins, bool teamwins, string text)
    {
        if (hasWinner)
        {
            if (!sidewins && !teamwins)
            {
                statusText.text = "FAILED!";
                leftSideImage.sprite = passiveStar;
                leftTeamImage.sprite = passiveStar;
                text = text + " (0/2)";
            }
            else if (sidewins && !teamwins)
            {
                statusText.text = "NOT BAD!";
                leftSideImage.sprite = activeStar;
                leftTeamImage.sprite = passiveStar;
                text = text + " (1/2)";
            }
            else if (!sidewins && teamwins)
            {
                statusText.text = "NOT BAD!";
                leftSideImage.sprite = passiveStar;
                leftTeamImage.sprite = activeStar;
                text = text + " (1/2)";
            }
            else
            {
                statusText.text = "PERFECT!";
                leftSideImage.sprite = activeStar;
                leftTeamImage.sprite = activeStar;
                text = text + " (2/2)";
            }
        }
        else
        {
            statusText.text = "RARE!";
            leftSideImage.sprite = passiveStar;
            leftTeamImage.sprite = passiveStar;
        }

        winnerText.text = text;
    }
}