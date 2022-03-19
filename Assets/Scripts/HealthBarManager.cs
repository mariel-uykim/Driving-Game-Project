using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarManager : MonoBehaviour
{
    public Slider healthSlider;
    private int currentHealth;
    private int healthDecay;
    private int addedHealth;
    private static HealthBarManager instance;
    public static HealthBarManager Instance
    {
        get
        {
            return instance;
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
    public int CurrentHealth
    {
        get{
            return currentHealth;
        }
    }

    //sets the maximum health of player
    public void MaxHealth(int max)
    {
        healthSlider.maxValue = max;
        healthSlider.value = max;
        currentHealth = max;
    }

    //sets health decay
    public void SetHealthDecay(int decay)
    {
        healthDecay = decay;
    }

    //sets added health 
    public void SetAddedHealth(int addAmount)
    {
        addedHealth = addAmount;
    }

    //changes health value in the health bar when called
    public void SetHealth(float impact)
    {
        if(currentHealth <= (impact / healthDecay))
        {
            currentHealth = 0;
            GameManager.Instance.GameOver(false);
        }

        else 
        {
            currentHealth-=(int)(impact / healthDecay);
        }

        healthSlider.value = currentHealth;
    }

    //adds health to current health
    public void AddHealth()
    {
        currentHealth += addedHealth;
        healthSlider.value = currentHealth;
    }
}
