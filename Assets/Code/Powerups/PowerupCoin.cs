using UnityEngine;

[System.Serializable]
public class PowerupCoin : Powerup
{
    public float scoreToAdd;
    public override void Apply(PowerupManager target)
    {
        //Apply Score from pickup
        //Get target Score
        Pawn targetComponent = target.gameObject.GetComponent<Pawn>();
        if (targetComponent != null)
        if (true)
        {
            //Give Score
            targetComponent.controller.AddScore(scoreToAdd);
        }
    }

    public override void Remove(PowerupManager target)
    {
        //Remove Score from pickup
        //Get target Score
        PlayerController targetComponent = target.gameObject.GetComponent<PlayerController>();
        if (targetComponent != null)
        {
            //Take Score
            targetComponent.AddScore(-scoreToAdd);
        }
    }
}
