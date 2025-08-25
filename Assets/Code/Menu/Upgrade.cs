using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public GameObject victoryObject;
    public VictoryManager victoryCanvas;
    public Attributes attributes;
    public GameObject[] allButton1;
    public GameObject[] allButton2;
    public GameManager gameManager;
    public int clickCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {

    }
    void Start()
    {
        victoryObject = GameObject.Find("WinScreenCanvas");
        victoryCanvas = victoryObject.GetComponent<VictoryManager>();
        attributes = Attributes.attributesInstance.GetComponent<Attributes>();
        //Set list of buttons to destroy
        allButton1 = GameObject.FindGameObjectsWithTag("P1Button");
        allButton2 = GameObject.FindGameObjectsWithTag("P2Button");
        gameManager = GameManager.gameInstance.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        victoryCanvas.clickCount += 1;
        victoryCanvas.OnButton();
    }

    //Button Codes Here
    public void GetHealth1()
    {
        attributes.P1HealthAmp += victoryCanvas.healthUpgradeValue;
        attributes.P1CurrentHP += 100;
        killP1Buttons();
    }

    public void GetHealth2()
    {
        attributes.P2HealthAmp += victoryCanvas.healthUpgradeValue;
        attributes.P2CurrentHP += 100;
        killP2Buttons();
    }

    public void GetAttackSpeed1()
    {
        attributes.P1AttackSpeedAmp += victoryCanvas.attackSpeedUpgradeValue;
        killP1Buttons();
    }

    public void GetAttackSpeed2()
    {
        attributes.P2AttackSpeedAmp += victoryCanvas.attackSpeedUpgradeValue;
        killP2Buttons();
    }

    public void GetDamage1()
    {
        attributes.P1DamageAmp += victoryCanvas.damageUpgradeValue;
        killP1Buttons();
    }

    public void GetDamage2()
    {
        attributes.P2DamageAmp += victoryCanvas.damageUpgradeValue;
        killP2Buttons();
    }

    public void GetKey1()
    {
        attributes.P1ArrowRounds += victoryCanvas.arrowUpgradeValue;
        killP1Buttons();
    }

    public void GetKey2()
    {
        attributes.P2ArrowRounds += victoryCanvas.arrowUpgradeValue;
        killP2Buttons();
    }

    public void GetPowerup1()
    {
        attributes.P1PowerupAmp += victoryCanvas.powerupUpgradeValue;
        killP1Buttons();
    }

    public void GetPowerup2()
    {
        attributes.P2PowerupAmp += victoryCanvas.powerupUpgradeValue;
        killP2Buttons();
    }

    public void GetSpeed1()
    {
        attributes.P1SpeedAmp += victoryCanvas.speedUpgradeValue;
        killP1Buttons();
    }

    public void GetSpeed2()
    {
        attributes.P2SpeedAmp += victoryCanvas.speedUpgradeValue;
        killP2Buttons();
    }

    public void GetTime1()
    {
        attributes.timeAmp += victoryCanvas.timeUpgradeValue;
        killP1Buttons();
    }

    public void GetTime2()
    {
        attributes.timeAmp += victoryCanvas.timeUpgradeValue;
        killP2Buttons();
    }

    public void GetLevel1()
    {
        attributes.levelAmp += victoryCanvas.levelUpgradeValue;
        killP1Buttons();
    }

    public void GetLevel2()
    {
        attributes.levelAmp += victoryCanvas.levelUpgradeValue;
        killP2Buttons();
    }

    public void GetShot1()
    {
        attributes.P1ShotGunRounds += victoryCanvas.shotGunUpgradeValue;
        killP1Buttons();
    }

    public void GetShot2()
    {
        attributes.P2ShotGunRounds += victoryCanvas.shotGunUpgradeValue;
        killP2Buttons();
    }

    public void killP1Buttons()
    {
        foreach (GameObject button in allButton1)
        {
            Destroy(button);
        }
    }

    public void killP2Buttons()
    {
        foreach (GameObject button in allButton2)
        {
            Destroy(button);
        }
    }

}
