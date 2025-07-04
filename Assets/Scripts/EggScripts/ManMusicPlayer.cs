using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManMusicPlayer : MonoBehaviour
{
    public GameObject BGMPLayer;
    private AudioSource BGMAudioSource;
    public AudioSource manAudio;

    private void Awake()
    {
        BGMAudioSource = BGMPLayer.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        BGMAudioSource.Stop();
        manAudio.Play();
    }

    private void OnDisable()
    {
        manAudio.Stop();
        BGMAudioSource.Play();
    }
}
