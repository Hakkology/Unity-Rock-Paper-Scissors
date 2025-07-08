using System.Linq;
using UnityEngine;

[ExecuteAlways]
public class ArenaBackgroundController : MonoBehaviour
{
    [SerializeField] private Material backgroundMaterial;

    void Update()
    {
        if (!backgroundMaterial || !Arena.Instance) return;

        float total = Arena.Instance.AllEntities.Count;
        if (total == 0) total = 1; // avoid divide by zero

        float rocks = Arena.Instance.Rocks.Count();
        float papers = Arena.Instance.Papers.Count();
        float scissors = Arena.Instance.Scissors.Count();

        float rockRatio = rocks / total;
        float paperRatio = papers / total;
        float scissorRatio = scissors / total;

        backgroundMaterial.SetFloat("_RockRatio", rockRatio);
        backgroundMaterial.SetFloat("_PaperRatio", paperRatio);
        backgroundMaterial.SetFloat("_ScissorsRatio", scissorRatio);
    }
}
