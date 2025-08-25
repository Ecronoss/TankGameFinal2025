using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnStartButton()
    {
        GameManager.gameInstance.ActivateGameplay();
        Invoke("GauntletStart", 0.1f);
    }

    public void OnOptionsButton()
    {
        GameManager.gameInstance.ActivateOptions();
    }

    public void OnCreditsButton()
    {
        GameManager.gameInstance.ActivateCredits();
    }

    public void OnQuitButton()
    {
        //Quit Game
        Application.Quit();
    }

    public void GauntletStart()
    {
        GameManager.gameInstance.gauntletLevel += 1;
    }
}
