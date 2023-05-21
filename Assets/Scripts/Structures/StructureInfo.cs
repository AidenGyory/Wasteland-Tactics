using System.Collections.Generic;
using System.Diagnostics;
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
        outlineRenderer.gameObject.SetActive(false); 
    }

    public void SelectStructure()
    {

    }

    public void UnselectStructure()
    {

    }

    public void HighlightStructure()
    {

    }

    public void UnhighlightStructure()
    {

    }

}
