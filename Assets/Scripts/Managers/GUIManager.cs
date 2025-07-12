using UnityEngine;

public class GUIManager : MonoBehaviour
{

    public static GUIManager Instance { get; private set; }
    public PlayerPanel playerPanel;
    public StatusPanel statusPanel;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}