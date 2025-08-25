using UnityEngine;

[System.Serializable]
public class PowerupGun : Powerup
{
public float amountToSpeedup;
    public override void Apply(PowerupManager target)
    {
        //Apply Speed
        //Get target speed
        Pawn targetPawnComponent = target.gameObject.GetComponent<Pawn>();
        AIController targetAIControllerComponent = target.gameObject.GetComponent<AIController>();
        if (targetPawnComponent != null)
        {
            //Apply negative damage
            targetPawnComponent.shotsPerSec += amountToSpeedup;
        }
        if (targetAIControllerComponent != null)
        {
            //Apply negative damage
            targetAIControllerComponent.baseAttackSpeed += amountToSpeedup;
        }
    }

    public override void Remove(PowerupManager target)
    {
        //Remove Speed
        //Get target speed
        Pawn targetPawnComponent = target.gameObject.GetComponent<Pawn>();
        AIController targetAIControllerComponent = target.gameObject.GetComponent<AIController>();
        if (targetPawnComponent != null)
        {
            //Apply negative damage
            targetPawnComponent.shotsPerSec -= amountToSpeedup;
        }
        if (targetAIControllerComponent != null)
        {
            //Apply negative damage
            targetAIControllerComponent.baseAttackSpeed -= amountToSpeedup;
        }
    }
}
