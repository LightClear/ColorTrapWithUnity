using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class NaiLongTimer : MonoBehaviour
{
    public GameObject backButton;
    public GameObject bgmPlayer;
    public float timer;
    private bool haveSet;
    public float exitTime;
    public AudioSource nailongAudio;
    public float audioSpeed;
    public TextMeshProUGUI text;

    private void Awake()
    {
        text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        //AudioSource audioSource = transform.Find("Video").GetComponent<AudioSource>();
        //if (audioSource == null)
        //{
        //    Debug.LogError("Do not find video");
        //}
        //audioSource.volume = DataManager.Instance.BGMVolume;
        
        
        AudioSource bgm = bgmPlayer.GetComponent<AudioSource>();
        bgm.Stop();
        nailongAudio.volume = DataManager.Instance.BGMVolume;
        nailongAudio.pitch = audioSpeed;
        nailongAudio.Play();
        haveSet = true;
        backButton.SetActive(false);
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (haveSet && timer >= exitTime)
        {
            backButton.SetActive(true);
            haveSet = false;
        }
        text.text = "乱点是吧\n奖励你看三分钟奶龙\n" + (int)timer;
    }

    private void OnDisable()
    {
        nailongAudio.Stop();
        AudioSource bgm = bgmPlayer.GetComponent<AudioSource>();
        bgm.Play();
    }
}
