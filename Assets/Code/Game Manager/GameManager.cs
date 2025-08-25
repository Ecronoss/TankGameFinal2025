using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager gameInstance;
    [Header("Lists")]
    public List<PlayerController> players;
    public List<TankPawn> pawns;
    public List<AIController> AI;
    public GameObject menuCamera;

    [Header("Prefabs")]
    public GameObject playerPawnPrefab;
    public GameObject playerControllerPrefab;

    [Header("Helper Objects")]
    public LevelGenerator levelGenerator;
    public float spawnX;
    public float spawnZ;
    public float spawnY;
    public Vector3 playerSpawn;
    public GameObject playerPawn;
    public float remainingTime;
    public float timeBonus = 90;

    [Header("Gameplay State Objects")]
    public GameObject pressStartObject;
    public GameObject mainMenuObject;
    public GameObject playGameObject;
    public GameObject gameOverWinObject;
    public GameObject gameOverLossObject;
    public GameObject gameOptionsObject;
    public GameObject creditsObject;
    private OptionsManager settings;
    public int timeClock = 300;
    public int gauntletLevel = 0;
    public GameObject optionCanvas;
    public OptionsManager option;

    [Header("Gameplay Settings")]
    public bool isSplitScreen = false;
    


    private void Awake()
    {
        GameObject optionCanvas = GameObject.Find("OptionsCanvas");
        OptionsManager option = optionCanvas.GetComponent<OptionsManager>();
        if (gameInstance == null)
        {
            // Set instance of GM to our static variable
            gameInstance = this;

            // Prevent object destruction on scene load
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Prevents multiple GMs from existing
            Destroy(gameObject);
        }



    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DeactivateAllStates();
        ChangeGameState(pressStartObject);
    }

    void StartGameplay()
    {
        //Level Generation
        levelGenerator.GenerateLevel();
        //Player Spawn
        //playerSpawn = GameObject.Find("PlayerSpawnPoint").GetComponent<Vector3> ();
        spawnZ = levelGenerator.startRow * -50;
        spawnX = levelGenerator.startCol * 50;
        spawnY = 0;
        playerSpawn = new(spawnX, spawnY, spawnZ);
        SpawnStart(playerSpawn);
    }

    public void ChangeGameState(GameObject gameplayObject)
    {
        //Deactivate all states
        DeactivateAllStates();

        //Activate the new state to move into
        gameplayObject.SetActive(true);

    }

    private void DeactivateAllStates()
    {
        pressStartObject.SetActive(false);
        mainMenuObject.SetActive(false);
        playGameObject.SetActive(false);
        gameOverWinObject.SetActive(false);
        gameOverLossObject.SetActive(false);
        gameOptionsObject.SetActive(false);
        creditsObject.SetActive(false);
    }

    public void ActivateMainMenu()
    {
        //Change to main menu
        ChangeGameState(mainMenuObject);
        menuCamera.SetActive(true);
        gauntletLevel = 1;
        
    }

    public void ActivateGameplay()
    {
        ChangeGameState(playGameObject);
        //Anything needed to do
        //Level Generation
        levelGenerator.GenerateLevel();
        //Player Spawn
        //playerSpawn = GameObject.Find("PlayerSpawnPoint").GetComponent<Vector3> ();
        spawnZ = levelGenerator.startRow * -50;
        spawnX = levelGenerator.startCol * 50;
        spawnY = 0;
        playerSpawn = new(spawnX, spawnY, spawnZ);
        SpawnStart(playerSpawn);
        //Create default camera
        Rect viewport = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        players[0].playerCamera.rect = viewport;
        levelGenerator.keyHolder = Random.Range(0, AI.Count);

        if (isSplitScreen == true)
        {
            spawnZ = levelGenerator.startRow2 * -50;
            spawnX = levelGenerator.startCol2 * 50;
            spawnY = 0;
            playerSpawn = new(spawnX, spawnY, spawnZ);
            SpawnStart(playerSpawn);
            //Create splitscreen camera
            Rect viewport2 = new Rect(0.0f, 0.0f, 1.0f, 0.5f);
            players[0].playerCamera.rect = viewport2;
            viewport2 = new Rect(0.0f, 0.5f, 1.0f, 0.5f);
            players[1].playerCamera.rect = viewport2;
            //AudioListener listener = players[1].playerCamera.GetComponent<AudioListener>();
            //Remove P2 Timer
            Pawn timerObject = players[1].pawn;
            Timer timer = timerObject.GetComponent<Timer>();
            timer.enabled = false;
            //listener.enabled = false;
        }
        
    }

    public void ActivateOptions()
    {
        //Activate object in scene
        ChangeGameState(gameOptionsObject);
        //Anything needed to do
    }

    public void ActivateCredits()
    {
        //Activate object in scene
        ChangeGameState(creditsObject);
        //Anything needed to do
    }

    public void ActivateVictory()
    {
        //Activate object in scene
        ChangeGameState(gameOverWinObject);
        //Anything needed to do
    }

    public void ActivateLoss()
    {
        //Activate object in scene
        ChangeGameState(gameOverLossObject);
        //Anything needed to do
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnStart(Vector3 spawnPosition)
    {
        GameObject tempPlayerControllerObject = Instantiate<GameObject>(playerControllerPrefab); //Create player object and copy of controller using prefab
        tempPlayerControllerObject.transform.position = Vector3.zero; //Move to 0,0,0
        PlayerController tempPlayerController = tempPlayerControllerObject.GetComponent<PlayerController>(); //Get component for controller
        //players.Add(tempPlayerController);

        GameObject tempPlayerPawnObject = Instantiate<GameObject>(playerPawnPrefab); //Create Tank Pawn object and copy of Tank using prefab
        TankPawn tempPlayerPawn = tempPlayerPawnObject.GetComponent<TankPawn>(); //Get component for Tank Pawn
        tempPlayerPawnObject.transform.position = spawnPosition; //Creates tank at Spawn Point
        //pawns.Add(tempPlayerPawn);

        tempPlayerController.pawn = tempPlayerPawn; // Connect pawn to controller
        tempPlayerController.Possess(tempPlayerPawn);
        tempPlayerPawn.controller = tempPlayerController;
        tempPlayerPawn.score1.text = $"Score {0}";

        if (isSplitScreen == true)
        {
            tempPlayerPawn.score2.text = $"Score {0}";
        }

        menuCamera.SetActive(false);
    }

    public void ResetGame()
    {
        GameObject player1 = GameObject.Find("Player 1");
        GameObject player2 = GameObject.Find("Player 2");
        if (player1 != null)
        {
            Destroy(player1);
        }
        if (player2 != null)
        {
            Destroy(player2);
        }
        players.Clear();
        foreach (Pawn objectPawn in pawns)
        {
            if (objectPawn != null)
            {
                Destroy(objectPawn.gameObject);
            }
        }
        pawns.Clear();
        AI.Clear();
    }
}
