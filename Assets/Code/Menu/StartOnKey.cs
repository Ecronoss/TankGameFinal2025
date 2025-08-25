using UnityEngine;

public class StartOnKey : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            GameManager.gameInstance.ActivateMainMenu();
        }
    }
}
