using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; //Speed of enemy
    public bool isVertical; //Is the enemy moving vertically?
    private Vector2 moveDirection;
    Rigidbody2D rigidbodyComponent;

    public float changeDirectionTime = 2f; //When the enemy should change direction
    private float changeDirectionTimer; //Timer for enermy direction change

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveDirection = isVertical ? Vector2.up : Vector2.right; //If vertical movement, go up, else, go right;
        changeDirectionTimer = changeDirectionTime; //Set timer
    }

    // Update is called once per frame
    void Update()
    {
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer < 0) //If time to change direction
        {
            moveDirection *= -1; //Change direction
            changeDirectionTimer = changeDirectionTime; //Reset timer
        }

        Vector2 position = rigidbodyComponent.position;
        position.x += moveDirection.x * speed * Time.deltaTime; //Enemy's x-coordinate
        position.y += moveDirection.y * speed * Time.deltaTime; //Enemy's y-coordinate
        rigidbodyComponent.MovePosition(position);

        animator.SetFloat("MoveX", moveDirection.x);
        animator.SetFloat("MoveY", moveDirection.y);
    }

    //==== When enemy collides with player, player HP - 1 ================================
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null) player.ChangeHP(-1);
    }
}
