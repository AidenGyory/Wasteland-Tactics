using UnityEngine;
using DG.Tweening;
using MoreMountains.Feedbacks;

public class TileInfo : MonoBehaviour
{
    public enum TileType
    {
        random, //gray 
        spawnTile, //Green
        empty, //white
        resource1, //blue
        resource2, //light blue
        power, //yellow
        hazard, //red
        reward, //purple
        mountain, //Orange
    }

    public enum TileState
    {
        AlreadyFlipped,
        Flippable,
        NotFlippable,
    }

    public enum tileOwners
    {
        none, 
        player1,
        player2,
        player3,
        player4,
    }
    public PlayerManager owner;
    [Space]
    [Header("Start up Tile Data")]
    public tileOwners AssignedTileOwnerForSetup;
    public Vector2 tileCoords;
    [Space]
    public TileType tileType;
    public TileState tileState;
    [Space]
    
    [Header("Tile Data for Map Generation")]
    public TileMapData mapData;
    [SerializeField] GameObject[] _tileModels;

    private TileType _previousTileType; 
    private TileState _previousTileState;

    public MMF_Player flipFeedback;

    
    private Color _bottomMaterialColor; 
    private Color _topMaterialColor;

    public void SaveTileDataToMapData()
    {
        mapData.tileCoords = tileCoords;
        mapData.tileType = tileType;
        mapData.tileState = tileState;
        mapData.AssignedTileOwnerOnStart = AssignedTileOwnerForSetup; 
    }

    private void OnValidate()
    {
        if (tileType != _previousTileType || tileState != _previousTileState)
        {
            _previousTileType = tileType;
            _previousTileState = tileState;
            RefreshTileInfo();
        }
    }


    private void Start()
    {
        
        if(tileType == TileType.random)
        {
            int _rand = Random.Range(0, 7);
            tileType = (TileType)(_rand + 2); 
        }
        RefreshTileInfo();

        _bottomMaterialColor = GetComponent<Renderer>().material.color;
        _topMaterialColor = _tileModels[(int)tileType].GetComponentInChildren<Renderer>().material.color;
        
    }

    public void RefreshTileInfo()
    {
        for (int i = 0; i < _tileModels.Length; i++)
        {
            if(i != (int)tileType)
            {
                _tileModels[i].SetActive(false);
            }
            else
            {
                _tileModels[i].SetActive(true);
            }
        }

        if(tileState == TileState.AlreadyFlipped)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else
        {
            transform.eulerAngles = new Vector3(-180, 0, 0);
        }

        SaveTileDataToMapData(); // - Save current info to MapData Constructor for easy "Save" mode
    }

    
    public void TileSelected()
    {
        Debug.Log("selected tile at: " + tileCoords);
        //Plug this into the Select Script "Selectedbject" Event

        if (tileState == TileState.Flippable)
        {
            FlipTile();
        }
        else
        {
            Debug.Log("Can't Flip");
            GetComponent<Renderer>().material.DOColor(_bottomMaterialColor * SelectObjectScript.Instance.brightness, 0.2f).SetLoops(-1, LoopType.Yoyo);
            _tileModels[(int)tileType].GetComponentInChildren<Renderer>().material.DOColor(_topMaterialColor * SelectObjectScript.Instance.brightness, 0.2f).SetLoops(-1, LoopType.Yoyo);
        }
    }

    public void TileUnselected()
    {
        Debug.Log("unselected tile at: " + tileCoords);
        //Plug this into the Select Script "Unselectedbject" Event
    }

    public void TileHighlighted()
    {
        Debug.Log("highlighted tile at: " + tileCoords);
        GetComponent<Renderer>().material.DOColor(_bottomMaterialColor * SelectObjectScript.Instance.brightness, 0.3f);
        _tileModels[(int)tileType].GetComponentInChildren<Renderer>().material.DOColor(_topMaterialColor * SelectObjectScript.Instance.brightness, 0.3f);
    }

    public void TileUnhighlighted()
    {
        Debug.Log("unhighlighted tile at: " + tileCoords);
        GetComponent<Renderer>().material.DOColor(_bottomMaterialColor, 0.3f);
        _tileModels[(int)tileType].GetComponentInChildren<Renderer>().material.DOColor(_topMaterialColor, 0.3f);
    }

    void FlipTile()
    {
        Debug.Log("Flip This Tile");
        transform.DOJump(transform.position, 0.2f, 1, 0.3f);
        flipFeedback?.PlayFeedbacks();
        tileState = TileState.AlreadyFlipped; 
        
    }
}
