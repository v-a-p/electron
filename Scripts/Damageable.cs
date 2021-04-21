using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //==== When collide with / stay inside damageables, HP - 1 =======================
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>(); //Create an instance of PlayerMovement
        if (player != null) //If player detected
        {
            player.ChangeHP(-1); //HP - 1
        }
    }
}
