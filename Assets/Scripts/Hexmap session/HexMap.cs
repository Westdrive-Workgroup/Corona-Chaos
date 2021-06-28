using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    public GameObject hexTilePrefab;
    public List<Hex> map;
    [Header("Hext tile properties")]
    public bool edgeUp = false;
    public float tileRadius = 1f;

    [Header("Grid size")]
    public int width;
    public int height;

    [Header("Materials")]
    public Material[] tileMaterials;

    [Header("Debug")] public bool showDebugCoord = true;
    //public float zOffset = 1.74f;

    //public float xOffset = 1.54f;
    // Start is called before the first frame update
    void Start()
    {
        
        for (int column = 0; column < width; column++)
        {
            for (int row = 0; row < height; row++)
            {
                /*float zPos = j;
                
                if (i % 2 == 1)
                {
                    zPos = (zPos*zOffset) + (zOffset / 2);
                    
                    GameObject hexTile = Instantiate(hexTilePrefab, new Vector3(i * xOffset, 0, zPos ), Quaternion.identity);
                    hexTile.name = "HexTile_" + i + "_" + j;
                    hexTile.transform.SetParent(this.transform);
                }
                else
                {
                    GameObject hexTile = Instantiate(hexTilePrefab, new Vector3(i * xOffset, 0, zPos * zOffset), Quaternion.identity);
                    hexTile.name = "HexTile_" + i + "_" + j;
                    hexTile.transform.SetParent(this.transform);
                }*/
                Hex tile = new Hex(column, row, tileRadius,edgeUp);
                if (edgeUp)
                {
                    GameObject hexTile = Instantiate(hexTilePrefab, tile.GetPosition(), Quaternion.identity,this.transform);
                    hexTile.name = "HexTile_" + column + "_" + row;
                    hexTile.GetComponent<MeshRenderer>().material =
                        tileMaterials[Random.Range(0, tileMaterials.Length)];
                    hexTile.GetComponent<TileDebug>().showDebugCoord = showDebugCoord;
                    hexTile.GetComponentInChildren<TextMesh>().text = string.Format("{0},{1}", column, row);
                }
                else
                {
                    GameObject hexTile = Instantiate(hexTilePrefab, tile.GetPosition(), Quaternion.Euler(0,90,0),this.transform);
                    hexTile.name = "HexTile_" + column + "_" + row;
                    hexTile.GetComponent<MeshRenderer>().material =
                        tileMaterials[Random.Range(0, tileMaterials.Length)];
                    hexTile.GetComponent<TileDebug>().showDebugCoord = showDebugCoord;
                    hexTile.GetComponentInChildren<TextMesh>().text = string.Format("{0},{1}", column, row);
                }
                map.Add(tile);
                
                
                
            }
        }
    }

    void SaveMap()
    {
        
    }

    void LoadMap()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
