using UnityEngine;

public class ArrowRotate : MonoBehaviour
{
    public Pawn pawn;
    public LevelGenerator levelGenerator;
    public float rotateSpeed = 360;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelGenerator = GameManager.gameInstance.GetComponent<LevelGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelGenerator.keyLocation != null)
        {
            Vector3 keyVector = levelGenerator.keyLocation.position;
            RotateToward(keyVector);
        }
        if (GameManager.gameInstance.gauntletLevel > 1 && levelGenerator.keyLocation == null)
        {
            GameObject key = GameObject.Find("Key(Clone)");
            if (key != null)
            {
                Vector3 keyVector = key.transform.position;
                RotateToward(keyVector);
            }
        }
    }
 
    public void RotateToward(Vector3 targetPosition)
    {
        //Find target vector
        Vector3 vectorToTarget = targetPosition - this.transform.position;
        vectorToTarget.y = 0f;
        //Find rotation to aim at vector
        Quaternion rotationVector = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        //Change rotation based on turn speed
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotationVector, this.rotateSpeed * Time.deltaTime);
    }
}
