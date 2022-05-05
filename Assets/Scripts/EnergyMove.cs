using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyMove : MonoBehaviour
{
    public float speed = 10.0f;
    public float lifeTime = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LifeTimeCheck");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
    }

    IEnumerator LifeTimeCheck()
    {
        
        //3ïbí‚é~
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}
