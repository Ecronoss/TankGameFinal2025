using UnityEngine;
using System.Collections.Generic;

public class PowerupManager : MonoBehaviour
{
    public List<Powerup> powerups;
    private List<Powerup> powerupRemoval;
    private AudioSource audioSource;
    public AudioClip powerSound;
    public Pawn pawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Clear powerup list
        powerups = new List<Powerup>();
        powerupRemoval = new List<Powerup>();
        audioSource = GetComponent<AudioSource>();
        pawn = GetComponent<Pawn>();

    }

    // Update is called once per frame
    void Update()
    {
        DecrementPowerupTimer();
    }

    public void DecrementPowerupTimer()
    {
        foreach (Powerup powerup in powerups)
        {
            if (!powerup.permanent)
            {
                //Decrease powerup timer
                powerup.duration -= Time.deltaTime;
                //If timer hits 0, remove powerup
                if (powerup.duration <= 0)
                {
                    powerupRemoval.Add(powerup);
                }
            }
        }
        //Remove qued up powerups from removal list
        foreach (Powerup powerup in powerupRemoval)
        {
            RemovePowerup(powerup);
        }
        //Clear removal list
        powerupRemoval.Clear();
    }
    public void AddPowerup(Powerup addedPowerup)
    {
        if (powerSound != null)
        {
            audioSource.PlayOneShot(powerSound);
        }
        //Add the powerup
        addedPowerup.duration += pawn.powerupPlus;
        addedPowerup.Apply(this);
        powerups.Add(addedPowerup);
    }

    public void RemovePowerup(Powerup removedPowerup)
    {
        removedPowerup.Remove(this);
        powerups.Remove(removedPowerup);
    }
}
