using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enargy : MonoBehaviour
{
    public GameObject[] capsules;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnRedButtonPressed();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            OnGreenButtonPressed();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            OnBlueButtonPressed();
        }
    }

    public void OnRedButtonPressed()
    {
        GameObject red = Instantiate(capsules[0]);
        red.transform.position = gameObject.transform.position;
        red.transform.rotation = gameObject.transform.rotation;
    }

    public void OnGreenButtonPressed()
    {
        GameObject green = Instantiate(capsules[1]);
        green.transform.position = gameObject.transform.position;
        green.transform.rotation = gameObject.transform.rotation;
    }

    public void OnBlueButtonPressed()
    {
        GameObject blue = Instantiate(capsules[2]);
        blue.transform.position = gameObject.transform.position;
        blue.transform.rotation = gameObject.transform.rotation;
    }


}
