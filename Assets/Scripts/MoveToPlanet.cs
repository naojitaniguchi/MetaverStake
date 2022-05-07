using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlanet : MonoBehaviour
{
    public float speed = 10.0f;
    public float distance = 70.0f;
    public GameObject backfireRight;
    public GameObject backfireLeft;
    [SerializeField] GameObject afterBurnerObj;

    bool move = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            move = true;
            backfireRight.SetActive(true);
            backfireLeft.SetActive(true);
            afterBurnerObj.SetActive(true);
        }
        if (move)
        {
            gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
            if (gameObject.transform.position.magnitude > distance)
            {
                move = false;
                backfireRight.SetActive(false);
                backfireLeft.SetActive(false);
                afterBurnerObj.SetActive(false);
            }
        }
    }
}
