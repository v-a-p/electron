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
        if (cornerDetectTimer < 0) //If time to detect corner
        {
            if (CheckIfTileExist() == 1)
            {
                Debug.Log("Left");
            }
            else if (CheckIfTileExist() == 2)
            {
                Debug.Log("Right");
            }
            else if (CheckIfTileExist() == 3)
            {
                Debug.Log("Top");
            }
            else if (CheckIfTileExist() == 4)
            {
                Debug.Log("Bottom");
            }
            else if (CheckIfTileExist() == 5)
            {
                Debug.Log("Bottom left");
            }
            else if (CheckIfTileExist() == 6)
            {
                Debug.Log("Bottom right");
            }
            else if (CheckIfTileExist() == 7)
            {
                Debug.Log("Top left");
            }
            else if (CheckIfTileExist() == 8)
            {
                Debug.Log("Top right");
            }
            else Debug.Log("Found tile");

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
    public int CheckIfTileExist()
    {
        PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); //Create an instance of object that has "Player" tag

        float laserLength = 10f;

        //== Bottom left =========================================================================================================
        Vector2 positionBL = new Vector2(player.downLeftX, player.downLeftY); //Get coordinates of bottom left corner
        RaycastHit2D hasObject_downLeft = Physics2D.Raycast(positionBL, Vector2.down, laserLength);

        //== Bottom right =========================================================================================================
        Vector2 positionBR = new Vector2(player.downRightX, player.downRightY); //Get coordinates of bottom right corner
        RaycastHit2D hasObject_downRight = Physics2D.Raycast(positionBR, Vector2.down, laserLength);

        //== Top left =========================================================================================================
        Vector2 positionTL = new Vector2(player.topLeftX, player.topLeftY); //Get coordinates of top left corner
        RaycastHit2D hasObject_topLeft = Physics2D.Raycast(positionTL, Vector2.up, laserLength);

        //== Top right =========================================================================================================
        Vector2 positionTR = new Vector2(player.topRightX, player.topRightY); //Get coordinates of top right corner
        RaycastHit2D hasObject_topRight = Physics2D.Raycast(positionTR, Vector2.up, laserLength);

        if (hasObject_downLeft.collider == null && hasObject_topLeft.collider == null && hasObject_downRight.collider != null && hasObject_topRight.collider != null) return 1; //Left
        else if (hasObject_downLeft.collider != null && hasObject_topLeft.collider != null && hasObject_downRight.collider == null && hasObject_topRight.collider == null) return 2; //Right
        else if (hasObject_downLeft.collider != null && hasObject_topLeft.collider == null && hasObject_downRight.collider != null && hasObject_topRight.collider == null) return 3; //Up
        else if (hasObject_downLeft.collider == null && hasObject_topLeft.collider != null && hasObject_downRight.collider == null && hasObject_topRight.collider != null) return 4; //Down
        else if (hasObject_downLeft.collider == null && hasObject_topLeft.collider == null && hasObject_downRight.collider == null && hasObject_topRight.collider != null) return 5; //Bottom left
        else if (hasObject_downLeft.collider == null && hasObject_topLeft.collider != null && hasObject_downRight.collider == null && hasObject_topRight.collider == null) return 6; //Bottom right
        else if (hasObject_downLeft.collider == null && hasObject_topLeft.collider == null && hasObject_downRight.collider != null && hasObject_topRight.collider == null) return 7; //Top left
        else if (hasObject_downLeft.collider != null && hasObject_topLeft.collider == null && hasObject_downRight.collider == null && hasObject_topRight.collider == null) return 8; //Top right
        else return -1;
    }
}