using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformControler : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        var x = Mathf.PingPong(Time.time * 2, 3);
        var p = new Vector3(x, 0, 0);
        transform.position = p;

        transform.Rotate(new Vector3(60,60,60) * Time.deltaTime); 
    }
}
