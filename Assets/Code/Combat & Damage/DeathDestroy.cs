using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathDestroy : Death
{
    private AudioSource audioSource;
    public AudioClip deathSFX;
    public GameManager gameManager;
    private Pawn pawn;
    public float scoreToGive;
    public GameObject spawnOnDeath;
    public void Awake()
    {
        gameManager = GameManager.gameInstance.GetComponent<GameManager>();
        audioSource = gameManager.GetComponent<AudioSource>();
        pawn = GetComponent<Pawn>();
    }
    public override void Die(Pawn source)
    {
        //Spawn object if needed
        SpawnItem();
        //Object is destoryed
        audioSource.Play();
        Destroy(gameObject);
        gameManager.players.Remove(pawn.controller);
        if (pawn.controller != null && gameManager.players.Count < 1)
        {
            gameManager.ChangeGameState(gameManager.gameOverLossObject);
        }
        if (source.controller != null)
        {
            source.controller.score += scoreToGive;
        }

    }

    public void SpawnItem()
    {
        AIController control = GetComponent<AIController>();
        if (control != null)
        {
            if (control.hasKey == true)
            {
                GameObject spawned = Instantiate<GameObject>(spawnOnDeath, (pawn.transform.position), pawn.transform.rotation);
                //Make spawned object child to Level Parent
                GameObject levelParent = GameObject.Find("LevelParent");
                Transform levelParentTrans = levelParent.transform;
                Transform spawnedObjectTrans = spawned.transform;
                spawnedObjectTrans.parent = levelParentTrans;
            }
        }
    }
}
