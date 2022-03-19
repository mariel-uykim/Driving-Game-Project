using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Renderer cpRenderer;
    public Color activeColor;
    public Color inactiveColor;
    private bool isActive;
    private BoxCollider sphereCollider;
    public float radius;
    void Start()
    {
        isActive = false;
        cpRenderer = GetComponent<Renderer>();

        //set detection radius by re-sizing box collider
        sphereCollider = GetComponent<BoxCollider>();
        sphereCollider.size = new Vector3(radius, transform.position.y, radius);

    }

    //calls gameManager on trigger

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "player" && isActive)
        {
            ChangeInactiveColor();
            CheckpointManager.Instance.CurrentCPStatus = false;
            GameManager.Instance.LastCheckpoint(transform);
        }
    }

    //sets to inactive color when called
    public void ChangeInactiveColor()
    {  
        if(inactiveColor != null && cpRenderer != null){
            isActive = false;
            cpRenderer.material.color = inactiveColor;
        }
    }

    //sets to active color when called
    public void ChangeActiveColor()
    {
        if(activeColor != null && cpRenderer != null){
            isActive = true;
            cpRenderer.material.color = activeColor;
        }
    }
}
