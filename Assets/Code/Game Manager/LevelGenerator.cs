using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LevelGenerator : MonoBehaviour
{
    private Room[,] grid; //List of all rooms in level
    public List<Room> roomPrefabs;
    public int numRows;
    public int numCols;
    public int tileWidth;
    public int tileHeight;
    public int startRow;
    public int startCol;
    public int startRow2;
    public int startCol2;
    private GameManager gameManager;
    public int keyHolder;
    public Transform keyLocation;
    private Attributes attributes; 

    public enum levelRandomType { Random, Seeded, MapOfDay, Gauntlet }
    public int levelSeed;
    public levelRandomType randomType;

    void Start()
    {
        attributes = Attributes.attributesInstance.GetComponent<Attributes>();
    }

    public void SeedRandomizer()
    {
        //Switch RNG type
        switch (randomType)
        {
            case levelRandomType.Random:
                Random.InitState((int)System.DateTime.Now.Ticks);
                break;
            case levelRandomType.Seeded:
                Random.InitState(levelSeed);
                break;
            case levelRandomType.MapOfDay:
                Random.InitState((int)System.DateTime.Today.Ticks);
                break;
            case levelRandomType.Gauntlet:
                Random.InitState((int)System.DateTime.Now.Ticks);
                break;
        }
    }

    public async Task GenerateLevel()
    {
        gameManager = GetComponent<GameManager>();
        //Create our level
        //Seed the generator
        SeedRandomizer();
        if (randomType == levelRandomType.Gauntlet)
        {
            numRows = 2 + Mathf.FloorToInt((gameManager.gauntletLevel / 2) - (attributes.levelAmp));
            numCols = 2 + Mathf.FloorToInt((gameManager.gauntletLevel / 2) - (attributes.levelAmp));
            if (numRows > 7)
            {
                numRows = 8;
                numCols = 8;
            }
            if (numRows < 2)
            {
                numRows = 2;
                numCols = 2;
            }
        }
        //Make array of rooms correct size based on number of rows/cols
        grid = new Room[numCols, numRows];
        startRow = Random.Range(0, numRows);
        startCol = Random.Range(0, numCols);
        //If multiplayer, set spawn zone for player 2
        if (gameManager.isSplitScreen == true)
        {
            startRow2 = Random.Range(0, numRows);
            startCol2 = Random.Range(0, numCols);
            //If they spawn on the same tile failsafe
            if (startRow2 == startRow && startCol2 == startCol)
            {
                startRow2 += 1;
                if (startRow2 >= numRows)
                {
                    startRow2 -= 2;
                }
            }
        }
        if (gameManager.isSplitScreen == false)
        {
            startRow2 = -1;
            startCol2 = -1;
        }

        //For every row
        for (int currentRow = 0; currentRow < numRows; currentRow += 1)
        {
            //Look at every col in row
            for (int currentCol = 0; currentCol < numCols; currentCol += 1)
            {
                //Instantiate a room
                if (currentRow == startRow && currentCol == startCol)
                {
                    GameObject startRoom = Instantiate(GetStartRoomPrefab().gameObject, Vector3.zero, Quaternion.identity) as GameObject;

                    //Move into position
                    float xPosition = currentCol * tileWidth;
                    float zPosition = currentRow * tileHeight;
                    startRoom.transform.position = new Vector3(xPosition, -1, -zPosition);
                    grid[currentCol, currentRow] = startRoom.GetComponent<Room>();

                    GameObject levelParent = GameObject.Find("LevelParent");
                    Transform levelParentTrans = levelParent.transform;
                    Transform tempRoomTrans = startRoom.transform;
                    tempRoomTrans.parent = levelParentTrans;

                    Generate(startRoom, currentRow, currentCol);
                }
                else if (currentRow == startRow2 && currentCol == startCol2)
                {
                    GameObject startRoom = Instantiate(GetStartRoomPrefab().gameObject, Vector3.zero, Quaternion.identity) as GameObject;

                    //Move into position
                    float xPosition = currentCol * tileWidth;
                    float zPosition = currentRow * tileHeight;
                    startRoom.transform.position = new Vector3(xPosition, -1, -zPosition);
                    grid[currentCol, currentRow] = startRoom.GetComponent<Room>();

                    GameObject levelParent = GameObject.Find("LevelParent");
                    Transform levelParentTrans = levelParent.transform;
                    Transform tempRoomTrans = startRoom.transform;
                    tempRoomTrans.parent = levelParentTrans;

                    Generate(startRoom, currentRow, currentCol);

                }
                else
                {
                    GameObject tempRoom = Instantiate(GetRandomRoomPrefab().gameObject, Vector3.zero, Quaternion.identity) as GameObject;

                    //Move into position
                    float xPosition = currentCol * tileWidth;
                    float zPosition = currentRow * tileHeight;
                    tempRoom.transform.position = new Vector3(xPosition, -1, -zPosition);

                    GameObject levelParent = GameObject.Find("LevelParent");
                    Transform levelParentTrans = levelParent.transform;
                    Transform tempRoomTrans = tempRoom.transform;
                    tempRoomTrans.parent = levelParentTrans;

                    Generate(tempRoom, currentRow, currentCol);
                }
            }
        }
        Invoke("PlaceKey", 1f);
    }

    public GameObject GetRandomRoomPrefab()
    {
        //return a random room from the list
        Room randomRoom = roomPrefabs[Random.Range(1, roomPrefabs.Count)];
        return randomRoom.gameObject;
    }
    public GameObject GetStartRoomPrefab()
    {
        //return a random room from the list
        Room randomRoom = roomPrefabs[0];
        return randomRoom.gameObject;
    }

    public virtual void Generate(GameObject LevelPiece, int currentRow, int currentCol)
    {
        //Save to the grid
        grid[currentCol, currentRow] = LevelPiece.GetComponent<Room>();

        //If bottom row, open North
        if (currentRow == 0)
        {
            grid[currentCol, currentRow].doorSouth.SetActive(false);
        }
        //If top, open South
        else if (currentRow == numRows - 1)
        {
            grid[currentCol, currentRow].doorNorth.SetActive(false);
        }
        //Otherwise open both
        else
        {
            grid[currentCol, currentRow].doorNorth.SetActive(false);
            grid[currentCol, currentRow].doorSouth.SetActive(false);
        }

        //If bottom row, open East
        if (currentCol == 0)
        {
            grid[currentCol, currentRow].doorEast.SetActive(false);
        }
        //If top, open West
        else if (currentCol == numCols - 1)
        {
            grid[currentCol, currentRow].doorWest.SetActive(false);
        }
        //Otherwise open both
        else
        {
            grid[currentCol, currentRow].doorEast.SetActive(false);
            grid[currentCol, currentRow].doorWest.SetActive(false);
        }
    }

    public void PlaceKey()
    {
        //Select an AI to hold the key
        if (randomType == levelRandomType.Gauntlet)
        {
            keyHolder = Random.Range(0, gameManager.AI.Count);
            gameManager.AI[keyHolder].hasKey = true;
            //Get location of key holder for the tracker
            keyLocation = gameManager.AI[keyHolder].GetComponent<Transform>();
        }
        else
        {
            Debug.Log("KEY IGNORED");
        }
    }

}
