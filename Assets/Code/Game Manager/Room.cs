using UnityEngine;

public class Room : MonoBehaviour
{
    // Stores walls / doors for easy access
    public GameObject doorNorth;
    public GameObject doorSouth;
    public GameObject doorEast;
    public GameObject doorWest;

    //Store waypoints to room if applicable
    //Store any additional info about room
    public int difficultyLvl;
}
