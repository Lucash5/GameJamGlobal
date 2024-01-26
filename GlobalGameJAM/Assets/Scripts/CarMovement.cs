using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed;
    public float steeringspeed;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb =  GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3 (-1 * speed, rb.velocity.y, Input.GetAxisRaw("Horizontal") * steeringspeed);

    }
}
