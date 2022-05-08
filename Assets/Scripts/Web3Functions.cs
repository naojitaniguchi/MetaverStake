using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


public class Web3Functions : MonoBehaviour
{
    public string[] projectAddressList;

    [DllImport("__Internal")] private static extern string WalletAddress();
    [DllImport("__Internal")] private static extern string getStakedCountAndAmount(byte[] array);
    [DllImport("__Internal")] private static extern string TestCopyToBuffer(byte[] array);
    [DllImport("__Internal")] private static extern string stake(byte[] projectAddress, byte[] _stakeAmount);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetWalletAddress()
    {
        return WalletAddress();
    }

    public void GetStake()
    {
        for (int i = 0; i < 1024; i++)
        {
            GlobalVariables.SkatedAmount[i] = 0;
        }
        getStakedCountAndAmount(GlobalVariables.SkatedAmount);

        StartCoroutine(SetTotalStake());
    }

    public void Stake()
    {
        string target = PlayerManager.Instance.targetProjectAddress;
        float stakeFloat = PlayerManager.Instance.stakeValue;
        byte[] projectAddress = System.Text.Encoding.UTF8.GetBytes(target);
        byte[] stakeCount = System.Text.Encoding.UTF8.GetBytes(stakeFloat.ToString());

        stake(projectAddress, stakeCount);
        PlayerManager.Instance.stakeValue = 0.0F;
    }

    private IEnumerator SetTotalStake()
    {
        yield return new WaitForSeconds(10);

        Debug.Log("SetTotalStake");
        string result = System.Text.Encoding.UTF8.GetString(GlobalVariables.SkatedAmount);
        Debug.Log(result);


    }
}
