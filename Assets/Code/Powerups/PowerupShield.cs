using UnityEngine;

[System.Serializable]
public class PowerupShield : Powerup
{
    public override void Apply(PowerupManager target)
    {
        //Apply shields
        //Get target component
        Pawn targetPawnComponent = target.gameObject.GetComponent<Pawn>();
        if (targetPawnComponent != null)
        {
            //Activate Shields
            targetPawnComponent.shieldActive = true;
        }
    }

    public override void Remove(PowerupManager target)
    {
        //Remove shields
        //Get target Shields
        Pawn targetPawnComponent = target.gameObject.GetComponent<Pawn>();
        if (targetPawnComponent != null)
        {
            //Apply negative damage
            targetPawnComponent.shieldActive = false;
        }
    }
}
