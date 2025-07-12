using UnityEngine;
using DG.Tweening;

public abstract class BasePanel : MonoBehaviour, IBasePanel
{
    [SerializeField] protected CanvasGroup canvasGroup;
    protected Tween currentTween;

    [SerializeField] protected float transitionDuration = 0.25f;
    [SerializeField] protected Ease transitionEase = Ease.OutCubic;
    public bool IsOpen;

    protected virtual void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public virtual void OpenPanel()
    {
        currentTween?.Kill();
        IsOpen = true;
        //SoundController.RequestSound(SoundID.ButtonClick);
        gameObject.SetActive(true);
        currentTween = canvasGroup.DOFade(1f, transitionDuration)
            .SetEase(transitionEase)
            .SetUpdate(true)
            .OnStart(() =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
    }

    public virtual void ClosePanel()
    {
        currentTween?.Kill();
        IsOpen = false;
        currentTween = canvasGroup.DOFade(0f, transitionDuration)
            .SetEase(transitionEase)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                gameObject.SetActive(false);
            });
    }

    public virtual void HidePanel() 
    {
        currentTween?.Kill();
        IsOpen = false;
        currentTween = canvasGroup.DOFade(0f, transitionDuration)
            .SetEase(transitionEase)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
    }
}