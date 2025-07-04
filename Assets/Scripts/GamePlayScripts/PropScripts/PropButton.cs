using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public class PropButton : MonoBehaviour
{
    public AudioSource audioSource;
    public string key;
    public TextMeshProUGUI keyText;
    public TextMeshProUGUI amountText;

    public event Action useGoldApple;
    //public string propName;

    private void Awake()
    {
        keyText=gameObject.transform.Find("KeyText").GetComponent<TextMeshProUGUI>();
        amountText= gameObject.transform.Find("AmountText").GetComponent<TextMeshProUGUI>();

        SpawnHeart spawnHeart=FindObjectOfType<SpawnHeart>();
        if(spawnHeart != null)
        {
            useGoldApple += spawnHeart.SpawnGoldHeart;
        }
    }

    private void Update()
    {
        string input = UnityEngine.Input.inputString;
        if (!string.IsNullOrEmpty(input) && input == key)
        {
            OnClicked();
        }
    }

    private void OnEnable()
    {
        audioSource.volume = DataManager.Instance.SFXVolume;
        SetAmountText();
        SetKeyText();
    }

    void SetKeyText()
    {
        keyText.text= key;
        keyText.color= Color.yellow;
    }

    void SetAmountText()
    {
        int amount = 0;
        switch (gameObject.name)
        {
            case "Suspend":
                amount = DataManager.Instance.suspendPropAmount;
                break;
            case "GoldApple":
                amount = DataManager.Instance.goldApplePropAmount;
                break;
            case "Hamburger":
                amount = DataManager.Instance.hamburgerPropAmount;
                break;
            case "Totem":
                amount = DataManager.Instance.totemPropAmount;
                break;
        }
        amountText.text=amount.ToString();
        amountText.color= Color.yellow;
    }

    public void OnClicked()
    {
        switch (gameObject.name)
        {
            case "Suspend": 
                if (DataManager.Instance.suspendPropAmount > 0)
                {
                    DataManager.Instance.suspendPropAmount--;
                    if (DataManager.Instance.isLocalDeploy)
                        PlayerPrefs.SetInt("suspendPropAmount", DataManager.Instance.suspendPropAmount);
                    SetAmountText();
                    UseProp();
                }
                break;
            case "GoldApple":
                if (DataManager.Instance.goldApplePropAmount > 0)
                {
                    DataManager.Instance.goldApplePropAmount--;
                    if (DataManager.Instance.isLocalDeploy)
                        PlayerPrefs.SetInt("suspendPropAmount", DataManager.Instance.goldApplePropAmount);
                    useGoldApple?.Invoke();
                    DataManager.Instance.penaltyTime -= 6f;
                    SetAmountText();
                    UseProp();
                }
                break;
            case "Hamburger":
                if (DataManager.Instance.hamburgerPropAmount > 0)
                {
                    DataManager.Instance.hamburgerPropAmount--;
                    if (DataManager.Instance.isLocalDeploy)
                        PlayerPrefs.SetInt("suspendPropAmount", DataManager.Instance.hamburgerPropAmount);
                    SetAmountText();
                    UseProp();
                }
                break;
            case "Totem":
                if (DataManager.Instance.totemPropAmount > 0)
                {
                    DataManager.Instance.totemPropAmount--;
                    if (DataManager.Instance.isLocalDeploy)
                        PlayerPrefs.SetInt("suspendPropAmount", DataManager.Instance.totemPropAmount);
                    SetAmountText();
                    UseProp();
                }
                break;
        }

    }
    public void UseProp()
    {
        DataManager.Instance.isPropUsed = true;
        DataManager.Instance.theUsedPropName = gameObject.name;
        audioSource.Stop();
        audioSource.PlayOneShot(audioSource.clip);
        Debug.Log("使用道具" + gameObject.name);
    }
}
