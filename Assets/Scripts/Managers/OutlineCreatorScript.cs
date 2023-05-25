using UnityEngine;

public class OutlineCreatorScript : MonoBehaviour
{
    public Material outlineMaterial;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColour;
    private Renderer _outlineRenderer;
    
    private static readonly int OutlineColor = Shader.PropertyToID("_OutlineColor");
    private static readonly int Scale = Shader.PropertyToID("_Scale");

    private void Start()
    {
        _outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColour);
        _outlineRenderer.enabled = true;
    }

    Renderer CreateOutline(Material outlineMat, float scaleFactor, Color color)
    {
        Transform parent;
        
        var outlineObject = Instantiate(this.gameObject, (parent = transform).position, parent.rotation, parent);
        var rend = outlineObject.GetComponent<Renderer>();

        rend.material = outlineMat; 
        rend.material.SetColor(OutlineColor, color);
        rend.material.SetFloat(Scale,scaleFactor);
        
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineObject.GetComponent<OutlineCreatorScript>().enabled = false;

        var o = gameObject;
        outlineObject.name = $"{o.name} Outline";
        outlineObject.transform.localScale = Vector3.one;
        
        transform.GetComponentInParent<SelectScript>().SetOutline(o); 
        
        if (outlineObject.GetComponent<Collider>() != null)
        {
            outlineObject.GetComponent<Collider>().enabled = false;
        }

        rend.enabled = false;

        return rend; 
    }
}
