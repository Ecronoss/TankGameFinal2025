using UnityEngine;

public class SaveOnLoad : MonoBehaviour
{
    public SaveOnLoad objectInstance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (objectInstance == null)
        {
            // Set instance of GM to our static variable
            objectInstance = this;

            // Prevent object destruction on scene load
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Prevents multiple GMs from existing
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
