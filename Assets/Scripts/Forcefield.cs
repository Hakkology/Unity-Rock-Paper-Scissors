using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Forcefield : MonoBehaviour
{
    [SerializeField] private Image leftField;
    [SerializeField] private Image rightField;
    [SerializeField] private float fadeDuration = 0.5f;

    public void DisableLeftField()
    {
        if (leftField != null)
        {
            leftField.DOFade(0f, fadeDuration);
        }
    }

    public void DisableRightField()
    {
        if (rightField != null)
        {
            rightField.DOFade(0f, fadeDuration);
        }
    }
}
