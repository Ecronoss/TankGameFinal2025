using UnityEngine;

public class PickupCoin : Pickup
{
    public PowerupCoin powerup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,rotateSpeed*Time.deltaTime,0);
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
            //...Apply powerup attatched to this object
            otherPowerupManager.AddPowerup(powerup);
            //Destroy object
            Destroy(gameObject);
        }
    }
}
