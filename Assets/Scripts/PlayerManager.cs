using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;


public class PlayerManager : SingleInstance<PlayerManager>
{
    [SerializeField] float rayDistance = 100;
    [SerializeField] TextMeshProUGUI projectNameText;
    [SerializeField] GameObject pushToStartObj;

    public float rotationSpeed = -20.0f;

    public float speed = 10.0f;
    public float distance = 70.0f;
    public GameObject backfireRight;
    public GameObject backfireLeft;
    [SerializeField] GameObject afterBurnerObj;
    [SerializeField] Image uiBackgroundImage;

    bool move = false;

    [SerializeField] ProjectTextBehaviour _projectTextBehaviour;
    [SerializeField] float distanceCriteria = 25; //この距離でHUD準備、API叩くなど
    [SerializeField] float stopCriteria = 25; //この距離で停止

    [SerializeField] Web3Functions _web3Functions;


    //    string projectNameStr = "";
    string totalStakedStr = "";

    bool isHudShowing = false;


    //==================================================
    //==================================================

    ////星に近づきHUDの情報を生成したタイミングで値が記録される
    ////たとえばこんな形でプロジェクト名とアドレスを取得できます
    //string tempProjName = PlayerManager.Instance.targetProjectName;
    //string tempProvAddress = PlayerManager.Instance.targetProjectAddress;

    public string tentativeProjectNameInFront = ""; //回転しているときに正面にある星のプロジェクト名
    public string targetProjectName = ""; //星に近づいてHUDの準備をしている対象プロジェクト名
    public string targetProjectAddress = ""; //星に近づいてHUDの準備をしている対象のアドレス
    public float stakeValue = 0.0f;

    //==================================================
    //==================================================

    void Start()
    {
        projectNameText.text = "";
        pushToStartObj.SetActive(false);
        uiBackgroundImage.enabled = true;
        var c = uiBackgroundImage.color;
        c.a = 0f; // 初期値
        uiBackgroundImage.color = c;

    }

    // Update is called once per frame
    void Update()
    {
        var direction = transform.forward;

        Vector3 rayPosition = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        Ray ray = new Ray(rayPosition, direction);
        RaycastHit hit;
        var raycastResult = Physics.Raycast(ray, out hit, rayDistance);

        if (move)
        {
            gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;

            if (raycastResult)
            {
                if (hit.collider.gameObject.CompareTag("Planet"))
                {
                    var tempDistance = Vector3.Distance(transform.position, hit.collider.gameObject.transform.position);
                    //星に一定の距離まで近づいたら停止させる
                    if (tempDistance < stopCriteria)
                    {
                        Debug.Log("Stop Move at " + tempDistance);
                        StopMove();
                    }

                    if (!isHudShowing)
                    {
                        //星に一定の距離まで近づいたらHUDを表示させる
                        if (tempDistance < distanceCriteria)
                        {
                            Debug.Log("Prep HUD at " + tempDistance);
                            SetHUD(hit.collider.gameObject);
                        }
                    }
                }
            }
        }
        else
        {
            //動いていないときだけキー入力をうける、動いている途中で向きがかわると処理が面倒なので
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                gameObject.transform.Rotate(0.0f, rotationSpeed * Time.deltaTime, 0.0f);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                gameObject.transform.Rotate(0.0f, -1.0f * rotationSpeed * Time.deltaTime, 0.0f);
            }




            if (raycastResult)
            {
                //            Debug.Log(hit.collider.gameObject.transform.position);
                //            Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.CompareTag("Planet"))
                {
                    if (hit.collider.gameObject.TryGetComponent(out PlanetBehaviour _behaviour))
                    {
                        projectNameText.text = "Project\n" + _behaviour.myProjectName;
                        pushToStartObj.SetActive(true);
                        tentativeProjectNameInFront = _behaviour.myProjectName;
                    }
                }
            }
            else
            {
                projectNameText.text = "";
                pushToStartObj.SetActive(false);
                tentativeProjectNameInFront = "";
            }

            //Spacekeyを押して前に進めるのはプロジェクトが正面にあるときだけ
            if (Input.GetKeyDown(KeyCode.Space) && tentativeProjectNameInFront != "")
            {
                move = true;
                backfireRight.SetActive(true);
                backfireLeft.SetActive(true);
                afterBurnerObj.SetActive(true);
                pushToStartObj.SetActive(false);
            }

        }


        // if (Physics.Raycast(ray, out hit, rayDistance))
        // {
        //     //            Debug.Log(hit.collider.gameObject.transform.position);
        //     //            Debug.Log(hit.collider.gameObject.name);
        //     if (hit.collider.gameObject.CompareTag("Planet"))
        //     {
        //         if (hit.collider.gameObject.TryGetComponent(out PlanetBehaviour _behaviour))
        //         {
        //             projectNameText.text = "Project\n" + _behaviour.myProjectName;
        //             PlayerManager.Instance.tentativeProjectNameInFront = _behaviour.myProjectName;
        //         }
        //     }
        // }
        // else
        // {
        //     projectNameText.text = "";
        //     PlayerManager.Instance.tentativeProjectNameInFront = "";
        // }


        // if (move)
        // {
        //     gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
        //
        //     if (Physics.Raycast(ray, out hit, 100))
        //     {
        //         if (hit.collider.gameObject.CompareTag("Planet"))
        //         {
        //             var tempDistance = Vector3.Distance(transform.position, hit.collider.gameObject.transform.position);
        //             // Debug.Log(tempDistance);
        //
        //             //星に一定の距離まで近づいたら停止させる
        //             if (tempDistance < stopCriteria)
        //             {
        //                 Debug.Log("Stop Move at " + tempDistance);
        //                 StopMove();
        //             }
        //
        //             if (!isHudShowing)
        //             {
        //                 //星に一定の距離まで近づいたらHUDを表示させる
        //                 if (tempDistance < distanceCriteria)
        //                 {
        //                     Debug.Log("Prep HUD at " + tempDistance);
        //                     SetHUD(hit.collider.gameObject);
        //                 }
        //             }
        //         }
        //     }
        // }
    }

    void ShowUI(TweenCallback callback)
    {
        DOTween.ToAlpha(
                () => uiBackgroundImage.color,
                color => uiBackgroundImage.color = color,
                1f, // 目標値
                1.6f // 所要時間
            ).SetEase(Ease.InOutSine)
            .OnComplete(callback);
    }

    void ShowText()
    {
        Debug.Log("showtext called");

        _projectTextBehaviour.SetTextBody("total stake pending", targetProjectName, totalStakedStr + " ASTAR", "120%");
    }


    void ShowErrorText()
    {
        //APIたたいてエラーの場合
        Debug.Log("ShowErrorText called");

        _projectTextBehaviour.SetTextBody("Analyzing...", targetProjectName, "Analyzing...(H)",
            "Analyzing...(" + Time.realtimeSinceStartup.ToString("F0") + ")");
    }


    // private async void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log(other.gameObject.name);
    //
    //     if (other.gameObject.CompareTag("Planet"))
    //     {
    //         StopMove();
    //
    //         //            SetHUD(other.gameObject);
    //
    //         //move = false;
    //         //backfireRight.SetActive(false);
    //         //backfireLeft.SetActive(false);
    //         //afterBurnerObj.SetActive(false);
    //         //projectNameObj.SetActive(false);
    //         //if (other.gameObject.TryGetComponent(out PlanetBehaviour _behaviour))
    //         //{
    //         //    projectNameStr = _behaviour.myProjectName;
    //         //    string[] tempAddress = { _behaviour.myProjectAddress };
    //         //    string resultStr = await APIManager.Instance.FetchEventDataByUniTask(tempAddress);
    //         //    Debug.Log(resultStr);
    //         //    JArray a = JArray.Parse(resultStr);
    //         //    Debug.Log(a[0]);
    //         //    totalStakedStr = a[0]["totalStaked"].ToString();
    //         //    Debug.Log(totalStakedStr);
    //         //}
    //         //ShowUI();
    //     }
    // }

    private void StopMove()
    {
        move = false;
        backfireRight.SetActive(false);
        backfireLeft.SetActive(false);
        afterBurnerObj.SetActive(false);
    }


    private async void SetHUD(GameObject targetObj)
    {
        if (targetObj.TryGetComponent(out PlanetBehaviour _behaviour))
        {
            isHudShowing = true;
            targetProjectName = _behaviour.myProjectName;
            targetProjectAddress = _behaviour.myProjectAddress;
            string[] tempAddress = { _behaviour.myProjectAddress };
            string resultStr = "";

            try
            {
                resultStr = await APIManager.Instance.FetchEventDataByUniTask(tempAddress);
                Debug.Log(resultStr);
                JArray a = JArray.Parse(resultStr);
                Debug.Log(a[0]);
                totalStakedStr = a[0]["totalStaked"].ToString();
                Debug.Log(totalStakedStr);
                pushToStartObj.SetActive(false);
                ShowUI(ShowText);
            }
            catch (System.Exception E)
            {
                Debug.LogError("API叩く際にエラー");
                Debug.LogError(E.Message);
                totalStakedStr = "!!! API error";
                pushToStartObj.SetActive(false);
                ShowUI(ShowErrorText);
            }
        }
    }
}
