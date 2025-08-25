using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RainbowBullet : MonoBehaviour
{
    public List<Material> colors;
    private GameObject gameObject;
    private Renderer material;
    private int materialCount;
    private int newMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        materialCount = colors.Count;
        gameObject = GetComponent<GameObject>();
        material = GetComponent<Renderer>();
        newMaterial = Random.Range(0, materialCount);
        material.sharedMaterial = colors[newMaterial];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
