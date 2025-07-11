using UnityEngine;
using UnityEngine.UI;

public class Forcefield : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image image;
    [SerializeField] private Material forcefieldMaterial;

    void Awake()
    {
        if (image != null && forcefieldMaterial != null)
            image.material = forcefieldMaterial;
    }

    public void SetIdle()
    {
        SetShaderParams(0f, 1f);
    }

    public void SetBlueWins()
    {
        SetShaderParams(1f, 1f);
    }

    public void SetRedWins()
    {
        SetShaderParams(-1f, 1f);
    }

    public void Hide()
    {
        SetShaderParams(0f, 0f);
    }

    private void SetShaderParams(float direction, float strength)
    {
        if (forcefieldMaterial != null)
        {
            forcefieldMaterial.SetFloat("_Direction", direction);
            forcefieldMaterial.SetFloat("_Strength", strength);
        }
    }
}