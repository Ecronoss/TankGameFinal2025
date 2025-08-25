using UnityEngine;

public class Attributes : MonoBehaviour
{
    public static Attributes attributesInstance;
    public Transform levelParent;
    private LevelGenerator levelGenerator;
    public GameManager gameManager;
    [Header("Player Components")]
    public GameObject player1;
    public GameObject player2;
    public PlayerController player1Controller;
    public PlayerController player2Controller;
    public Pawn pawn1;
    public Pawn pawn2;
    public Health player1Health;
    public Health player2Health;
    [Header("Shared stats")]
    public float totalTime;
    public float timeAmp;
    public float levelAmp;
    [Header("Player 1 Stat Block")]
    public float P1HealthAmp = 0;
    public float P1CurrentHP;
    public float P1AttackSpeedAmp;
    public float P1SpeedAmp;
    public float P1DamageAmp;
    public float P1PowerupAmp;
    public float P1Score;
    public float P1ArrowRounds;
    public float P1ShotGunRounds;
    [Header("Player 2 Stat Block")]
    public float P2HealthAmp = 0;
    public float P2CurrentHP;
    public float P2AttackSpeedAmp;
    public float P2SpeedAmp;
    public float P2DamageAmp;
    public float P2PowerupAmp;
    public float P2Score;
    public float P2ArrowRounds;
    public float P2ShotGunRounds;

    private void Awake()
    {
        if (attributesInstance == null)
        {
            // Set instance of GM to our static variable
            attributesInstance = this;

            // Prevent object destruction on scene load
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.gameInstance.GetComponent<GameManager>();
        levelGenerator = GameManager.gameInstance.GetComponent<LevelGenerator>();
        GameObject levelParent = GameObject.Find("LevelParent");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Check()
    {
        Invoke("StatBlocker", 0.01f);
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        player1Controller = player1.GetComponent<PlayerController>();
        pawn1 = player1Controller.pawn;
        player1Health = pawn1.healthComp;
        if (player2 != null)
        {
            player2Controller = player2.GetComponent<PlayerController>();
            pawn2 = player2Controller.pawn;
            player2Health = pawn2.healthComp;
            Destroy(player2Controller);
        }
        Destroy(player1Controller);
    }

    public void StatBlocker()
    {
        totalTime = gameManager.remainingTime;
        P1Score = player1Controller.score;
        P1CurrentHP = player1Health.currentHP;
        if (player2 != null)
        {
            P2Score = player2Controller.score;
            P2CurrentHP = player2Health.currentHP;
        }
        else;
        Invoke("KillTank", 0.1f);
        if (P1ArrowRounds > 0)
        {
            P1ArrowRounds -= 1;
        }
        else;
        if (P2ArrowRounds > 0)
        {
            P2ArrowRounds -= 1;
        }
        else;
        if (P1ShotGunRounds > 0)
        {
            P1ShotGunRounds -= 1;
        }
        else;
        if (P2ShotGunRounds > 0)
        {
            P2ShotGunRounds -= 1;
        }
        else;
    }

    public void KillTank()
    {
        GameObject pawnObject = pawn1.GetComponent<GameObject>();
        Destroy(pawnObject);
        if (player2 != null)
        {
            pawnObject = pawn2.GetComponent<GameObject>();
            Destroy(pawnObject);
        }
    }
}
