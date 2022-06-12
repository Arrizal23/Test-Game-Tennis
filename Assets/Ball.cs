using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector3 initialPos; 

    private void Start()
    {
        initialPos = transform.position; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall")) 
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero; 
            transform.position = initialPos; 
        }
    }

}