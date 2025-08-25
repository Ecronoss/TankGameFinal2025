using UnityEngine;

public class PickupGun : Pickup
{
    public PowerupGun powerup;

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
        //If it has powerup manager
        if (otherPowerupManager != null)
        {
            //...Apply powerup attatched to this object
            otherPowerupManager.AddPowerup(powerup);
            //Destroy object
            Destroy(gameObject);
        }
    }
}
