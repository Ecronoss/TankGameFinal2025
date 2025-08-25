using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> powerupPrefabs;
    private int numPowerups;
    public bool randPowerup;
    public GameObject objectToSpawn;
    public float firstSpawnDelay;
    public float respawnTime;
    private float nextSpawnTime;
    private GameObject spawnedObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextSpawnTime = Time.time + firstSpawnDelay;
        numPowerups = powerupPrefabs.Count;
        if (randPowerup)
        {
            objectToSpawn = powerupPrefabs[Random.Range(0, powerupPrefabs.Count)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Check for spawn
        CheckForSpawn();
    }

    public void CheckForSpawn()
    {

        if (spawnedObject == null)
        //Spawn object
        {
            if (Time.time >= nextSpawnTime)
            {
                spawnedObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity) as GameObject;
                //Make spawned object child of Level Parent
                GameObject levelParent = GameObject.Find("LevelParent");
                Transform levelParentTrans = levelParent.transform;
                Transform spawnedObjectTrans = spawnedObject.transform;
                spawnedObjectTrans.parent = levelParentTrans;
                //Set next respawn time
                nextSpawnTime = Time.time + respawnTime;
            }

        }
        else
        {
            //If spawned object exists, extend respawn time
            nextSpawnTime = Time.time + respawnTime;
        }
    }
}  
