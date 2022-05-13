using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectTextBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text_L1;
    [SerializeField] TextMeshProUGUI text_L2;
    [SerializeField] TextMeshProUGUI text_L3;
    [SerializeField] TextMeshProUGUI text_L4;

    public void SetTextBody(string str_L1, string str_L2, string str_L3, string str_L4)
    {
        text_L1.text = str_L1;
        text_L2.text = str_L2;
        text_L3.text = str_L3;
        text_L4.text = str_L4;

        text_L1.transform.parent.gameObject.SetActive(true);
        text_L2.transform.parent.gameObject.SetActive(true);
        text_L3.transform.parent.gameObject.SetActive(true);
        text_L4.transform.parent.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetTexts();
    }

    public void ResetTexts()
    {
        text_L1.text = "";
        text_L2.text = "";
        text_L3.text = "";
        text_L4.text = "";

        text_L1.transform.parent.gameObject.SetActive(false);
        text_L2.transform.parent.gameObject.SetActive(false);
        text_L3.transform.parent.gameObject.SetActive(false);
        text_L4.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
