using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Space]
    [HideInInspector]
    public bool randomiseTurnOrder;
    
    [PropertyOrder(-1)]
    [HideIf("randomiseTurnOrder")]
    [Button(ButtonSizes.Large), GUIColor(0.5f, 1, 0.5f)]
    private void StandardTurnOrder()
    {
        this.randomiseTurnOrder = !this.randomiseTurnOrder;
    }
    
    [PropertyOrder(-1)]
    [ShowIf("randomiseTurnOrder")]
    [Button(ButtonSizes.Large), GUIColor(1f, 1, 0.5f)]
    private void RandomiseTurnOrder()
    {
        this.randomiseTurnOrder = !this.randomiseTurnOrder;
    }
    [InfoBox(" VV -- WARNING: IF 'Players' COMPONENT IS EMPTY, PLAYERS START ORDER WILL BE RANDOM ONCE FOUND-- VV")]
    [Space]
    public List<PlayerManager> players;
    [Space]
    public PlayerManager currentPlayersTurn; 
    public int globalTurnCounter;
    [PropertySpace(SpaceBefore = 15, SpaceAfter = 10)]
    public GameObject structurePlacementPrefab; 
    
    private int _playerTurnIndex;
    void Awake()
    {
        Instance = this;
    }
    public void StartGame()
    {
        Debug.Log("Start Game");
        if (players.Count < 1 || randomiseTurnOrder) { AddPlayersToMatch(); }   

        if (players.Count < 1) { CantLoadLevel(); Debug.Log("not enough players!!"); }

        if(!CheckSpawnTiles()) { CantLoadLevel(); Debug.Log("Spawn tiles don't match amount of players!"); }

        SetPlayerIndex(); 

        _playerTurnIndex = 0;
        globalTurnCounter += 1; 

        BeginNewTurnSequence(); 
    }
    void AddPlayersToMatch()
    {
        //clear player list
        players.Clear();

        // Get all PlayerManager components in the scene
        PlayerManager[] allPlayerManagers = FindObjectsOfType<PlayerManager>();

        // Create a list from the array
        players = new List<PlayerManager>(allPlayerManagers);

        // Shuffle the list using the Sort() method with a custom comparison delegate
        players.Sort((a, b) => Guid.NewGuid().CompareTo(Guid.NewGuid()));
    }

    void SetPlayerIndex()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].playerIndex = i + 1;
        }
    }
    bool CheckSpawnTiles()
    {
        bool canPlay = true;

        HexTileGeneration _generator = FindObjectOfType<HexTileGeneration>();

        if (_generator.spawnTiles.Count != players.Count)
        {
            canPlay = false;
        }

        return canPlay;
    }
    void CantLoadLevel()
    {
        Debug.LogWarning("Can't Load Level!!!"); 
    }
    public void BeginNewTurnSequence()
    {
        currentPlayersTurn = players[_playerTurnIndex];
        Debug.Log("Player: " + currentPlayersTurn.playerProfile.playerProfileName + " Turn Start!");
        currentPlayersTurn.StartTurn(); 
    }
    [Button(ButtonSizes.Large), GUIColor(1, 1, 1)]
    public void EndTurnSequence()
    {
        if(_playerTurnIndex +1 > players.Count) 
        { 
            _playerTurnIndex = 0;
            globalTurnCounter += 1;
        } 
        else
        {
            _playerTurnIndex += 1;
        }
        BeginNewTurnSequence();
    }

    public void PlaceBuilding(Vector3 _position, int _structureIndex, PlayerManager _playerInfo)
    {
        GameObject _placeStructure = Instantiate(structurePlacementPrefab); 
        _placeStructure.transform.position = _position;
        _placeStructure.GetComponent<StructurePlacementScript>().structureIndex = _structureIndex;
        _placeStructure.GetComponent<StructurePlacementScript>().playerInfo = _playerInfo;
        _placeStructure.GetComponent<StructurePlacementScript>().PlaceStructure(); 

        //Destroy(_placeStructure); 
    }



}
