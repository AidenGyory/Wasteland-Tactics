using System.Transactions;
using UnityEngine;

public class StructurePlacementScript : MonoBehaviour
{
    [SerializeField] GameObject[] NewEdenStructures;
    [SerializeField] GameObject[] SAPIENStructures;
    [SerializeField] GameObject[] StarbornStructures;
    [SerializeField] GameObject[] CyberswarmStructures;

    public int structureIndex;
    public PlayerManager playerInfo;

    public void PlaceStructure()
    {
        GameObject _structure = null;

        switch (playerInfo.playerProfile.playerFaction)
        {
            case FactionType.NewEden:
                 _structure = Instantiate(NewEdenStructures[structureIndex]); 
                break;
            case FactionType.Sapien:
                 _structure = Instantiate(NewEdenStructures[structureIndex]);

                break;
            case FactionType.StarBorn:
                 _structure = Instantiate(NewEdenStructures[structureIndex]);

                break;
            case FactionType.CyberSwarm:
                 _structure = Instantiate(NewEdenStructures[structureIndex]);

                break;
            default:
                break;
        }

        _structure.transform.position = transform.position;
        StructureInfo _info = _structure.GetComponent<StructureInfo>(); 
        _info.owner = playerInfo;
        _info.UpdateMaterials(); 


        _structure.transform.SetParent(playerInfo.transform);
    }
}
