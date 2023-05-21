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
    [SerializeField] Renderer[] modelRenderers;

    public void UpdateMaterials()
    {
        for (int i = 0; i < modelRenderers.Length; i++)
        {
            
        }
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
