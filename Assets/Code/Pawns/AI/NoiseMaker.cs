using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    public float noiseVolume;
    public float noiseDecayPerSec;

    //Update function
    void Update()
    {
        noiseVolume -= noiseDecayPerSec * Time.deltaTime;
        if (noiseVolume < 0)
        {
            noiseVolume = 0;
        }
    }

    public void MakeNoise(float amountOfNoiseMade)
    {
        noiseVolume =  Mathf.Max(amountOfNoiseMade, noiseVolume);
    }
}
