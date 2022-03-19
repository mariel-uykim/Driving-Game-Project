using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private static CheckpointManager instance;
    public static CheckpointManager Instance
    {
        get
        {
            return instance;
        }
    }

    private bool currentCPStatus;
    private int currentCP;
    public Checkpoint[] checkpoints;
    public bool CurrentCPStatus
    {
        set
        {
            currentCPStatus = value;
        }
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        
    }

    void Start()
    {
        //initializes current checkpoint
        currentCP = 0;
        currentCPStatus = true;

        //sets every checkpoint to inactive color except first
        for(int i = 0; i < checkpoints.Length; i++)
        {
            if(i == 0)
            {
                checkpoints[i].ChangeActiveColor();
            }
            else{
                checkpoints[i].ChangeInactiveColor();
            }
        }
    }

    void Update()
    {
        //moves to next checkpoint when current one has been reached
        if(!currentCPStatus)
        {
            //calls game over function in Game Manager when last checkpoint is reached
            if(currentCP >= checkpoints.Length - 1)
            {
                GameManager.Instance.GameOver(true);
                gameObject.SetActive(false);
            }
            else
            {
                currentCP++;
                NextCheckpoint();
            }
        }
    }

    //activates the next checkpoint
    void NextCheckpoint()
    {
        checkpoints[currentCP].ChangeActiveColor();
        currentCPStatus = true;
    }
}
