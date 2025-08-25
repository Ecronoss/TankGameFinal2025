using UnityEngine;

public class PickupKey : Pickup
{
    public PowerupKey powerup;
    public GameManager gameManager;
    public Attributes attributes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.gameInstance.GetComponent<GameManager>();
        attributes = Attributes.attributesInstance.GetComponent<Attributes>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    public void OnTriggerEnter(Collider other)
    {
        //Apply the Powerup
        //Get the powerup manager of colliding object
        PowerupManager otherPowerupManager = other.gameObject.GetComponent<PowerupManager>();
        Pawn otherPawn = other.gameObject.GetComponent<Pawn>();
        //If it has powerup manager
        if (otherPowerupManager != null && otherPawn.controller != null)
        {
            //...Activate Level Win
            gameManager.ChangeGameState(gameManager.gameOverWinObject);
            attributes.Check();
            //Destroy object
            Destroy(gameObject);
        }
    }
}
