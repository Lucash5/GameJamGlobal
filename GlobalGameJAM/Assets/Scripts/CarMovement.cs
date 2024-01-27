using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CarMovement : MonoBehaviour
{
    public Transform mickey;
    public AudioClip[] audioclips;
    AudioSource AS;
    public float speed;
    public float maxSpeed = 200;
    public float steeringspeed;
    Rigidbody rb;
    
    public TMP_Text TMP;

    public float currentSpeed; 

    bool hasFlipped = false;

    float inputX;
    float inputZ = 0;
    
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(startcounting());


        AS = GetComponent<AudioSource>();
        rb =  GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        mickey.rotation = new Quaternion(mickey.rotation.x, mickey.rotation.y - 0.0007f, mickey.rotation.z, mickey.rotation.w);
        

        inputX = Input.GetAxisRaw("Horizontal") * steeringspeed;

        inputZ = Mathf.Clamp(inputZ, 0, maxSpeed);
    }

    /*void FixedUpdate()
    {
        if (!hasFlipped)
        {
            rb.velocity = new Vector3(inputZ * -1, rb.velocity.y, inputX);
        }
    }*/


    private void OnCollisionEnter(Collision collision)
    {
        RagdollCharacter rc = collision.gameObject.GetComponentInParent<RagdollCharacter>();

        if (rc != null)
        {
            rc.ToggleRigidbody(true);
            rc.screaming();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Road")
        {
            inputZ += speed * Time.deltaTime;
            rb.velocity = new Vector3(inputZ * -1, rb.velocity.y, inputX);
        }
    }


    IEnumerator startcounting()
    {

        yield return new WaitForSeconds(3);
        AS.PlayOneShot(audioclips[1]);
        yield return new WaitForSeconds(2);
        TMP.text = "Mickey : I think you can press the gas a little more now";
        maxSpeed = 100;
        AS.PlayOneShot(audioclips[0]);

    }
}
