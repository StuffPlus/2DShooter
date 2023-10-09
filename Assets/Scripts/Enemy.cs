using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;// the speed of the enemy
    private Transform target;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")// to check whether the player has entered
        {
            target = other.transform;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       
        if (collision.gameObject.tag == "Player")// to check whether the player has left

        {
            target = null;
           

        }
    }
    private void Update()//Occurs every frame
    {
        
        if (target != null)
        {
            
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step); //moves the enemy to the player position 
        }
    }
    [SerializeField]
    private float attackDamage = 10f;


    [SerializeField]
    private float attackSpeed = 1f;
    private float canAttack; // so i can use a timer

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            if (attackSpeed <= canAttack)
            {
                collision.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
                canAttack = 0f;

            }
            else
            {
                canAttack += Time.deltaTime;
            }
        }
    }




}