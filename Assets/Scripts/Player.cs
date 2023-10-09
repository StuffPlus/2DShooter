using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider; //The collider for 2d physics
    private RaycastHit2D hit;// Casting the collider Box
    private Vector3 moveDelta; // 3d vectors allowing movement
    public float speed = 5f;
   

    private void Start() //Only ran once 
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate() //Function happens each frame
    {
        //Gets inputs from the Horizontal and vertical inputs
        float x = Input.GetAxisRaw("Horizontal"); //A and D keys
        float y = Input.GetAxisRaw("Vertical"); // W and S keys


        

        //Resets moveDelta
       moveDelta = new Vector3(x,y,0);

        

        //Make sure we can move in this direction by casting a box there first, if the box returns null we are free to move

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y*speed), Mathf.Abs(moveDelta.y * speed* Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //Make sprite move
        transform.Translate(0, moveDelta.y * speed * Time.deltaTime, 0); // deltatime allows for low end computers to run at the same time compared to high end computers 
        }
        //hit detection for the y axis
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x* speed, 0), Mathf.Abs(moveDelta.x * speed * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //Make sprite move
            transform.Translate(moveDelta.x * speed * Time.deltaTime, 0, 0); 
        }

    }

}
