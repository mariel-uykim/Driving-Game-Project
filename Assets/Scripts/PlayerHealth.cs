using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int minCollisionDamage;
    //subtracts current health on collision with obstacles
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Obstacle")
        {
            if(col.impulse.magnitude > minCollisionDamage){
                GameManager.Instance.PlayerCollision(col.gameObject.name, col.impulse.magnitude);
            }
        }
    }

}