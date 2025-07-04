using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownManager : MonoBehaviour
{
    private float timer;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject game;
    private bool p1Active;
    private bool p2Active;
    private bool p3Active;

    public event Action CountDownOver;

    private void Awake()
    {
        //SpawnHeart spawnHeart = FindAnyObjectByType<SpawnHeart>();
        //if(spawnHeart!=null) CountDownOver += spawnHeart.SpawnRedHeart;
    }

    private void OnEnable()
    {
        game.SetActive(false);
        timer = 0;
        Debug.Log("timer等于0");
        p1.SetActive(true);
        p2.SetActive(true);
        p3.SetActive(true);
        p1Active = true;
        p2Active = true;
        p3Active = true;

        //将分数、惩罚时间设置为0
        DataManager.Instance.score = 0;
        DataManager.Instance.penaltyTime = 0;
    }

    void Update()
    {
        timer+= Time.deltaTime;
        if (p1Active && timer >= 3)
        {
            timer = 0;
            p1.SetActive(false);
            Debug.Log("倒计时1");
            p1Active = false;
            game.SetActive(true);
            CountDownOver?.Invoke();
        }
        else if (p2Active && timer >= 2)
        {
            p2.SetActive(false);
            Debug.Log("倒计时2");
            p2Active = false;
        }
        else if (p3Active && timer >= 1)
        {
            p3.SetActive(false);
            Debug.Log("倒计时3");
            p3Active = false;
        }
    }
}
