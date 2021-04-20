using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody2D rigidbodyComponent;
   
    public int maxHP = 10;
    public int currentHP;

    private bool stopDamage;
    private float stopDamageTime = 1f;
    private float stopDamageTimer;

    private Vector2 lookDirection = new Vector2(1, 0); //Default direction is right

    private static float playerX;
    private static float playerY;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        currentHP = 8;
        UIManager.instance.UpdateHP(currentHP, maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Vector2 position = rigidbodyComponent.position;
        position.x += horizontalMovement * speed * Time.deltaTime;
        position.y += verticalMovement * speed * Time.deltaTime;
        playerX = position.x;
        playerY = position.y;
        rigidbodyComponent.MovePosition(position);

        //===== Invincible period after taking damage =========================
        if (stopDamage)
        {
            stopDamageTimer -= Time.deltaTime;
            if(stopDamageTimer < 0)
            {
                stopDamage = false;
            }
        }

        //==== Interacting with NPC (Key Z) ====================================
        if (Input.GetKeyDown(KeyCode.Z))
        {

        }
    }

    public void ChangeHP(int amount)
    {
        if(amount < 0)
        {
            if (stopDamage) return;
            currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);
            stopDamage = true;
            stopDamageTimer = stopDamageTime;
        }
        else
        {
            currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);
        }
        UIManager.instance.UpdateHP(currentHP, maxHP);
        Debug.Log(currentHP + "/" + maxHP);
    }

    //==== Return player position ==================================================
    public float PlayerX
    {
        get { return playerX; }
    }

    public float PlayerY
    {
        get{ return playerY; }
    }

    //==== Return corner position ==================================================
    [SerializeField] private Transform corner;

    public float CornerX
    {
        get { return corner.position.x; }
    }

    public float CornerY
    {
        get { return corner.position.y; }
    }
}
