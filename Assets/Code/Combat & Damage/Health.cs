using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHP;
    public float currentHP;
    private Death deathComponent;
    private Pawn pawn;
    private TankPawn tankPawn;
    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip deathSound;
    public float healthPercentage;
    public Image health1;
    public Image underHealth;
    private GameManager gameManager;
    public Attributes attributes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get DeathComponent
        deathComponent = GetComponent<Death>();

        //Start full HP
        currentHP = maxHP;
        healthPercentage = 1;

        //Get pawn component
        pawn = GetComponent<Pawn>();
        tankPawn = GetComponent<TankPawn>();
        gameManager = GameManager.gameInstance.GetComponent<GameManager>();
        attributes = Attributes.attributesInstance.GetComponent<Attributes>();
        audioSource = GetComponent<AudioSource>();
        GuantCheck();
        healthPercentage = currentHP / maxHP;
        if (pawn.controller != null)
        {
            if (pawn.controller.name == "Player 2")
            {
                RectTransform healthTransform = health1.GetComponent<RectTransform>();
                healthTransform.anchoredPosition = new Vector2(0, 0);
                healthTransform = underHealth.GetComponent<RectTransform>();
                healthTransform.anchoredPosition = new Vector2(365, 199);
            }
        }
    }

    public void Update()
    {
        health1.fillAmount = healthPercentage;
        if (gameManager.remainingTime <= 0)
        {
            TakeTrueDamage(10, pawn);
        }
    }

    //Take Damage
    public void TakeDamage(float damage, Pawn source, Pawn dealer)
    {
        if (!pawn.shieldActive)
        {
            //Subtract HP
            if (currentHP == 1)
            {
                audioSource.PlayOneShot(deathSound);
            }
            currentHP -= damage;
            audioSource.PlayOneShot(hitSound);

            //Value bounds
            currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        }


        //Kill at 0
        if (currentHP <= 0)
        {
            deathComponent.Die(dealer);
        }

        healthPercentage = currentHP / maxHP;

    }

    public void TakeTrueDamage(float damage, Pawn source)
    {
        //Subtract HP
        if (currentHP == 1)
        {
            audioSource.PlayOneShot(deathSound);
        }
        currentHP -= damage;
        audioSource.PlayOneShot(hitSound);

        //Value bounds
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);


        //Kill at 0
        if (currentHP <= 0)
        {
            deathComponent.Die(source);
        }

        healthPercentage = currentHP / maxHP;

    }

    public void HealDamage(float heal)
    {
        //Subtract HP
        currentHP += heal;

        //Value bounds
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        healthPercentage = currentHP / maxHP;
    }

    public void GuantCheck()
    {
        if (gameManager.gauntletLevel > 1)
        {
            if (pawn.controller != null)
            {
                if (pawn.controller.name == "Player 1")
                {
                    maxHP += attributes.P1HealthAmp;
                    currentHP = attributes.P1CurrentHP;
                }
                if (pawn.controller.name == "Player 2")
                {
                    maxHP += attributes.P2HealthAmp;
                    currentHP = attributes.P2CurrentHP;
                }
            }
            if (currentHP <= 0)
            {
                currentHP = 1;
            }
            if (currentHP > maxHP)
            {
                currentHP = maxHP;
            }
        }
    }

    public void HealthPos()
    {
        
    }
}
