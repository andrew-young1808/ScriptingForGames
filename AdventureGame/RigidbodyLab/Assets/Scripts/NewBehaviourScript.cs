using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.right * 500);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with " +  collision.gameObject.name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
