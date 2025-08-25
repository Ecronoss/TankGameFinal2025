using UnityEngine;

[System.Serializable]
public class PowerupKey : Powerup
{
    public override void Apply(PowerupManager target)
    {
        //Apply Score from pickup
        //Get target Score
        Pawn targetComponent = target.gameObject.GetComponent<Pawn>();
        if (targetComponent != null)
        if (true)
        {
            //Activate Level Win
            
        }
    }

    public override void Remove(PowerupManager target)
    {
        //Remove Score from pickup
        //Get target Score
        PlayerController targetComponent = target.gameObject.GetComponent<PlayerController>();
        if (targetComponent != null)
        {
            //Nothing occurs
            return;
        }
    }
}
