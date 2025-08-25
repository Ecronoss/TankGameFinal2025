using UnityEngine;

public class PivotRotater : MonoBehaviour
{
    public Transform pivotPoint;
    public float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pivotPoint.position, Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
