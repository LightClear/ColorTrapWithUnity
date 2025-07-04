using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCurse : MonoBehaviour
{
    public GameObject yes;
    public GameObject no;
    public event Action UpdateText;

    //private void OnEnable()
    //{
    //    if (DataManager.Instance.isCurseUnlocked == 0)
    //    {
    //        yes.SetActive(false);
    //        no.SetActive(true);
    //    }
    //    else
    //    {
    //        yes.SetActive(true);
    //        no.SetActive(false);
    //    }
    //}

    public void OnClicked()
    {
        if (DataManager.Instance.isCurseUnlocked == 1)
        {
            Debug.Log("¹Ø±Õ×çÖä");
            DataManager.Instance.isCurseUnlocked = 0;
            //yes.SetActive(false);
            //no.SetActive(true);
        }
        else 
        {
            Debug.Log("¿ªÆô×çÖä");
            DataManager.Instance.isCurseUnlocked = 1;
            //yes.SetActive(true);
            //no.SetActive(false);
        }
        UpdateText?.Invoke();
        if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt("isCurseUnlocked", DataManager.Instance.isCurseUnlocked);
    }
}
