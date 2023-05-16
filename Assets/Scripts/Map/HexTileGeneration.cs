using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class HexTileGeneration : MonoBehaviour
{
    [PropertySpace(SpaceBefore = 15, SpaceAfter = 10)]
    [Title("Map Profile Data Settings")]
    public MapProfileSO mapProfile;
    public bool GenerateLevelOnStart; 
    [InfoBox("WARNING: [SaveMap] will override Map Profile!!")]
    [PropertyOrder(1)]
    [Header("Map Settings")]
    [ReadOnly] public Vector2 mapSize = new Vector2 (10, 20);
    [Space]
    [PropertyOrder(2)]
    [Header("OFFSETS FOR TILE PLACEMENT")]
    [SerializeField] float _tileXOffset;
    [PropertyOrder(2)]
    [SerializeField] float _tileZOffset;
    [Space]
    [PropertyOrder(3)]
    [SerializeField] GameObject hexagonPrefab;

    [PropertyOrder(4)]
    public List<TileInfo> tiles = new List<TileInfo>();

    [PropertyOrder(-1)]
    [VerticalGroup("Split/right")]
    [Button(ButtonSizes.Large), GUIColor(0, 1, 0)]
    private void CreateMap()
    {
        ClearMap();
        CreateHexTileMap();

    }
    [PropertyOrder(-1)]
    [HorizontalGroup("Split", 0.5f)]
    [Button(ButtonSizes.Large), GUIColor(1,0,0)]
    private void ClearMap()
    {
        List<GameObject> tileList = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            tileList.Add(transform.GetChild(i).gameObject);
        }

        foreach (GameObject tile in tileList)
        {
            DestroyImmediate(tile);
        }
        tiles.Clear();


    }
    public void CreateHexTileMap()
    {
        tiles.Clear();
        //for each number on the X Axis (width) 
        for (int x = 0; x <= mapSize.x; x++)
        {
            //for each number on the Z Axis (height) 
            for (int z = 0; z <= mapSize.y; z++)
            {
                if(mapProfile.ThemedHexagonPrefab != null)
                {
                    hexagonPrefab = mapProfile.ThemedHexagonPrefab;
                }
                
                GameObject _tile = Instantiate(hexagonPrefab);

                TileInfo _info = _tile.GetComponent<TileInfo>();
                EditorCoordinateTileDebugScript _debug = _tile.GetComponent<EditorCoordinateTileDebugScript>();

                _info.tileCoords.x = x;
                _info.tileCoords.y = z;

                _debug.DisplayCoords(x,z); 

                if (z % 2 == 0)
                {
                    //If even number then offset in a line
                    _tile.transform.position = new Vector3(transform.position.x + x * _tileXOffset, transform.position.y, transform.position.z + z * _tileZOffset);
                }
                else
                {
                    // if odd number then offset half the width to slot in the middle
                    _tile.transform.position = new Vector3(transform.position.x + x * _tileXOffset + _tileXOffset / 2, transform.position.y, transform.position.z + z * _tileZOffset);
                }
                
                SetTileParent(_tile, x, z);

            }
        }
    }

    // Set the tile parent to make clean 
    void SetTileParent(GameObject _tile, int x, int z)
    {
        _tile.transform.parent = transform;
        _tile.name = "HexTile: " + x.ToString() + ", " + z.ToString();
        _tile.GetComponent<TileInfo>().SetTileMapData();
        tiles.Add(_tile.GetComponent<TileInfo>()); 

    }
    [PropertyOrder(0)]
    [VerticalGroup("Split1/right")]
    [Button(ButtonSizes.Large), GUIColor(1,1,1)]
    public void SaveMap()
    {
        if (tiles.Count < 1)
        {
            Debug.LogWarning("Please [Create Map] before saving map Data");
        }
        else
        {
            mapProfile.mapData.Clear();

            TileInfo[] tilesToSave = FindObjectsOfType<TileInfo>();

            foreach (TileInfo tile in tilesToSave)
            {
                tile.SetTileMapData();
                mapProfile.mapData.Add(tile.mapData);
            }
            Debug.LogWarning("Map Profile saved to: " + mapProfile.name);
        }
    }
    [PropertyOrder(0)]
    [HorizontalGroup("Split1", 0.5f)]
    [Button(ButtonSizes.Large), GUIColor(1, 1, 1)]
    public void LoadMap()
    {
        if(tiles.Count < 1)
        {
            Debug.LogWarning("Please [Create Map] before loading map Data"); 
        }
        else
        {
            for (int i = tiles.Count - 1; i >= 0; i--)
            {
                bool matchFound = false;

                for (int j = 0; j < mapProfile.mapData.Count; j++)
                {
                    TileMapData mapTileCoords = mapProfile.mapData[j];

                    if (tiles[i].tileCoords == mapTileCoords.tileCoords)
                    {
                        matchFound = true;

                        tiles[i].tileType = mapTileCoords.tileType;
                        tiles[i].tileState = mapTileCoords.tileState;
                        tiles[i].RefreshTileInfo();

                        break;
                    }
                }

                if (!matchFound)
                {
                    Debug.Log("No match found for TileCoords: " + tiles[i].tileCoords);
                    DestroyImmediate(tiles[i].gameObject);
                    tiles.RemoveAt(i);
                }
            }
        }
    }

    private void Start()
    {
        if(GenerateLevelOnStart)
        {
            CreateMap();
            LoadMap();
        }
    }
}
