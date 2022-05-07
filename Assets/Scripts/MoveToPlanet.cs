using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class MoveToPlanet : MonoBehaviour
{
    public float speed = 10.0f;
    public float distance = 70.0f;
    public GameObject backfireRight;
    public GameObject backfireLeft;
    [SerializeField] GameObject afterBurnerObj;
    [SerializeField] Image uiBackgroundImage;
    bool move = false;
    // Start is called before the first frame update
    [SerializeField] GameObject projectNameObj;
    [SerializeField] ProjectTextBehaviour _projectTextBehaviour;
    [SerializeField] float distanceCriteria = 25;

    string projectNameStr = "";
    string totalStakedStr = "";

    bool isHudShowing = false;


    void Start()
    {
        uiBackgroundImage.enabled = true;
        var c = uiBackgroundImage.color;
        c.a = 0f; // 初期値
        uiBackgroundImage.color = c;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            move = true;
            backfireRight.SetActive(true);
            backfireLeft.SetActive(true);
            afterBurnerObj.SetActive(true);
        }
        if (move)
        {
            gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;

            if (!isHudShowing)
            {
                //星に一定の距離まで近づいたらHUDを表示させる
                var direction = transform.forward;
                Vector3 rayPosition = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
                Ray ray = new Ray(rayPosition, direction);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.collider.gameObject.CompareTag("Planet"))
                    {
                        var tempDistance = Vector3.Distance(transform.position, hit.collider.gameObject.transform.position);
                        Debug.Log(tempDistance);
                        if (tempDistance < distanceCriteria)
                        {
                            SetHUD(hit.collider.gameObject);
                        }

                    }
                }
            }
        }
    }

    void ShowUI()
    {
        DOTween.ToAlpha(
            () => uiBackgroundImage.color,
            color => uiBackgroundImage.color = color,
            1f, // 目標値
            1.6f // 所要時間
        ).SetEase(Ease.InOutSine)
        .OnComplete(ShowText);
    }
    void ShowText()
    {
        Debug.Log("showtext called");
        _projectTextBehaviour.SetTextBody(projectNameStr, totalStakedStr, "", "");
    }

    private async void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.gameObject.CompareTag("Planet"))
        {
            StopMove();

            //            SetHUD(other.gameObject);

            //move = false;
            //backfireRight.SetActive(false);
            //backfireLeft.SetActive(false);
            //afterBurnerObj.SetActive(false);
            //projectNameObj.SetActive(false);
            //if (other.gameObject.TryGetComponent(out PlanetBehaviour _behaviour))
            //{
            //    projectNameStr = _behaviour.myProjectName;
            //    string[] tempAddress = { _behaviour.myProjectAddress };
            //    string resultStr = await APIManager.Instance.FetchEventDataByUniTask(tempAddress);
            //    Debug.Log(resultStr);
            //    JArray a = JArray.Parse(resultStr);
            //    Debug.Log(a[0]);
            //    totalStakedStr = a[0]["totalStaked"].ToString();
            //    Debug.Log(totalStakedStr);
            //}
            //ShowUI();
        }
    }

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
            projectNameStr = _behaviour.myProjectName;
            string[] tempAddress = { _behaviour.myProjectAddress };
            string resultStr = await APIManager.Instance.FetchEventDataByUniTask(tempAddress);
            Debug.Log(resultStr);
            JArray a = JArray.Parse(resultStr);
            Debug.Log(a[0]);
            totalStakedStr = a[0]["totalStaked"].ToString();
            Debug.Log(totalStakedStr);
            projectNameObj.SetActive(false);
            ShowUI();
        }
    }




}
