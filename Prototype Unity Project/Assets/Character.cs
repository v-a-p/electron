using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public float MovementSpeed = 5f;
    public Animator animator;
    public Rigidbody2D rb;

    Vector2 movement;

    // Update is called once per frame
    private void Update() //Input
    { 
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() { //Physics 

        rb.MovePosition(rb.position + movement * MovementSpeed * Time.fixedDeltaTime);

        if (movement.x < 0 && movement.y == 0) animator.SetInteger("Direction", 1); //Left
        else if (movement.x == 0 && movement.y > 0) animator.SetInteger("Direction", 2); //Up
        else if (movement.x > 0 && movement.y == 0) animator.SetInteger("Direction", 3); //Right
        else if (movement.x == 0 && movement.y < 0) animator.SetInteger("Direction", 4); //Down
        else if (movement.x < 0 && movement.y > 0) animator.SetInteger("Direction", 5); //Top-Left
        else if (movement.x > 0 && movement.y > 0) animator.SetInteger("Direction", 6); //Top-Right
        else if (movement.x < 0 && movement.y < 0) animator.SetInteger("Direction", 7); //Bottom-Left
        else if (movement.x > 0 && movement.y < 0) animator.SetInteger("Direction", 8); //Bottom-Right


        if (movement.sqrMagnitude > 0) animator.SetFloat("Speed", MovementSpeed);
        else animator.SetFloat("Speed", 0);
    }
}
