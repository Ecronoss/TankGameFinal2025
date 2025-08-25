using UnityEngine;

public class KillTimer : MonoBehaviour
{
    public Timer timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject gameObject = GetComponent<GameObject>();
        if (timer.enabled = false)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
