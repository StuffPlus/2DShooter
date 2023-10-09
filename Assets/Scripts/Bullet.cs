using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{ 
public GameObject hitEffect; //Attach an animation for when the bullet hits an object.
public int damage = 40;

private void OnCollisionEnter2D(Collision2D collision)
{

    BoxCollider2D enemyHitBox = collision.gameObject.GetComponent<BoxCollider2D>();
    if (collision.collider == enemyHitBox) // this checks if the collider hit is the box collider.
    {

        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); //call the take damage function in the enemy health script.
        }
    }
    GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);//Duplicate the animation at the position it collides with an object. Quaternion is used as there is no rotation needed in this function.

    Destroy(effect, 0.15f); // Set a delay to when the prefab is destroyed in order for the bullet destroying animation to play out
    Destroy(gameObject); // Destroy the bullet prefab

} }


