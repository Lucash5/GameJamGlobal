using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CarMovement : MonoBehaviour
{
    public ParticleSystem[] particles;

    public Transform mickey;
    public AudioClip[] audioclips;
    public AudioSource[] AS;
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
        foreach (ParticleSystem part in particles)
        {
            part.gameObject.SetActive(false);
        }

        StartCoroutine(startcounting());


        AS[0] = GetComponent<AudioSource>();
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

    private void OnTriggerEnter(Collider other)
    {

        RagdollCharacter rc = other.gameObject.GetComponentInParent<RagdollCharacter>();
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
        AS[1].PlayOneShot(audioclips[2]);
        TMP.text = "Willey : Remember to follow driving regulations and avoid driving over people.";

        yield return new WaitForSeconds(15);
        TMP.text = "Willey : I think you can press the gas a little more now.";
        maxSpeed = 30;




        yield return new WaitForSeconds(20);
        TMP.text = "Willey : Lets slow down a little.";
        AS[1].Stop();
        AS[1].PlayOneShot(audioclips[3]);
        yield return new WaitForSeconds(2);
        maxSpeed = 100;
        AS[0].PlayOneShot(audioclips[0]);
        yield return new WaitForSeconds(1);
        AS[0].PlayOneShot(audioclips[1]);
        
        foreach (ParticleSystem part in particles)
        {
            part.gameObject.SetActive(true);
        }
        
        AS[1].Stop();
        AS[1].PlayOneShot(audioclips[1]);
        TMP.text = "Willey : what the fuck are you doing!";
        AS[1].loop = true;
        yield return new WaitForSeconds(3);
        TMP.text = "";

    }

    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        if (rigidbody != null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            //forceDirection.y = 0;
            forceDirection.Normalize();
            rigidbody.AddForceAtPosition(forceDirection * 5, transform.position, ForceMode.Impulse);
        }
    }
}
