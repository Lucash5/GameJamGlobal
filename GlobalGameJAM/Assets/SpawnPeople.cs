using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPeople : MonoBehaviour
{
    bool hasspawned;
    public GameObject person;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (hasspawned == false)
        {
            hasspawned = true;

        for (int i = 0; i < 75; i++)
        {
            GameObject klooni = Instantiate(person);
            klooni.transform.position = new Vector3(Random.Range(-1380, 1700), 0.35f, Random.Range(139, 148));
        }
        for (int i = 0; i < 150; i++)
        {
            GameObject klooni = Instantiate(person); 
            klooni.transform.position = new Vector3(Random.Range(-4460, -1380), 0.35f, Random.Range(139, 148));
        }
        for (int i = 0; i < 300; i++)
        {
            GameObject klooni = Instantiate(person);
            klooni.transform.position = new Vector3(Random.Range(-7540, -4460), 0.35f, Random.Range(139, 148));
        }
        for (int i = 0; i < 600; i++)
        {
            GameObject klooni = Instantiate(person);
            klooni.transform.position = new Vector3(Random.Range(-10620, -7540), 0.35f, Random.Range(139, 148));
        }
        for (int i = 0; i < 1200; i++)
        {
            GameObject klooni = Instantiate(person);
            klooni.transform.position = new Vector3(Random.Range(-13700, -10620), 0.35f, Random.Range(139, 148));
        }
        }
    }

  
    

    //139 148
    //1700 -13700
}
