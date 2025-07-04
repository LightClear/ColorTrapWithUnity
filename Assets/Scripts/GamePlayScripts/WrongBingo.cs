using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongBingo : MonoBehaviour
{
    private float timer;
    public AudioSource SFX;

    private void OnEnable()
    {
        timer = 0;
    }

    void Update()
    {
        timer+= Time.deltaTime;
        if(timer>=2f)gameObject.SetActive(false);
    }

    public void PlayeSound()
    {
        SFX.Stop();
        SFX.PlayOneShot(SFX.clip, DataManager.Instance.SFXVolume);
    }
}
