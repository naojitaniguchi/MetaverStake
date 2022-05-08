using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static byte[] SkatedAmount;
    public static ProjectStatusJson[] ProjectStatus;
    // Start is called before the first frame update
    void Start()
    {
        SkatedAmount = new byte[1024];
        ProjectStatus = new ProjectStatusJson[7];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
