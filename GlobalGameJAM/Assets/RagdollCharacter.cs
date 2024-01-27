using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RagdollCharacter : MonoBehaviour
{
    bool a;
    Rigidbody[] ragdollRigidbodies;
    Animator anim;
    public AudioClip[] screams;
    AudioSource AS;
    

    private void Awake()
    {
        AS = GetComponentInChildren<AudioSource>();
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        anim = GetComponent<Animator>();

        ToggleRigidbody(false);
    }


    public void ToggleRigidbody(bool t)
    {
        
        anim.enabled = t;

        foreach (Rigidbody rigidbody in ragdollRigidbodies)
        {
            rigidbody.isKinematic = !t;
            
        }
    }

    public void screaming()
    {
        
        if (a == false)
        {
            a = true;
        AS.PlayOneShot(screams[Random.Range(0, screams.Length)]);
        }

    

    }

    private void OnTriggerEnter(Collider other)
    {
        ToggleRigidbody(true);
    }

    IEnumerator destroying()
    {
        yield return new WaitForSeconds(8);
        Destroy(gameObject);
    }
}
