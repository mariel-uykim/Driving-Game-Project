using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target; 
    private float targetX;
    private float targetY;
    private float targetZ;
    public Vector3 offset;
    public GameObject targetSubObject;
    public float decay = 15f;
    public Transform lookAtTarget; 


    void FixedUpdate() 
    {
        targetX = target.transform.eulerAngles.x;
        targetY = target.transform.eulerAngles.y;
        targetZ = target.transform.eulerAngles.z;

        Vector3 cameraPos = targetSubObject.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, cameraPos, decay * Time.deltaTime);
        transform.eulerAngles = new Vector3(targetX - targetX, targetY, targetZ-targetZ);
    }
}
