using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

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

    public void DisableFields()
    {
        if (leftField != null)
        {
            var color = leftField.color;
            color.a = 0f;
            leftField.color = color;
        }

        if (rightField != null)
        {
            var color = rightField.color;
            color.a = 0f;
            rightField.color = color;
        }
    }

    public void ActivateFields()
    {
        if (leftField != null)
        {
            leftField.DOFade(1f, fadeDuration);
        }

        if (rightField != null)
        {
            rightField.DOFade(1f, fadeDuration);
        }
    }
}
