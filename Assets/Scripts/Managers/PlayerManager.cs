using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerManager : MonoBehaviour
{
    public PlayerProfileSO playerProfile;

    public int playerIndex; 
    public int turn;
    [Space]
    public int ExplorationPointsMax; //The maximum amount of points you have to flip tiles 
    public int ExplorationPointsLeft; //How many points you haveleft this turn. 
    [Space]
    public int MetalScrapAmount; //The Amount of MetalScrap Resources you have in Total 
    [Space]
    public int maintenanceResource; // NewEden: Oxygen, SAPIEN: Power, StarBorn: Energon, CyberSwarm: Bio-Matter
    [Space]
    public int ResearchPoints; // Points used for researching things 
    public int SuperPowerPoints; //Used to unleash Super Powers

    public UnityEvent playerRelics; 
    public UnityEvent playerMalfunctions;

    public void StartTurn()
    {
        if(turn < 1)
        {
            FirstTurnSequence(); 
        }

        turn += 1;
    }

    private void FirstTurnSequence()
    {
        Debug.Log("First Turn!!");
        //Check for Assigned Spawn Tile

        bool canSpawn = false;
        
        HexTileGeneration _generator = FindObjectOfType<HexTileGeneration>();

        List<TileInfo> _availableTiles = new List<TileInfo>();

        for (int i = 0; i < _generator.spawnTiles.Count; i++)
        {
            if(_generator.spawnTiles[i].AssignedTileOwnerForSetup == TileInfo.tileOwners.none)
            {
                _availableTiles.Add(_generator.spawnTiles[i]);
            }

            if((int)_generator.spawnTiles[i].AssignedTileOwnerForSetup == playerIndex)
            {
                _generator.spawnTiles[i].owner = this;
                transform.position = _generator.spawnTiles[i].transform.position;
                canSpawn = true;
                SpawnHQ(); 
                break; 
            }
            
        }

        if (!canSpawn)
        {
            if (_availableTiles.Count > 0)
            {
                int randomIndex = Random.Range(0, _availableTiles.Count);
                TileInfo randomTile = _availableTiles[randomIndex];

                randomTile.owner = this;
                transform.position = randomTile.transform.position;
                canSpawn = true;
                SpawnHQ();
            }
            else
            {
                Debug.LogWarning("No available spawn tiles for player profile: " + playerProfile);
            }
        }
    }

    void SpawnHQ()
    {
        Debug.Log("Can Spawn HQ"); 
        GameManager.Instance?.PlaceBuilding(transform.position, (int)StructureType.Headquarters, this);
    }
}
