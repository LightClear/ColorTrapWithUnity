using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMTest : MonoBehaviour
{
    public GameObject BGMPlayer;

    public AudioSource thisBGM;

    public void OnCicked()
    {
        AudioSource audioSource = BGMPlayer.GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.clip=thisBGM.clip;
        audioSource.Play();
    }
}
