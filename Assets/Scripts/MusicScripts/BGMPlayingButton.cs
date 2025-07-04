using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayingButton : MonoBehaviour
{
    public GameObject waitButton;
    public GameObject theBGMPlayer;
    public GameObject icon;
    
    public void OnClicked()
    {
        AudioSource audioSource = theBGMPlayer.GetComponent<AudioSource>();
        if(audioSource != null)
        {
            audioSource.Stop();
        }

        icon.transform.DOKill();
        icon.transform.rotation = Quaternion.Euler(0, 0, 0);

        waitButton.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
