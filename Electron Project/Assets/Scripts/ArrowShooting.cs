using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooting : MonoBehaviour
{
    Rigidbody2D rigidbodyComponent;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ArrowMovement(Vector2 direction, float force)
    {
        rigidbodyComponent.AddForce(direction * force);
    }
}
