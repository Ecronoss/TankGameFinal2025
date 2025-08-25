using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] Image backdrop;
    private GameManager gameManager;
    public Attributes attributes;
    public float newTime;

    void Awake()
    {
        gameManager = GameManager.gameInstance.GetComponent<GameManager>();
    }
    void Start()
    {
        //Get Components
        //If Gauntlet Mode is active, use time remaining from previous level
        attributes = Attributes.attributesInstance.GetComponent<Attributes>();
        newTime = attributes.totalTime;
        if (gameManager.gauntletLevel > 1)
        {
            gameManager.remainingTime = gameManager.timeBonus + newTime + attributes.timeAmp;
        }
        //If Gauntlet Mode is NOT active, use default time for current game mode
        if (gameManager.gauntletLevel <= 1)
        {
            gameManager.remainingTime = gameManager.timeClock;
        }
        Invoke("ClockPos", .01f);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Change time variables based on elapsed time in seconds & minutes
        if (attributes.player1 == null)
        {
            gameManager.remainingTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(gameManager.remainingTime / 60);
            int seconds = Mathf.FloorToInt(gameManager.remainingTime % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void ClockPos()
    {
        //Place timer in the center of the screen if multiplayer is active
        if (gameManager.isSplitScreen == true)
        {
            RectTransform timerTransform = timeText.GetComponent<RectTransform>();
            timerTransform.anchoredPosition = new Vector2(0, -5);
            RectTransform imageTransform = backdrop.GetComponent<RectTransform>();
            imageTransform.anchoredPosition = new Vector2(0, -2);
        }
        //Place timer at bottom of screen if multiplayer is NOT active
        else
        {
            RectTransform timerTransform = timeText.GetComponent<RectTransform>();
            timerTransform.anchoredPosition = new Vector2(0, -205);
            RectTransform imageTransform = backdrop.GetComponent<RectTransform>();
            imageTransform.anchoredPosition = new Vector2(0, -203);
        }
    }
}
