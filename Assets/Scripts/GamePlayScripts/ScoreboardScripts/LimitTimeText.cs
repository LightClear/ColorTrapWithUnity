using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class LimitTimeText : MonoBehaviour
{
    public TextMeshProUGUI text;
    private float timer;
    private float propTimer;
    private float difficultyTime;
    private bool isTotemUsed;

    public AudioSource totemAudio;

    public List<Option>options=new List<Option>();

    private string firstHalfText;
    private string mode;

    private void Awake()
    {
        firstHalfText = "本题剩余时间：";
        text.color = Color.yellow;

        for(int i=0;i<options.Count;i++)
        {
            options[i].HaveClicked += SetTimer;
        }
        
    }

    private void OnEnable()
    {
        totemAudio.volume = DataManager.Instance.SFXVolume;

        timer = 0;
        propTimer = 0;
        isTotemUsed = false;

        difficultyTime=DataManager.Instance.difficultyTime;
        mode=DataManager.Instance.mode;
    }

    private void Update()
    {
        //检测是否使用了时停道具
        if (!DataManager.Instance.isPropUsed) timer += Time.deltaTime;
        else if(DataManager.Instance.theUsedPropName == "Totem")
        {
            isTotemUsed=true;
            DataManager.Instance.isPropUsed = false;
        }
        else if(DataManager.Instance.theUsedPropName == "Suspend")
        {
            propTimer += Time.deltaTime;
            if (propTimer >= 5f)
            {
                propTimer = 0;
                DataManager.Instance.isPropUsed = false;
            }
        }

        int remainTime = (int)Math.Ceiling(difficultyTime - timer);
        SetText(remainTime);
        CheckDeath(difficultyTime - timer);
    }

    void SetText(int remainTime)
    {
        
        text.text = firstHalfText + remainTime;
    }

    public void SetTimer()
    {
        timer = 0;
    }

    private void CheckDeath(float remainTime)
    {
        if (remainTime <= 0f)
        {
            if (isTotemUsed)
            {
                Debug.Log("触发不死图腾，单限时间回复");
                totemAudio.PlayOneShot(totemAudio.clip);
                timer = 0;
            }
            else
            {
                DataManager.Instance.isDead = true;
                timer = 0;
            }
        }
    }
}
