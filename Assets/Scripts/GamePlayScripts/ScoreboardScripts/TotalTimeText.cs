using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalTimeText : MonoBehaviour
{
    public TextMeshProUGUI text;

    private float totalTime;
    private float timer;
    private float propTimer;
    private bool isTotemUsed;
    private float penaltyTime;
    private string firstHalfText;
    private string mode;

    public AudioSource totemAudio;

    public event Action DestroyHeart;

    private void Awake()
    {
        SpawnHeart spawnHeart=FindObjectOfType<SpawnHeart>();
        if (spawnHeart != null)
        {
            DestroyHeart += spawnHeart.DestroyHeart;
        }
    }

    private void OnEnable()
    {
        totemAudio.volume=DataManager.Instance.SFXVolume;

        mode = DataManager.Instance.mode;
        if (DataManager.Instance.mode != "survival") firstHalfText = "总共剩余时间";
        else firstHalfText = "剩余饱食度：";

        timer = 0;
        propTimer = 0;
        isTotemUsed = false;
        
        if (DataManager.Instance.mode == "race") totalTime = DataManager.Instance.difficultyTime;
        else if (DataManager.Instance.mode == "limit") totalTime = 60f;
        else totalTime = 5f;

        SetText((int)Mathf.Ceil(totalTime - timer - DataManager.Instance.penaltyTime));
    }

    void Update()
    {
        //是否使用了道具
        if(!DataManager.Instance.isPropUsed)
        {
            timer += Time.deltaTime;
        }
        else if (DataManager.Instance.theUsedPropName == "Hamburger")
        {
            DataManager.Instance.penaltyTime -= 8f;
            timer += Time.deltaTime;
            DataManager.Instance.isPropUsed = false;
        }
        else if(DataManager.Instance.theUsedPropName == "Totem")
        {
            isTotemUsed= true;
        }
        else if (DataManager.Instance.theUsedPropName == "GoldApple")
        {
            propTimer += Time.deltaTime;
            if (propTimer >= 5f)
            {
                propTimer = 0;
                if (DataManager.Instance.mode != "limit") DataManager.Instance.isPropUsed = false;
            }
        }
        else if (DataManager.Instance.theUsedPropName == "Suspend")
        {
            propTimer += Time.deltaTime;
            if (propTimer >= 5f)
            {
                propTimer = 0;
                if (DataManager.Instance.mode != "limit") DataManager.Instance.isPropUsed = false;
            }
        }
        if(totalTime - timer - DataManager.Instance.penaltyTime > 60)
        {
            timer += totalTime - timer - DataManager.Instance.penaltyTime - 60f;
        }
        int remainTime = (int)Mathf.Ceil(totalTime - timer - DataManager.Instance.penaltyTime);
        if (totalTime - timer - DataManager.Instance.penaltyTime <= 0f) CheckDeath();
        SetText(remainTime);
    }

    void SetText(int remainTime)
    {
        text.text = firstHalfText + remainTime;
        SetColor(remainTime);
    }

    void SetColor(int remainTime)
    {
        if (mode != "survival")
        {
            if (remainTime <= totalTime / 3)
            {
                text.color = Color.red;
            }
            else
            {
                text.color = Color.green;
            }
        }
        else
        {
            if(remainTime <= 15f)text.color = Color.red;
            else text.color = Color.green;
        }
    }

    void CheckDeath()
    {
        switch (DataManager.Instance.mode)
        {
            case "race": DataManager.Instance.isDead = true; timer = 0; break;
            case "limit":
                if (isTotemUsed)
                {
                    Debug.Log("使用不死图腾，单限总时间增加十五秒");
                    totemAudio.PlayOneShot(totemAudio.clip);
                    timer -= 15f;
                    isTotemUsed = false;
                }
                else
                {
                    DataManager.Instance.isDead = true;
                    timer = 0;
                }
                break;
            case "survival": DataManager.Instance.penaltyTime = 0; DestroyHeart?.Invoke(); timer = 0; break;
            default: Debug.LogError("无法找到DataManager.Instance.mode" + DataManager.Instance.mode); break;
        }
    }

}
