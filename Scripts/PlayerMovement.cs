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

    public GameObject arrowPrefab; //Arrow (weapon)

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

        rigidbodyComponent.MovePosition(position);

        //===== Invincible period after taking damage =========================
        if (stopDamage)
        {
            stopDamageTimer -= Time.deltaTime;
            if (stopDamageTimer < 0)
            {
                stopDamage = false;
            }
        }

        //==== Shooting arrow (Key space) ====================================
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject arrow = Instantiate(arrowPrefab, rigidbodyComponent.position, Quaternion.identity);
            ArrowShooting arrowShoot = arrow.GetComponent<ArrowShooting>();
            if (arrowShoot != null)
            {
                arrowShoot.ArrowMovement(lookDirection, 200);
            }
        }

        //==== Press down Z key to intereact with NPC =================================
        if (Input.GetKeyDown(KeyCode.Z))
        {
            RaycastHit2D npcDetect = Physics2D.Raycast(rigidbodyComponent.position, lookDirection, 2f, LayerMask.GetMask("NPC"));
            if (npcDetect.collider != null)
            {
                Debug.Log("NPC exist");
                NPCManager npc = npcDetect.collider.GetComponent<NPCManager>();
                if (npc != null) npc.ShowDialog();
            }
        }
    }

    public void ChangeHP(int amount)
    {
        if (amount < 0)
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

    //==== Return empty detector position (not relative to player) Down Left ==================================================
    [SerializeField] private Transform downLeft;

    public float downLeftX
    {
        get { return downLeft.position.x; }
    }

    public float downLeftY
    {
        get { return downLeft.position.y; }
    }

    //==== Return empty detector position (not relative to player) Down Right ==================================================
    [SerializeField] private Transform downRight;

    public float downRightX
    {
        get { return downRight.position.x; }
    }

    public float downRightY
    {
        get { return downRight.position.y; }
    }
    //==== Return empty detector position (not relative to player) Top Left ==================================================
    [SerializeField] private Transform topLeft;

    public float topLeftX
    {
        get { return topLeft.position.x; }
    }

    public float topLeftY
    {
        get { return topLeft.position.y; }
    }

    //==== Return empty detector position (not relative to player) Top Right ==================================================
    [SerializeField] private Transform topRight;

    public float topRightX
    {
        get { return topRight.position.x; }
    }

    public float topRightY
    {
        get { return topRight.position.y; }
    }
}
