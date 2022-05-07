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
        titleText.text = "Project: " + title;
        totalStakeText.text = "Total Staked: " + totalStake + " ASTAR";
        yourStakeText.text = "Your Stake: " + yourStake + " ASTAR";
        YourRewardText.text = "Your Reward: " + yourReward + " ASTAR";
    }

    // Start is called before the first frame update
    void Start()
    {
        titleText.text = "";
        totalStakeText.text = "";
        yourStakeText.text = "";
        YourRewardText.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
