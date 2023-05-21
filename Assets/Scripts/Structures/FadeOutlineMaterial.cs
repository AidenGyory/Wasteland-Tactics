using UnityEngine;
using DG.Tweening;

public class FadeOutlineMaterial : MonoBehaviour
{
    public Renderer OutlineRenderer;
    public Color originalColor; 

    private void Start()
    {
        TurnOffOutline();
        originalColor = OutlineRenderer.material.color;
    }
    public void TurnOnOutline()
    {
        OutlineRenderer.material.DOColor(originalColor, 0.3f);
    }

    public void TurnOffOutline()
    {
        OutlineRenderer.material.DOColor(Color.clear, 0.3f);
    }
}
