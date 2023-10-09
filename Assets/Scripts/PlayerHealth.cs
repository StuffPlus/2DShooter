using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float health = 0f;
    [SerializeField]//so i can edit in unity
    private float maxhealth = 100f;
    public HealthBar healthBar;

    private void Start()
    {
        health = maxhealth; //might want to start the player with less health than the max health
        healthBar.SetMaxHealth(maxhealth);
    }

    public void UpdateHealth(float mod)
    {
        health += mod;
        if(health > maxhealth) // if we gain health we have to check if the player is not more than the max health
        {
            health = maxhealth;//make it so we cant go over the max health
        }
        else if(health <= 0)
        {
            health = 0f; // Once i can display health the health can't display lower than 0
            Debug.Log("Player Respawn"); //so that i know if the player loses all health
            Destroy(gameObject);
        }
        healthBar.SetHealth(health);
    }
}
