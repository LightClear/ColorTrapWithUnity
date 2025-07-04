using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHeart : MonoBehaviour
{
    public GameObject redHeart;
    public GameObject goldHeart;
    public Transform scoreboard;
    private int nowHp;

    private bool isTotemUsed;
    private bool isGoldHeartSpawned;
    
    private List<GameObject> heartList = new List<GameObject>();
    public List<Option> options = new List<Option>();

    public AudioSource hurtAudio;
    public AudioSource totemAudio;

    private void Awake()
    {
        CountDownManager countDownManager=FindObjectOfType<CountDownManager>();
        if (countDownManager != null) countDownManager.CountDownOver += SpawnRedHeart;

        for (int i = 0; i < options.Count; i++)
        {
            options[i].DestroyHeart += DestroyHeart;
        }

        hurtAudio.volume = DataManager.Instance.SFXVolume;
        totemAudio.volume = DataManager.Instance.SFXVolume;
    }

    private void OnEnable()
    {
        isGoldHeartSpawned = false;
        isTotemUsed = false;
    }

    private void Update()
    {
        if (DataManager.Instance.isPropUsed)
        {
            
            if(DataManager.Instance.theUsedPropName == "Totem")
            {
                isTotemUsed=true;
                DataManager.Instance.isPropUsed=false;
            }
        }
    }

    public void SpawnARedHeart()
    {
        heartList.Add(Instantiate(redHeart, scoreboard));
        nowHp++;
    }

    public void SpawnRedHeart()
    {
        heartList.Clear();
        nowHp = DataManager.Instance.difficultyHp;
        for (int i = 0; i < nowHp; i++)
        {
            heartList.Add(Instantiate(redHeart, scoreboard));
        }
    }

    //[ExecuteInEditMode]
    public void SpawnGoldHeart()
    {
        Debug.Log("生成一颗金心");
        heartList.Add(Instantiate (goldHeart,scoreboard));
        DataManager.Instance.isPropUsed = false;
        nowHp++;
    }

    //[ExecuteInEditMode]
    public void DestroyHeart()
    {
        Debug.Log("摧毁一颗" + heartList[nowHp - 1].name);
        Destroy(heartList[nowHp-1]);
        heartList.RemoveAt(nowHp - 1);
        nowHp--;
        hurtAudio.PlayOneShot(hurtAudio.clip);
        if (nowHp == 0)
        {
            if (isTotemUsed)
            {
                Debug.Log("触发不死图腾，生成一颗红心");
                totemAudio.PlayOneShot(totemAudio.clip);
                SpawnARedHeart();
                DataManager.Instance.penaltyTime -= 5f;
            }
            else DataManager.Instance.isDead = true;
        }
    }
}
