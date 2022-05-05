using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnargy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.name);
        if (other.CompareTag("EnergyRed"))
        {
            gameObject.GetComponent<Animator>().SetTrigger("Red");
            Destroy(other);
        }
        if (other.CompareTag("EnergyGreen"))
        {
            gameObject.GetComponent<Animator>().SetTrigger("Green");
            Destroy(other);
        }
        if (other.CompareTag("EnergyBlue"))
        {
            gameObject.GetComponent<Animator>().SetTrigger("Blue");
            Destroy(other);
        }
    }
}
