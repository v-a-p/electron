using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InfinitelyGenerate : MonoBehaviour
{
    public GameObject[] tilePrefabs; //An array that stores objects/maps that should be generated in procudual generation

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
    private void FixedUpdate()
    {
        if (CheckIfTileExist()) //Check if there is tile on the top right corner, and print out information in console
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
    public float tileWidth; //Width of the tile, or distance between two objects
    public float ySpawnHorizontal; //Spawn position (down is positive)
    private float adjustSpace = 0.17f; //Adjust 3D axis to 2D axis

    public void TileSpawnHorizontal(int index)
    {
        Instantiate(tilePrefabs[index], transform.position * xSpawnHorizontal + Vector3.forward * xSpawnHorizontal * 2 + Vector3.down * ySpawnHorizontal, transform.rotation); //Clone object
        xSpawnHorizontal += tileWidth; //Set the position of next clone (x-axis)
        ySpawnHorizontal += adjustSpace * tileWidth; //Adjust 3D axis to 2D axis
    }

    //== Vertical ================================================================
    public float ySpawnVertical; //Spawn position (top is positive)
    public float tileHeight; //Height of the tile, or distance between two objects

    public void TileSpawnVertical(int index)
    {
        Instantiate(tilePrefabs[index], transform.up * ySpawnVertical + Vector3.forward * ySpawnVertical * 2, transform.rotation); //Clone object
        ySpawnVertical += tileHeight; //Set the position of next clone (y-axis)
    }

    //== Area (use Vertical variables) =====================================================
    public float rowSpace; //x-coordinate of the row
    public float rowSpaceAdjust; //Space between two rows

    public void TileSpawnArea(int index)
    {
        float savePosition = ySpawnVertical; //Save y-coordinate
        for (int i = 0; i < 2; i++) //Number of column
        {
            for (int j = 0; j < 2; j++)//Number of row
            {
                Instantiate(tilePrefabs[index], transform.up * ySpawnVertical + Vector3.forward * ySpawnVertical * 2 + Vector3.right * rowSpace, transform.rotation); //Clone object
                ySpawnVertical += tileHeight; //Set the position of next clone (y-axis)
            }
            rowSpace += rowSpaceAdjust; //Set the position of the next clone column (x-axis)
            ySpawnVertical = savePosition; //Reset y-axis
        }
    }

    //== Check if corner has tile, if not, generate ==========================================
    public bool CheckIfTileExist()
    {
        Tilemap tilemap = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Tilemap>(); //Create an instance of object that has "Tilemap" tag
        PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); //Create an instance of object that has "Player" tag

        Vector2 position = new Vector2(player.PlayerX + player.CornerX, player.PlayerY + player.CornerY); //Get coordinates of top right corner
        Vector3 positionConvert = position; //Implicitly convert Vector2 to Vector3
        Vector3Int positionConvertInt = Vector3Int.FloorToInt(positionConvert); //Convert Vector3 to Vector3Int by taking the floor value.
        bool existTile = tilemap.HasTile(positionConvertInt); //Check if there is tile in the position of the top right corner
        return existTile;
    }
}
