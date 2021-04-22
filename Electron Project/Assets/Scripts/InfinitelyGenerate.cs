using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitelyGenerate : MonoBehaviour
{
    public GameObject[] tilePrefabs; //An array that stores objects/maps that should be generated in procudual generation
    private float cornerDetectTime = 3f; //Detect corner every x second
    private float cornerDetectTimer;

    //== Horizontal ==============================================================
    public float tileWidth; //Width of the tile, or distance between two objects
    private float adjustSpace = 0.17f; //Adjust 3D axis to 2D axis

    public float xSpawnHorizontal_right; //Spawn position (right is positive)
    public float ySpawnHorizontal_right; //Spawn position (down is positive)

    public float xSpawnHorizontal_left;
    public float ySpawnHorizontal_left;

    //== Vertical ================================================================
    public float tileHeight; //Height of the tile, or distance between two objects
    public float tileHeight_down;

    public float ySpawnVertical_up; //Spawn position (top is positive)
    public float ySpawnVertical_down;

    // Start is called before the first frame update
    void Start()
    {
        cornerDetectTimer = cornerDetectTime; //Set timer

        //Instantiate first map
        Instantiate(tilePrefabs[4], transform.position * xSpawnHorizontal_right + Vector3.forward * xSpawnHorizontal_right * 2 + Vector3.up * 1.79f, transform.rotation);
        xSpawnHorizontal_right += tileWidth;
        xSpawnHorizontal_left -= tileWidth;
        ySpawnVertical_up += tileHeight;
        ySpawnVertical_down += tileHeight;
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
                TileSpawnHorizontal_Left(4);
            }
            else if (CheckIfTileExist() == 2)
            {
                Debug.Log("Right");
                TileSpawnHorizontal_Right(4);
            }
            else if (CheckIfTileExist() == 3)
            {
                Debug.Log("Top");
                TileSpawnVertical_Up(4);
            }
            else if (CheckIfTileExist() == 4)
            {
                Debug.Log("Bottom");
                TileSpawnVertical_Down(4);
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

    public void TileSpawnHorizontal_Right(int index)
    {
        Instantiate(tilePrefabs[index], transform.position * xSpawnHorizontal_right + Vector3.forward * xSpawnHorizontal_right * 2 + Vector3.down * ySpawnHorizontal_right, transform.rotation); //Clone object
        xSpawnHorizontal_right += tileWidth; //Set the position of next clone (x-axis)
        ySpawnHorizontal_right += adjustSpace * tileWidth; //Adjust 3D axis to 2D axis
    }

    public void TileSpawnHorizontal_Left(int index)
    {
        Instantiate(tilePrefabs[index], transform.position * xSpawnHorizontal_left + Vector3.forward * xSpawnHorizontal_left * 2 + Vector3.up * ySpawnHorizontal_left, transform.rotation); //Clone object
        xSpawnHorizontal_left -= tileWidth; //Set the position of next clone (x-axis)
        ySpawnHorizontal_left += adjustSpace * tileWidth; //Adjust 3D axis to 2D axis
    }

    public void TileSpawnVertical_Up(int index)
    {
        Instantiate(tilePrefabs[index], transform.up * ySpawnVertical_up + Vector3.forward * ySpawnVertical_up * 2, transform.rotation); //Clone object
        ySpawnVertical_up += tileHeight; //Set the position of next clone (y-axis)
    }

    public void TileSpawnVertical_Down(int index)
    {
        Instantiate(tilePrefabs[index], transform.up * -ySpawnVertical_down + Vector3.forward * ySpawnVertical_up * 2, transform.rotation); //Clone object
        ySpawnVertical_down += tileHeight_down; //Set the position of next clone (y-axis)
    }



    //== Area (use Vertical variables) =====================================================
    public float rowSpace; //x-coordinate of the row
    public float rowSpaceAdjust; //Space between two rows

    public void TileSpawnArea(int index)
    {
        float savePosition = ySpawnVertical_up; //Save y-coordinate
        for (int i = 0; i < 2; i++) //Number of column
        {
            for (int j = 0; j < 2; j++)//Number of row
            {
                Instantiate(tilePrefabs[index], transform.up * ySpawnVertical_up + Vector3.forward * ySpawnVertical_up * 2 + Vector3.right * rowSpace, transform.rotation); //Clone object
                ySpawnVertical_up += tileHeight; //Set the position of next clone (y-axis)
            }
            rowSpace += rowSpaceAdjust; //Set the position of the next clone column (x-axis)
            ySpawnVertical_up = savePosition; //Reset y-axis
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