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

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null) //Player detected
        {
            player.ChangeHP(-1);
        }
    }
}
