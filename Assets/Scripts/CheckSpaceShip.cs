using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpaceShip : MonoBehaviour
{
    public float dist = 35.0f;
    public GameObject spaceShip;

    bool changeColor = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceVec = spaceShip.transform.position - gameObject.transform.position;
        if ( distanceVec.magnitude < dist)
        {
            if (!changeColor)
            {
                int colorNum = Random.Range(0, 2);
                switch (colorNum)
                {
                    case 0:
                        gameObject.GetComponent<Animator>().SetTrigger("Red");
                        break;
                    case 1:
                        gameObject.GetComponent<Animator>().SetTrigger("Green");
                        break;
                    case 2:
                        gameObject.GetComponent<Animator>().SetTrigger("Blue");
                        break;
                }
                changeColor = true;
            }
        }
        
    }
}
