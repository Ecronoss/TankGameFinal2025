using UnityEngine;

[System.Serializable]
public class PowerupSpeed : Powerup
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
            targetPawnComponent.moveSpeed += amountToSpeedup;
            targetPawnComponent.shootForce += (amountToSpeedup * 20);
        }
        if (targetAIControllerComponent != null)
        {
            //Apply negative damage
            targetAIControllerComponent.baseSpeed += amountToSpeedup;
            targetPawnComponent.rotateSpeed += (amountToSpeedup * 20);
            targetPawnComponent.shootForce += (amountToSpeedup *20);
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
            targetPawnComponent.moveSpeed -= amountToSpeedup;
            targetPawnComponent.shootForce -= (amountToSpeedup * 20);
        }
        if (targetAIControllerComponent != null)
        {
            //Apply negative damage
            targetAIControllerComponent.baseSpeed -= amountToSpeedup;
            targetPawnComponent.rotateSpeed -= (amountToSpeedup * 20);
            targetPawnComponent.shootForce -= (amountToSpeedup *20);
        }
    }
}
