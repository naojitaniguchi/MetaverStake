using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
            if (gameObject.transform.position.magnitude > distance)
            {


                move = false;
                backfireRight.SetActive(false);
                backfireLeft.SetActive(false);
                afterBurnerObj.SetActive(false);
                projectNameObj.SetActive(false);
                ShowUI();
            }
        }
    }

    void ShowUI()
    {

        DOTween.ToAlpha(
            () => uiBackgroundImage.color,
            color => uiBackgroundImage.color = color,
            1f, // 目標値
            1f // 所要時間
        ).SetEase(Ease.InOutSine)
        .OnComplete(ShowText);
    }
    void ShowText()
    {
        Debug.Log("showtext called");
    }





}
