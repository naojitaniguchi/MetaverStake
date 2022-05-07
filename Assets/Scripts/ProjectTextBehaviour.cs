using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectTextBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI totalStakeText;
    [SerializeField] TextMeshProUGUI yourStakeText;
    [SerializeField] TextMeshProUGUI YourRewardText;

    public void SetTextBody(string title, string totalStake, string yourStake, string yourReward)
    {
        titleText.text = title;
        totalStakeText.text = totalStake + " ASTAR";
        yourStakeText.text = yourStake + " ASTAR";
        YourRewardText.text = yourReward + " ASTAR";

        titleText.transform.parent.gameObject.SetActive(true);
        totalStakeText.transform.parent.gameObject.SetActive(true);
        yourStakeText.transform.parent.gameObject.SetActive(true);
        YourRewardText.transform.parent.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        titleText.text = "";
        totalStakeText.text = "";
        yourStakeText.text = "";
        YourRewardText.text = "";

        titleText.transform.parent.gameObject.SetActive(false);
        totalStakeText.transform.parent.gameObject.SetActive(false);
        yourStakeText.transform.parent.gameObject.SetActive(false);
        YourRewardText.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
