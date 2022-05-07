using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class UnitaskSampleBehaviour : MonoBehaviour
{
    [SerializeField] private string queryWord = "hololens";
    string baseUrl = "https://connpass.com/api/v1/event/?keyword=";
    string url;


    //Coroutineで叩く例
    //private void Start()
    //{
    //    StartCoroutine(FetchEventDataByCoroutine(queryWord, OnFinishedCoroutine));
    //}


    ////Taskで叩く例
    //private async void Start()
    //{
    //    string result = await FetchEventDataByTask(queryWord);
    //    Debug.Log("結果：" + result);
    //}


    //UniTask2で叩く例
    private async void Start()
    {
        string result = await FetchEventDataByUniTask(queryWord);
        Debug.Log("結果：" + result);
    }


    private IEnumerator FetchEventDataByCoroutine(string queryWord, System.Action<string> callback)
    {
        var requestUrl = baseUrl + queryWord;
        //        Debug.Log(requestUrl);
        UnityWebRequest req = UnityWebRequest.Get(requestUrl);
        yield return req.SendWebRequest();

        string resultString = "";
        if (string.IsNullOrEmpty(req.error))
        {
            // Debug.Log(req.downloadHandler.text);
            resultString = req.downloadHandler.text;
        }
        else
        {
            Debug.Log(req.error);
            resultString = "レスポンスがエラー";
        }

        callback(resultString);
    }

    // コルーチン終了時
    public void OnFinishedCoroutine(string result)
    {
        Debug.Log("結果：" + result);
    }

    async Task<string> FetchEventDataByTask(string queryWord)
    {
        var requestUrl = baseUrl + queryWord;
        //        Debug.Log(requestUrl);
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

    async UniTask<string> FetchEventDataByUniTask(string queryWord)
    {
        var requestUrl = baseUrl + queryWord;
        //        Debug.Log(requestUrl);
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
