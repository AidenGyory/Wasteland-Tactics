using UnityEngine;
using DG.Tweening;
using TMPro;

public class TileEffects : MonoBehaviour
{
    bool ishighlighted;

    public void turnOnHighlightEffect(bool _ishighlighted)
    {
        ishighlighted = _ishighlighted;
        if (ishighlighted)
        {
            Renderer[] _allRenderers = GetComponentsInChildren<Renderer>();

            foreach (Renderer _renderer in _allRenderers)
            {
                foreach (Material _mat in _renderer.materials)
                {
                    _mat.DOColor(_mat.color * 0.5f, 0.3f);
                }
            }
        }
        else
        {
            Renderer[] _allRenderers = GetComponentsInChildren<Renderer>();

            foreach (Renderer _renderer in _allRenderers)
            {
                foreach (Material _mat in _renderer.materials)
                {
                    _mat.DOColor(_mat.color * -0.5f, 0.3f);
                }
            }
        }
    }
}
