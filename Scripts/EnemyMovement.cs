using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    public bool isVertical;
    private Vector2 moveDirection;
    Rigidbody2D rigidbodyComponent;

    public float changeDirectionTime = 2f;
    private float changeDirectionTimer;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveDirection = isVertical ? Vector2.up : Vector2.right;
        changeDirectionTimer = changeDirectionTime;
    }

    // Update is called once per frame
    void Update()
    {
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer < 0)
        {
            moveDirection *= -1;
            changeDirectionTimer = changeDirectionTime;
        }

        Vector2 position = rigidbodyComponent.position;
        position.x += moveDirection.x * speed * Time.deltaTime;
        position.y += moveDirection.y * speed * Time.deltaTime;
        rigidbodyComponent.MovePosition(position);

        animator.SetFloat("MoveX", moveDirection.x);
        animator.SetFloat("MoveY", moveDirection.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null) player.ChangeHP(-1);
    }
}
