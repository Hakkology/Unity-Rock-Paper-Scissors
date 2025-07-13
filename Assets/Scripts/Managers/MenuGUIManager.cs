using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGUIManager : MonoBehaviour
{
    public static MenuGUIManager Instance { get; private set; }

    [Header("Panels")]
    public BasePanel mainMenuPanel;
    public BasePanel settingsPanel;
    public BasePanel creditsPanel;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        settingsPanel.ClosePanel();
        creditsPanel.ClosePanel();
        mainMenuPanel.OpenPanel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel.IsOpen)
            {
                settingsPanel.ClosePanel();
                mainMenuPanel.OpenPanel();
            }
            else if (creditsPanel.IsOpen)
            {
                creditsPanel.ClosePanel();
                mainMenuPanel.OpenPanel();
            }
        }
    }

    public void OnBackToMenuPressed()
    {
        settingsPanel.ClosePanel();
        creditsPanel.ClosePanel();
        mainMenuPanel.OpenPanel();
    }

    // public void OnExitPressed()
    // {
    //     Application.Quit();
    // #if UNITY_EDITOR
    //     UnityEditor.EditorApplication.isPlaying = false;
    // #endif
    // }
}
