using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InfinitelyGenerate : MonoBehaviour
{
    public GameObject[] tilePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < 5; i++)
        //{
        //TileSpawnHorizontal(0);
        //TileSpawnHorizontal(1);
        //TileSpawnVertical(0);
        //TileSpawnVertical(1);
        //}
    }

    // Update is called once per frame
    private void Update()
    {
        if (CheckIfTileExist())
        {
            Debug.Log("Found Tile");
        }
        else
        {
            TileSpawnArea(1);
        }
    }

    //== Horizontal ==============================================================
    public float xSpawnHorizontal; //Spawn position (right is positive)
    public float tileWidth;
    public float ySpawnHorizontal; //Spawn position (down is positive)
    private float adjustSpace = 0.17f;

    public void TileSpawnHorizontal(int index)
    {
        Instantiate(tilePrefabs[index], transform.position * xSpawnHorizontal + Vector3.forward * xSpawnHorizontal * 2 + Vector3.down * ySpawnHorizontal, transform.rotation);
        xSpawnHorizontal += tileWidth;
        ySpawnHorizontal += adjustSpace * tileWidth;
    }

    //== Vertical ================================================================
    public float ySpawnVertical;
    public float tileHeight;

    public void TileSpawnVertical(int index)
    {
        Instantiate(tilePrefabs[index], transform.up * ySpawnVertical + Vector3.forward * ySpawnVertical * 2, transform.rotation);
        ySpawnVertical += tileHeight;
    }

    //== Area (use Vertical variables) =====================================================
    public float rowSpace;
    public float rowSpaceAdjust;

    public void TileSpawnArea(int index) 
    {
        float savePosition = ySpawnVertical; //Save y-coordinate
        for (int i = 0; i < 10; i++) //Number of column
        {
            for (int j = 0; j < 10; j++)//Number of row
            {
                Instantiate(tilePrefabs[index], transform.up * ySpawnVertical + Vector3.forward * ySpawnVertical * 2 + Vector3.right * rowSpace, transform.rotation);
                ySpawnVertical += tileHeight;
            }
            rowSpace += rowSpaceAdjust;
            ySpawnVertical = savePosition;
        }
    }

    //== Check if corner has tile, if not, generate ==========================================
    public bool CheckIfTileExist()
    {
        Tilemap tilemap = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Tilemap>();
        PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        float playerX = player.PlayerY;

        Vector2 position = new Vector2(player.PlayerX + player.CornerX, player.PlayerY + player.CornerY);
        Vector3 positionConvert = position;
        Vector3Int positionConvertInt = Vector3Int.FloorToInt(positionConvert);
        bool existTile = tilemap.HasTile(positionConvertInt);
        return existTile;
    }
}
