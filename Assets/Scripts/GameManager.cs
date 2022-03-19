using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using System;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    public UIManager uIManager;
    public Transform lastCheckpoint;
    private float currentTime;
    private float checkpointTime;

    //array and index of array for checkpoint time 
    private String [] checkpointsTime = new String[7];
    private int cpTimeIdx = 0;

    private int currentHealth;
    private String totalTime;
    public GameObject player;
    public GameObject carSmoke;
    public GameObject carExplosion;
    public int healthThreshold;
    public int maxHealth;
    public int healthDecay;
    public int addedHealth;
    private String lastCollidedObject;
    private bool gameOver;
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
        //starts analytics
        AnalyticsResult start = AnalyticsEvent.GameStart();

        //sets the max health and decay rate to specified variables
        HealthBarManager.Instance.MaxHealth(maxHealth);
        HealthBarManager.Instance.SetHealthDecay(healthDecay);
        HealthBarManager.Instance.SetAddedHealth(addedHealth);

        gameOver = false;
    }

    
    void Update()
    {
        //stopwatch display
        currentTime += Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        uIManager.UpdateTime(time);

        //time for since last checkpoint
        checkpointTime += Time.deltaTime;

        //get current health and check if health is below specified threshold, if so
        //activate smoke on car
        currentHealth = HealthBarManager.Instance.CurrentHealth;

        if(currentHealth <= healthThreshold)
        {
            carSmoke.SetActive(true);
        }
    }

    public void LastCheckpoint(Transform cp)
    {
        //record current checkpoint
        lastCheckpoint = cp;

        //convert time to string and push current checkpoint time to an array
        TimeSpan cpTime = TimeSpan.FromSeconds(checkpointTime);
        String timeFormat = cpTime.Minutes.ToString() + ":" + cpTime.Seconds.ToString() + ":" + cpTime.Milliseconds.ToString();
        checkpointsTime[cpTimeIdx] = timeFormat;

        //send current health to analytics
        totalTime = uIManager.CurrentTime();
        Analytics.CustomEvent("checkpointReached", new Dictionary<string, object>
        {
            { "time", totalTime },
            { "health", currentHealth },
            { "checkpoint", lastCheckpoint.position.ToString() }
        });

        //adds health to player's life
        HealthBarManager.Instance.AddHealth();

        //restart stopwatch for next checkpoint
        checkpointTime = 0;
        cpTimeIdx++;
    }
    public void PlayerCollision(string objectName, float impact)
    {
        //calls healthbar manager to reduce health and records last obstacle hit
        HealthBarManager.Instance.SetHealth(impact);
        lastCollidedObject = objectName;
    }
    public void GameOver(bool win)
    {
        if(!win)
        {
            carExplosion.SetActive(true);
        }
        //shows game over screen and pauses game
        uIManager.GameOverScreen(win, checkpointsTime);
        gameOver = true;
        
        //sends player death information to analytics
        if(gameOver)
        {
            totalTime = uIManager.CurrentTime();

            Analytics.CustomEvent("playerDeath", new Dictionary<string, object>
            {
                { "time", totalTime },
                { "position", player.transform.position },
                { "collided object", lastCollidedObject }
            });
            AnalyticsResult end = AnalyticsEvent.GameOver(); 
        }
    }
}
