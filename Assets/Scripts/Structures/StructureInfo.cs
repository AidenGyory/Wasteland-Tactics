using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public enum StructureType
{
    Headquarters,
    Outpost,
    Generator,
    Refinery,
    ResearchLab,
}
public class StructureInfo : MonoBehaviour
{
    public string structureName;
    public FactionType faction;
    public StructureType structureType;
    [Space]
    public PlayerManager owner;
    public int currentLevel;
    [Space]
    public int powerUsedPerTurn;
    public int powerGeneratedPerTurn;
    [Space]
    public int resourcesUsedPerTurn; 
    public int resourcesGeneratedPerTurn;
    [Space]
    public int StructureMaxHealth; 
    public int StructureCurrentHealth;
    public bool isAlive;
    [Space]
    public TileInfo OccupiedTile;
    [Space]
    [SerializeField] Renderer[] modelRenderer;
    [SerializeField] Renderer outlineRenderer;
    private Color originalOutlineColour; 

    
    public void UpdateMaterials()
    {
        List<Material> _materials = new List<Material>();

        switch (structureType)
        {
            case StructureType.Headquarters:
                _materials.AddRange(owner.playerProfile.colourProfile.Headquarters);
                break;
            case StructureType.Outpost:
                _materials.AddRange(owner.playerProfile.colourProfile.Outpost);
                break;
            case StructureType.Generator:
                _materials.AddRange(owner.playerProfile.colourProfile.Generator);
                break;
            case StructureType.Refinery:
                _materials.AddRange(owner.playerProfile.colourProfile.Refinery);
                break;
            case StructureType.ResearchLab:
                _materials.AddRange(owner.playerProfile.colourProfile.ResearchLab);
                break;
            default:
                break;
        }


        if(_materials.Count > 1)
        {
            for (int i = 0; i < _materials.Count; i++)
            {
                if (modelRenderer[i]?.materials.Length > 1)
                {
                    for (int j = 0; j < modelRenderer[i].materials.Length; j++)
                    {
                        modelRenderer[i].materials[j] = _materials[i];
                    }
                }
                else
                {
                    modelRenderer[i].material = _materials[i];
                }
            }
        }
        else
        {
            modelRenderer[0].material = _materials[0];
        }
        
        outlineRenderer.material = owner.playerProfile.colourProfile.OutlineMaterial;
        originalOutlineColour = outlineRenderer.material.color; 
        outlineRenderer.material.color = Color.clear; 
    }

    public void SelectStructure()
    {
        Camera.main.GetComponent<CameraFollow>().LerpToPosition(this.transform.position); 
    }

    public void UnselectStructure()
    {
        
    }

    public void HighlightStructure()
    {
        outlineRenderer.material.DOColor(originalOutlineColour, 0.3f);
    }

    public void UnhighlightStructure()
    {
        outlineRenderer.material.DOColor(Color.clear, 0.3f);

    }

}
