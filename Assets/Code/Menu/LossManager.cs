using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LossManager : MonoBehaviour
{
    public Transform levelParent;
    public TextMeshProUGUI P1Score;
    public TextMeshProUGUI P2Score;
    public TextMeshProUGUI gauntletClear;
    public GameObject player1;
    public GameObject player2;
    private GameObject newLevelParent;
    private GameManager gameManager;
    private LevelGenerator levelGenerator;
    private Attributes attributes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.gameInstance.GetComponent<GameManager>();
        levelGenerator = GameManager.gameInstance.GetComponent<LevelGenerator>();
        attributes = Attributes.attributesInstance.GetComponent<Attributes>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnEnable()
    {
        Invoke("EndStats", 0.01f);

    }

    public void ReloadScene()
    {
        levelGenerator.numRows = 3;
        levelGenerator.numCols = 3;
        GameManager.gameInstance.ActivateMainMenu();
        LevelReset();
    }

    public void LevelReset()
    {
        GameManager.gameInstance.ResetGame();
        int nbChildren = levelParent.childCount;
        {
            for (int i = nbChildren - 1; i >= 0; i--)
            {
                DestroyImmediate(levelParent.GetChild(i).gameObject);
            }
        }
    }

    public void EndStats()
    {
        //Find player objects and display scores
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        //reset level setting for gauntlet mode
        LevelText();
        PlayerController player1Score = player1.GetComponent<PlayerController>();
        P1Score.text = $"PLAYER 1 SCORE:\n {player1Score.score}";
        if (player2 != null)
        {
            PlayerController player2Score = player2.GetComponent<PlayerController>();
            P2Score.text = $"PLAYER 2 SCORE:\n {player2Score.score}";
        }
        AttributeReset();
    }

    public void LevelText()
    {
        if (gameManager.gauntletLevel <= 2)
        {
            gauntletClear.text = " ";
        }
        if (gameManager.gauntletLevel > 2)
        {
            gauntletClear.text = $"LEVELS CLEARED : {gameManager.gauntletLevel - 2}";
        }
    }

    public void AttributeReset()
    {
        attributes.totalTime = 0;
        attributes.timeAmp = 0;
        attributes.levelAmp = 0;

        attributes.P1HealthAmp = 0;
        attributes.P1CurrentHP = 0;
        attributes.P1AttackSpeedAmp = 0;
        attributes.P1SpeedAmp = 0;
        attributes.P1DamageAmp = 0;
        attributes.P1PowerupAmp = 0;
        attributes.P1Score = 0;
        attributes.P1ArrowRounds = 0;
        attributes.P1ShotGunRounds = 0;

        attributes.P2HealthAmp = 0;
        attributes.P2CurrentHP = 0;
        attributes.P2AttackSpeedAmp = 0;
        attributes.P2SpeedAmp = 0;
        attributes.P2DamageAmp = 0;
        attributes.P2PowerupAmp = 0;
        attributes.P2Score = 0;
        attributes.P2ArrowRounds = 0;
        attributes.P2ShotGunRounds = 0;
    }
}
