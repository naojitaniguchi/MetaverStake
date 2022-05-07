using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehaviour : MonoBehaviour
{
    public string myProjectName;
    // Start is called before the first frame update
    void Start()
    {
        if (string.IsNullOrEmpty(myProjectName))
        {
            myProjectName = gameObject.name;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
