using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class VictoryManager : MonoBehaviour
{
    public static VictoryManager victoryInstance;
    public Transform levelParent;
    private LevelGenerator levelGenerator;
    public GameManager gameManager;
    public Attributes attributes;
    [Header("Upgrade Lists")]
    public List<GameObject> UpgradeButtonPrefabs;
    public List<GameObject> P1UpgradeButtons;
    public List<GameObject> P2UpgradeButtons;
    public Canvas canvas;
    public int clickCount;
    [Header("Upgrade Button Transforms")]
    public Transform button1Loc;
    public Transform button2Loc;
    public Transform button3Loc;
    public Transform button4Loc;
    public Transform button5Loc;
    public Transform button6Loc;
    [Header("Upgrade Values")]
    public float healthUpgradeValue;
    public float attackSpeedUpgradeValue;
    public float damageUpgradeValue;
    public float arrowUpgradeValue;
    public float powerupUpgradeValue;
    public float speedUpgradeValue;
    public float timeUpgradeValue;
    public float levelUpgradeValue;
    public float shotGunUpgradeValue;

    void Awake()
    {
        gameManager = GameManager.gameInstance.GetComponent<GameManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelGenerator = GameManager.gameInstance.GetComponent<LevelGenerator>();
        GameObject levelParent = GameObject.Find("LevelParent");
        attributes = Attributes.attributesInstance.GetComponent<Attributes>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnEnable()
    {
        int rng;
        
        //Instatiate Upgrade Buttons
        rng = Random.Range(0, P1UpgradeButtons.Count);
        GameObject button1 = Instantiate(P1UpgradeButtons[rng], button1Loc.position, button1Loc.rotation) as GameObject;
        button1.transform.SetParent(canvas.transform);
        //P1UpgradeButtons.RemoveAt(rng);

        rng = Random.Range(0, P1UpgradeButtons.Count);
        GameObject button2 = Instantiate(P1UpgradeButtons[rng], button2Loc.position, button2Loc.rotation) as GameObject;
        button2.transform.SetParent(canvas.transform);
        //P1UpgradeButtons.RemoveAt(rng);

        rng = Random.Range(0, P1UpgradeButtons.Count);
        GameObject button3 = Instantiate(P1UpgradeButtons[rng], button3Loc.position, button3Loc.rotation) as GameObject;
        button3.transform.SetParent(canvas.transform);
        //P1UpgradeButtons.RemoveAt(rng);

        if (gameManager.isSplitScreen == true)
        {
            rng = Random.Range(0, P2UpgradeButtons.Count);
            GameObject button4 = Instantiate(P2UpgradeButtons[rng], button4Loc.position, button4Loc.rotation) as GameObject;
            button4.transform.SetParent(canvas.transform);
            //P1UpgradeButtons.RemoveAt(rng);

            rng = Random.Range(0, P2UpgradeButtons.Count);
            GameObject button5 = Instantiate(P2UpgradeButtons[rng], button5Loc.position, button5Loc.rotation) as GameObject;
            button5.transform.SetParent(canvas.transform);
            //P1UpgradeButtons.RemoveAt(rng);

            rng = Random.Range(0, P2UpgradeButtons.Count);
            GameObject button6 = Instantiate(P2UpgradeButtons[rng], button6Loc.position, button6Loc.rotation) as GameObject;
            button6.transform.SetParent(canvas.transform);
            //P1UpgradeButtons.RemoveAt(rng);
        }
        //Save current stats

    }

    public void OnButton()
    {
        //Destroy current level
        //Start next level
        if (gameManager.isSplitScreen == false && clickCount >= 1)
        {
            gameManager.gameOverWinObject.SetActive(false);
            Invoke("Reset", 0.01f);
            clickCount = 0;
        }
        if (gameManager.isSplitScreen == true && clickCount >= 2)
        {
            gameManager.gameOverWinObject.SetActive(false);
            Invoke("Reset", 0.01f);
            clickCount = 0;
        }
    }

    public void Reset()
    {
        GameManager.gameInstance.ResetGame();
        int nbChildren = levelParent.childCount;
        {
            for (int i = nbChildren - 1; i >= 0; i--)
            {
                DestroyImmediate(levelParent.GetChild(i).gameObject);
            }
        }
        Invoke("NextLevel", 0.01f);
    }

    public void NextLevel()
    {
        //Increase Gauntlet level and reset map
        gameManager.ActivateGameplay();
        gameManager.gauntletLevel += 1;
    }
}
