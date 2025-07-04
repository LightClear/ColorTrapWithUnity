using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BGMVolumeSeter : MonoBehaviour
{
    public GameObject bgmPlayer;
    private AudioSource bgmAudio;
    public Slider slider;
    public GameObject textObject;
    public TextMeshProUGUI text;

    private void Awake()
    {
        bgmAudio = bgmPlayer.transform.GetComponent<AudioSource>();
        if( bgmAudio != null)
        {
            print("成功获取BGMPlayer的AudioSource");
        }

        text = textObject.transform.GetComponent<TextMeshProUGUI>();

        if (DataManager.Instance.isLocalDeploy)
        {
            slider.value = PlayerPrefs.GetFloat("BGMVolume");
            bgmAudio.volume = slider.value;
        }

        text.text= ((int)(slider.value * 100f)).ToString();
    }

    public void OnSlide()
    {
        text.text = ((int)(slider.value * 100f)).ToString();
        bgmAudio.volume = slider.value;
        DataManager.Instance.BGMVolume = slider.value;

        if (DataManager.Instance.isLocalDeploy)
        {
            PlayerPrefs.SetFloat("BGMVolume", slider.value);
        }
    }
}
