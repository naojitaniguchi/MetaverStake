using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnargy : MonoBehaviour
{
    Animator myAnimator;
    bool isAnimationOn = false;


    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimationOn)
        {
            Debug.Log(myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            if (myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 3)
            {
                //3回繰り返したら停止
                myAnimator.SetBool("boolRed", false);
                myAnimator.SetBool("boolGreen", false);
                myAnimator.SetBool("boolBlue", false);
                isAnimationOn = false;
            }
        }




    }
    void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.name);
        if (other.CompareTag("EnergyRed"))
        {
            //            gameObject.GetComponent<Animator>().SetTrigger("Red");
            myAnimator.SetBool("boolRed", true);
            Invoke(nameof(SetAnimationFlag), 0.3f);
            Destroy(other);
        }
        if (other.CompareTag("EnergyGreen"))
        {
            //gameObject.GetComponent<Animator>().SetTrigger("Green");
            myAnimator.SetBool("boolGreen", true);
            Invoke(nameof(SetAnimationFlag), 0.3f);
            Destroy(other);
        }
        if (other.CompareTag("EnergyBlue"))
        {
            //gameObject.GetComponent<Animator>().SetTrigger("Blue");
            myAnimator.SetBool("boolBlue", true);
            Invoke(nameof(SetAnimationFlag), 0.3f);
            Destroy(other);
        }
    }

    void SetAnimationFlag()
    {
        isAnimationOn = true;
    }

}
