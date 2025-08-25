using UnityEngine;

[System.Serializable]
public class PowerupHeal : Powerup
{
    public float amountToHeal;
    public override void Apply(PowerupManager target)
    {
        //Apply Heal
        //Get target health
        Health targetHealthComponent = target.gameObject.GetComponent<Health>();
        if (targetHealthComponent != null)
        {
            //Apply negative damage
            targetHealthComponent.HealDamage(amountToHeal);
        }
    }

    public override void Remove(PowerupManager target)
    {
        //Remove Heal
        //Get target health
        Health targetHealthComponent = target.gameObject.GetComponent<Health>();
        if (targetHealthComponent != null)
        {
            //Apply negative damage
            targetHealthComponent.HealDamage(-amountToHeal);
        }
    }
}
