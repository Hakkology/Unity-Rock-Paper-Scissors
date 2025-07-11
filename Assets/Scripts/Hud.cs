using System.Linq;
using TMPro;
using UnityEngine;

public class Hud : MonoBehaviour
{
    public static Hud Instance;

    [SerializeField]
    private TextMeshProUGUI leftRockText;
    [SerializeField]
    private TextMeshProUGUI rightRockText;
    [SerializeField]
    private TextMeshProUGUI leftPaperText;
    [SerializeField]
    private TextMeshProUGUI rightPaperText;
    [SerializeField]
    private TextMeshProUGUI leftScissorText;
    [SerializeField]
    private TextMeshProUGUI rightScissorText;
    [SerializeField]
    private TextMeshProUGUI leftTotalText;
    [SerializeField]
    private TextMeshProUGUI rightTotalText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateLeftRockText(string message) => leftRockText.text = message;
    public void UpdateRightRockText(string message) => rightRockText.text = message;
    public void UpdateLeftPaperText(string message) => leftPaperText.text = message;
    public void UpdateRightPaperText(string message) => rightPaperText.text = message;
    public void UpdateLeftScissorText(string message) => leftScissorText.text = message;
    public void UpdateRightScissorText(string message) => rightScissorText.text = message;
    public void UpdateLeftTotalText(string message) => leftTotalText.text = message;
    public void UpdateRightTotalText(string message) => rightTotalText.text = message;

    public void UpdateEntityCounters()
    {
        UpdateLeftRockText(Arena.Instance.LeftRocks.Count().ToString());
        UpdateLeftPaperText(Arena.Instance.LeftPapers.Count().ToString());
        UpdateLeftScissorText(Arena.Instance.LeftScissors.Count().ToString());

        UpdateRightRockText(Arena.Instance.RightRocks.Count().ToString());
        UpdateRightPaperText(Arena.Instance.RightPapers.Count().ToString());
        UpdateRightScissorText(Arena.Instance.RightScissors.Count().ToString());

        UpdateLeftTotalText(Arena.Instance.LeftEntities.Count().ToString());
        UpdateRightTotalText(Arena.Instance.RightEntities.Count().ToString());
    }
}