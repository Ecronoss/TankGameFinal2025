using UnityEngine;
[System.Serializable]
public class PowerupStar : Powerup
{
    public override void Apply(PowerupManager target)
    {
        //Apply Morning Star
        //Get target component
        Pawn targetPawnComponent = target.gameObject.GetComponent<Pawn>();
        if (targetPawnComponent != null)
        {
            //Activate Morning Star
            targetPawnComponent.starActive = true;
        }
    }

    public override void Remove(PowerupManager target)
    {
        //Remove Morning Star
        //Get target Morning Star
        Pawn targetPawnComponent = target.gameObject.GetComponent<Pawn>();
        if (targetPawnComponent != null)
        {
            //Remove Morning Star
            targetPawnComponent.starActive = false;
        }
    }
}
