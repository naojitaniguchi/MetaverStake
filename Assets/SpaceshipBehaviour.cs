using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpaceshipBehaviour : MonoBehaviour
{
    [SerializeField] float rayDistance = 20;
    [SerializeField] TextMeshProUGUI projectNameText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var direction = transform.forward;

        Vector3 rayPosition = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        Ray ray = new Ray(rayPosition, direction);
        //        Debug.DrawRay(rayPosition, direction * rayDistance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            //            Debug.Log(hit.collider.gameObject.transform.position);
            //            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.CompareTag("Planet"))
            {
                if (hit.collider.gameObject.TryGetComponent(out PlanetBehaviour _behaviour))
                {
                    projectNameText.text = "Project\n" + _behaviour.myProjectName;
                }
            }
        }
        else
        {
            projectNameText.text = "";
        }



    }
}
