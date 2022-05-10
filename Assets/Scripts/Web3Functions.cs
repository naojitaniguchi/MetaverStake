using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.Networking;
using Newtonsoft.Json;



public class Web3Functions : MonoBehaviour
{
    public string[] projectAddressList;
    public GameObject[] Planets;

    [SerializeField] private ProjectTextBehaviour _projectTextBehaviour;
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

        // //表示を変えるとき
        // _projectTextBehaviour.SetTextBody(PlayerManager.Instance.targetProjectName, totalStakedStr, "pending", "120%");

    }

    private IEnumerator SetTotalStake()
    {
        yield return new WaitForSeconds(10);

        Debug.Log("SetTotalStake");
        string result = System.Text.Encoding.UTF8.GetString(GlobalVariables.SkatedAmount);
        Debug.Log(result);

        // //表示を変えるとき
        // _projectTextBehaviour.SetTextBody(PlayerManager.Instance.targetProjectName, totalStakedStr, "pending", "120%");


    }

    public void GetProjectStatus()
    {
        Debug.Log("GetProjectStatus called");

        // e.g.curl "https://us-central1-metaverstake.cloudfunctions.net/projects?address=0x854fb5E2E490f22c7e0b8eA0aD4cc8758EA34Bc9&address=0x92561F28Ec438Ee9831D00D1D59fbDC981b762b2"
        // -> [{ "totalStaked":0.0011,"apy":120},{ "totalStaked":0.1,"apy":120}]


        StartCoroutine(CallProjectMethod());
    }

    private IEnumerator CallProjectMethod()
    {

        string requestString = "https://us-central1-metaverstake.cloudfunctions.net/projects?";
        for (int i = 0; i < projectAddressList.Length; i++)
        {
            requestString += "address=";
            requestString += projectAddressList[i];
            if (i < projectAddressList.Length - 1)
            {
                requestString += "&";
            }
        }

        // Debug.Log(requestString);

        //1.UnityWebRequestを生成
        UnityWebRequest request = UnityWebRequest.Get(requestString);

        //2.SendWebRequestを実行し、送受信開始
        yield return request.SendWebRequest();

        //3.isNetworkErrorとisHttpErrorでエラー判定
        if (request.result == UnityWebRequest.Result.ProtocolError)
        {
            //4.エラー確認
            Debug.Log(request.error);
        }
        else
        {
            //4.結果確認
            Debug.Log(request.downloadHandler.text);
            ProjectStatusJson[] test = JsonConvert.DeserializeObject<ProjectStatusJson[]>(request.downloadHandler.text);

            double tempTotalStake = 0;
            string tempProjectStakeStr = "";

            Debug.Log(test.Length);
            for (int i = 0; i < test.Length; i++)
            {
                Debug.Log(test[i].totalStaked);
                Debug.Log(test[i].apy);

                Planets[i].GetComponent<PlanetBehaviour>().totalStaked = test[i].totalStaked;
                Planets[i].GetComponent<PlanetBehaviour>().apy = test[i].apy;

                tempTotalStake += test[i].totalStaked;

                if (projectAddressList[i] == PlayerManager.Instance.targetProjectAddress)
                {
                    tempProjectStakeStr = test[i].totalStaked.ToString();
                }

                //GlobalVariables.ProjectStatus[i].totalStaked = test[i].totalStaked;
                //GlobalVariables.ProjectStatus[i].apy = test[i].apy;
            }

            // //表示を更新
            _projectTextBehaviour.SetTextBody(PlayerManager.Instance.targetProjectName, tempProjectStakeStr + " ASTAR", tempTotalStake.ToString()+ " ASTAR", "120%");

        }

    }
}
