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
    [SerializeField] private Image leftTeamImage;
    [SerializeField] private Image leftSideImage;

    public void TogglePanel(bool open)
    {
        if (open)
            this.OpenPanel();
        else
            this.ClosePanel();
    }

    public void UpdateUIElements(bool sidewins, bool teamwins)
    {
        if (!sidewins && !teamwins)
        {
            statusText.text = "FAILED!";
            leftSideImage.sprite = passiveStar;
            leftTeamImage.sprite = passiveStar;
        }
        else if (sidewins && !teamwins)
        {
            statusText.text = "NOT BAD!";
            leftSideImage.sprite = activeStar;
            leftTeamImage.sprite = passiveStar;
        }
        else if (!sidewins && teamwins)
        {
            statusText.text = "NOT BAD!";
            leftSideImage.sprite = passiveStar;
            leftTeamImage.sprite = activeStar;
        }
        else
        {
            statusText.text = "PERFECT!";
            leftSideImage.sprite = activeStar;
            leftTeamImage.sprite = activeStar;
        }
    }
}