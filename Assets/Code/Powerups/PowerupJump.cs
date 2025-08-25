using UnityEngine;

[System.Serializable]
public class PowerupJump : Powerup
{
    public float amountToJump;
    public override void Apply(PowerupManager target)
    {
        //Apply Jumpforce
        //Get target Jumpforce
        Pawn targetComponent = target.gameObject.GetComponent<TankPawn>();
        if (targetComponent != null)
        {
            //Add jump foce
            targetComponent.jumpHeight += amountToJump;
        }
    }

    public override void Remove(PowerupManager target)
    {
        //Remove Jumpforce
        //Get target Jumpforce
        Pawn targetComponent = target.gameObject.GetComponent<TankPawn>();
        if (targetComponent != null)
        {
            //Apply negative jump force
            targetComponent.jumpHeight -= amountToJump;
        }
    }
}
