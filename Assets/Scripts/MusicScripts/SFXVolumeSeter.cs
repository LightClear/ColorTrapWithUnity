using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SFXVolumeSeter : MonoBehaviour
{
    public GameObject textObject;
    public TextMeshProUGUI text;
    public Slider slider;

    private void Awake()
    {
        text = textObject.transform.GetComponent<TextMeshProUGUI>();

        if (DataManager.Instance.isLocalDeploy)
        {
            slider.value = PlayerPrefs.GetFloat("SFXVolume");
        }

        text.text = ((int)(slider.value * 100f)).ToString();
    }

    public void OnSlider()
    {
        DataManager.Instance.SFXVolume = slider.value;
        if (DataManager.Instance.isLocalDeploy)
        {
            PlayerPrefs.SetFloat("SFXVolume", slider.value);
        }
        text.text = ((int)(slider.value * 100f)).ToString();
    }
}
