using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurseMgr : MonoBehaviour
{
    private int score;
    private string[] curse =
    {
        "smallSize", "moreOptions", "darkness", 
        "questionTimedDisplay", "positionExchange", "optionTimedDisplay",
        "errorDisplay", "localRotate", /*"optionMove",*/
        "randomPosition"
    };
    private string[] curseName =
    {
        "缩小", "更多选项", "黑暗",
        "题目限时显示", "上下交换", "选项限时显示",
        "错误显示", "旋转", /*"移动",*/
        "随机位置"
    };
    public GameObject questionGroup;
    public GameObject optionGroup;
    public GameObject darkness;
    public GameObject extraOptions;
    public GameObject theText;

    public TextMeshProUGUI text;

    private Vector3 initialQuestionGroupPosition;
    private Vector3 initialOptionGroupPosition;

    public event Action UseExtraOptions;

    private void Awake()
    {
        ObjectSpawn objectSpawn=FindObjectOfType<ObjectSpawn>();
        if (objectSpawn != null)
        {
            UseExtraOptions += objectSpawn.SetExtraOptionText;
        }
        else
        {
            Debug.LogError("未找到ObjectSpawn");
        }

        text=transform.Find("Text").GetComponent<TextMeshProUGUI>();

        initialQuestionGroupPosition = questionGroup.transform.position;
        initialOptionGroupPosition = optionGroup.transform.position;
    }

    private void OnEnable()
    {
        questionGroup.transform.position = initialQuestionGroupPosition;
        optionGroup.transform.position = initialOptionGroupPosition;
        darkness.SetActive(false);
        extraOptions.SetActive(false);
        theText.SetActive(false);
        score = 0;
        if (DataManager.Instance.isCurseUnlocked == 1)
        {
            text.text = "当前诅咒：无";
            theText.SetActive(true);
        }
    }

    private void Update()
    {
        int nowScore=DataManager.Instance.score;
        if(DataManager.Instance.isCurseUnlocked==1 && nowScore >= 5 && nowScore % 5==0 && nowScore != score)
        {
            UpdateCurse();
            score= nowScore;
        }
        if (DataManager.Instance.isCurseUnlocked == 1 && !DataManager.Instance.isCurseOperated)
        {
            CheckAmountOperated(DataManager.Instance.theOperatedAmount, DataManager.Instance.curse);
        }
    }

    void CheckAmountOperated(int amount, string curse)
    {
        switch (curse)
        {
            case "smallSize":
            case "localRotate":
            case "errorDisplay":
            case "randomPosition":
                if (amount >= 9)
                {
                    DataManager.Instance.isCurseOperated = true;
                    DataManager.Instance.theOperatedAmount = 0;
                }
                break;
            case "optionTimedDisplay":
                if(amount >= 8)
                {
                    DataManager.Instance.isCurseOperated = true;
                    DataManager.Instance.theOperatedAmount = 0;
                }
                break;
        }
    }

    void UpdateCurse()
    {
        if (DataManager.Instance.curse != "null")
        {
            switch (DataManager.Instance.curse)
            {
                case "darkness":
                    darkness.SetActive(false); break;
                case "moreOptions":
                    extraOptions.SetActive(false); break;
                case "positionExchange":
                    questionGroup.transform.position = initialQuestionGroupPosition;
                    optionGroup.transform.position = initialOptionGroupPosition;
                    break;
            }
        }
        DataManager.Instance.curse = curse[UnityEngine.Random.Range(0, curse.Length)];
        print("诅咒为" + DataManager.Instance.curse);
        DataManager.Instance.isCurseOperated = false;
        OperateCurse();
        SetText();
    }

    void OperateCurse()
    {
        switch (DataManager.Instance.curse)
        {
            case "darkness":
                darkness.SetActive(true);
                DataManager.Instance.isCurseOperated = true;
                break;
            case "positionExchange":
                questionGroup.transform.position = initialOptionGroupPosition;
                optionGroup.transform.position = initialQuestionGroupPosition;
                DataManager.Instance.isCurseOperated = true;
                break;
            case "moreOptions":
                extraOptions.SetActive(true);
                UseExtraOptions?.Invoke();
                DataManager.Instance.isCurseOperated= true;
                break;
        }
    }

    void SetText()
    {
        text.text = "当前诅咒：";
        string theCurse=DataManager.Instance.curse;
        for (int i = 0; i < curse.Length; i++)
        {
            if (curse[i] == theCurse)
            {
                //text.text += curseName[i];
                text.text += curse[i];
                break;
            }
        }
    }
}
