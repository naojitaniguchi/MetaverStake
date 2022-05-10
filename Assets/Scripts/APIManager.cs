using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using System;
using TMPro;
public class APIManager : SingleInstance<APIManager>
{
    //    string baseUrl = "https://us-central1-metaverstake.cloudfunctions.net/projects?address=0x854fb5E2E490f22c7e0b8eA0aD4cc8758EA34Bc9";
    string baseUrl = "https://us-central1-metaverstake.cloudfunctions.net/projects";
    [SerializeField] string[] projAddress;
//    [SerializeField] TextMeshProUGUI resultText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public async void OnTestButtonPressed()
    {
        string result = await FetchEventDataByUniTask(projAddress);
        Debug.Log("結果：" + result);
//        resultText.text = result;
    }


    //async Task<string> FetchEventDataByTask()
    //{
    //    var requestUrl = baseUrl;
    //    //        Debug.Log(requestUrl);
    //    UnityWebRequest req = UnityWebRequest.Get(requestUrl);
    //    await req.SendWebRequest();
    //    if (string.IsNullOrEmpty(req.error))
    //    {
    //        // Debug.Log(req.downloadHandler.text);
    //        return req.downloadHandler.text;
    //    }
    //    else
    //    {
    //        Debug.Log(req.error);
    //        return "レスポンスがエラー";
    //    }
    //}

    public async UniTask<string> FetchEventDataByUniTask(string[] addresses)
    {
        string requestUrl = baseUrl;

        int tempIndex = 0;
        foreach (var eachAddress in addresses)
        {
            if (tempIndex == 0)
            {
                requestUrl = requestUrl + "?address=" + eachAddress;
            }
            else
            {
                requestUrl = requestUrl + "&address=" + eachAddress;
            }
            tempIndex++;
        }
        Debug.Log(requestUrl);

        UnityWebRequest req = UnityWebRequest.Get(requestUrl);
        await req.SendWebRequest();
        if (string.IsNullOrEmpty(req.error))
        {
            // Debug.Log(req.downloadHandler.text);
            return req.downloadHandler.text;
        }
        else
        {
            Debug.Log(req.error);
            return "レスポンスがエラー";
        }
    }


}
