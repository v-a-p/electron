using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InfinitelyGenerate : MonoBehaviour
{
    public GameObject[] tilePrefabs; //An array that stores objects/maps that should be generated in procudual generation
    private float cornerDetectTime = 3f; //Detect corner every x second
    private float cornerDetectTimer;

    // Start is called before the first frame update
    void Start()
    {
        cornerDetectTimer = cornerDetectTime; //Set timer

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
        cornerDetectTimer -= Time.deltaTime;
        if(cornerDetectTimer < 0) //If time to detect corner
        {
            if (CheckIfTileExist()) //Check if there is tile on the top right corner, and print out information in console
            {
                Debug.Log("Found Tile");
            }
            else
            {
                Debug.Log("No Tile");

                int rand = Random.Range(0, 9); //Randomly generate an integer between 0 and 9

                //Corner 10%, Red 20%, Rock 40%, Water 30%
                if (rand == 0) TileSpawnArea(0);
                else if (rand == 1 || rand == 2) TileSpawnArea(1);
                else if (rand > 2 && rand < 7) TileSpawnArea(2);
                else if (rand > 6 && rand < 10) TileSpawnArea(3);


            }
            cornerDetectTimer = cornerDetectTime; //Reset timer
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
        PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); //Create an instance of object that has "Player" tag

        float laserLength = 0.000001f;
        Vector2 position = new Vector2(player.CornerX, player.CornerY); //Get coordinates of top right corner
        RaycastHit2D hasObject = Physics2D.Raycast(position, Vector2.right, laserLength);

        bool existTile;
        if (hasObject.collider == null) existTile = false;
        else existTile = true;

        return existTile;
    }
}