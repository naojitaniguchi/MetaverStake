using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanets : MonoBehaviour
{
    public float rotY = 10.0f ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0.0f, rotY * Time.deltaTime, 0.0f);
    }
}
